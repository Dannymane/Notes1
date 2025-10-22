//Don’t try to fully understand how it works, there is a big mess with it. Before read this file, check 
// 4.6.1 Short tutorial - this is enough to know. 

//setTimeout is a JS function that runs the passed function after delay (ms)
setTimeout(() => console.log("Delayed"), 1000); //doesn't block the program, just execute this later

//after long investigation

const promise0 = new Promise((resolve, reject) => {
        resolve("Done!");  // marks as successful or reject("Error!") - marks as failed
});

const promise = new Promise((resolve, reject) => {
    // async work here
    setTimeout(() => {
        resolve("Done!"); 
    }, 1000);
}); //promise fetch/run timeout here, not below with .then.catch...

promise
    .then(result => console.log(result)) // if resolved
    .catch(error => console.error(error)) // if rejected
    .finally(() => console.log("Always runs"));

//example
function getUser() {
    return new Promise((resolve, reject) => {
        setTimeout(() => resolve({ name: "Alice" }), 1000);
    });
}

getUser()
    .then(result => console.log(result.name)) // "Alice"
    .catch(err => console.error(err));

//------------------------------------------------------------------------------------
// info below is important but it works differently with await -> for await see below

console.log("Start");
const promise2 = new Promise((resolve, reject) => {

    for (let i = 0; i < 1e9; i++) {}  // ~1 second 
    console.log("Inside executor");
    resolve("Done!");
})
.then(r => console.log(r)); //then schedules microtask
// microtasks are run always after the current synchronous code
for (let i = 0; i < 1e9; i++) {}  // ~1 second 
console.log("End");

//Output
// Start
// Inside executor   <-- after 1 s
// End 				 <-- after 1 s
// Done!

// Current stack, microtask queue, macrotask queue 
console.log("Start");

const promiseTimeout1 = new Promise((resolve, reject) => {      //schedules it to macrotask queue - runs after microtask queue
        setTimeout(() => resolve("Promise.then.timeout 1"), 0 );
    })
    .then((result) => console.log(result)); //.then schedules it to  microtask queue, but timeout inside moves it to macrotask queue

setTimeout(() => Promise.resolve().then(() => console.log("setTimeout Promise.then")), 0);
setTimeout(() => console.log("setTimeout"), 0); 
Promise.resolve().then(() => console.log("Promise.then")); //schedules it to  microtask queue

const promiseTimeout2 = new Promise((resolve, reject) => {   
        setTimeout(() => resolve("Promise.then.timeout 2"), 0 );
    })
    .then((result) => console.log(result));

console.log("End");

//Output
// Start
// End
// Promise.then
// Promise.then.timeout 1
// setTimeout Promise.then //but if timeout ends after "setTimeout" - it will be the last, even if both ends before "end"
// setTimeout
// Promise.then.timeout 2

//Finally: 
// Sync code runs first,                                                                                     Sync queue.
// Then promises without timeout (microtasks) even they finished earlier than sync.                          Microtask queue. 
//  Then any timeouts regardless it within Promise or not, even they finished earlier than promises/sync.    Macrotask queue.
// the order within each queue is depends on finishing time

//with await:

console.log("sync  1");
setTimeout(() => console.log("setTimeout 1"), 0);
fetchData();
async function fetchData() {
    try {
      setTimeout(() => console.log("setTimeout inside 1"), 0);
      console.log("sync inside 1");
      const res = await fetch("https://api.example.com/data"); //throws error and code below won't be executed
      const data = await res.json();
      console.log(data);
      console.log("sync inside 2");
    } catch (e) {
      console.error("Failed:", e.message);
    }
}
setTimeout(() => console.log("setTimeout 2"), 0);
console.log("sync  2");  

//output: 
// sync  1
// sync inside 1
// sync  2
// setTimeout 1
// setTimeout inside 1
// setTimeout 2
// Exception caught: fetch failed


//Async/await
//await replaces this:
promise.then(result => {
    // ...
  }).catch(err => {
    // ...
  });
//to this
try {
    const result = await promise;
  } catch (err) {
    // ...
  }
  
//async function always returns promise
async function getData() {
    return 42;
  }
  
  getData().then(console.log); // logs 42
  //Behind the scenes: return 42; is turned into return Promise.resolve(42);


  async function fetchData() {
    try {
      const res = await fetch("https://api.example.com/data");
      const data = await res.json();
      console.log(data);
    } catch (e) {
      console.error("Failed:", e.message);
    }
  }
  
  fetchData(); //Failed: fetch failed
  

//Don’t mix await and .then, instead of this:
const data = await fetch(url).then(res => res.json()); //note that await is applied for whole .then chain 
//- awaits for fetch and then awaits for res.json()

//write this
const res = await fetch(url);
const data = await res.json();

//try catch 
try {
    throw new Error("Error message");
  } catch (err) {
    console.error("Error caught:", err.message);
  } finally {
    console.log("Try-catch run");
  }
//try catch works only with synchronous code, or with await (also sync) (same as in C#)
async function fetchData() {
    try {
        const response = await fetch("https://api.example.com/data");
        const data = await response.json();
        console.log(data);
    } catch (e) {
        console.error("Fetch failed", e);
    }
}

async function fetchData() {
    try {
        const response = fetch("https://api.example.com/data"); //won't catch async error
    } catch (e) {
        console.error("Fetch failed", e);
    }
}


// wrong using try-catch:
const resolvePromise = new Promise((resolve, reject) => {   // resolvePromise start Timeout here since this line, not in .then
    setTimeout(() => resolve("ok"), 1000 );
});

const rejectPromise = new Promise((resolve, reject) => {
    setTimeout(() => reject("bad request"), 1000);
});

try{
    resolvePromise.then((result) => console.log(result))
        .catch((error) => { throw new Error(error); }); 

    rejectPromise.then((result) => console.log(result))
        .catch((error) => {throw new Error(error)});
} catch (e) {
    console.log(`Registered error: ${e.message}`);
} finally {
    console.log("End of try catch");
}
//the output:
// End of try catch
// ok
// ERROR! ... (thrown error outside of try-catch)

// wrong mixing try-catch with .then.catch:
const resolvePromise1 = new Promise((resolve, reject) => {
    setTimeout(() => resolve("ok"), 1000 );
});

const rejectPromise1 = new Promise((resolve, reject) => {
    setTimeout(() => reject("bad request"), 1000);
});

try{
    await resolvePromise1.then((result) => console.log(result))
        .catch((error) => { throw new Error(error); });

    await rejectPromise1.then((result) => console.log(result))
        .catch((error) => {throw new Error(error)});
} catch (e) {
    console.log(`Registered error: ${e.message}`);
} finally {
    console.log("End of try catch");
}

//the output appears in 1 second: (because the promises are run during their initialization, not when .then/.catch assigned)
// ok 
// ERROR!
// Registered error: bad request
// End of try-catch

//instead of this use: (same promises, same output)

try{
    const result = await resolvePromise1;
    console.log(result);

    const result2 = await rejectPromise1;
    console.log(result2);                   //won't be reached
} catch (e) {
    console.log(`Registered error: ${e}`);  //e instead of e.message because reject(plain_string)
} finally {
    console.log("End of try catch");
}                                           

//Promise.all
const resolvePromise2 = new Promise((resolve, reject) => { // runs the promise
    setTimeout(() => resolve("ok"), 10 );
});

const rejectPromise2 = new Promise((resolve, reject) => { // runs the promise
    setTimeout(() => reject("bad request"), 1000);
});

try{
    await Promise.all([resolvePromise2, rejectPromise2]); // promises were run earlier, they even might finish faster than 
                                                          // call stack reach this line, in this case await Promise.all
                                                          // will be finished immediately 

    const ok = await resolvePromise2;   //won't reach this because of earlier reject in Promise.all, but you can put it before Promise.all
    console.log(ok);
} catch (e){                                             
    console.log(`Caught error with message: ${e}`);       
} finally {
    console.log("End of try-catch");
}
//Output after 1 s (waits for longer rejectPromise2)
// Caught error with message: bad request
// End of try-catch

//the correct way to use Promise.all that returns an array of responses
try {
    const [ok, ok2] = await Promise.all([resolvePromise2, rejectPromise2]); //(if one rejects - immediately goes to catch)
    console.log(ok); //ok and ok2 are two const variables
    console.log(ok2);
} catch (e) {
    console.log(`Caught error with message: ${e}`);
} finally {
    console.log("End of try-catch");
}
//also you can use Promise.allSettled to get all results including rejected ones - it never throws an error

//Full flow example TS
async function getUser(id: number) {
    const res = await fetch(`/api/users/${id}`);
    if (!res.ok) throw new Error("User not found");
    return res.json();
}
  
async function main() {
    try {
        const user = await getUser(1);
        console.log("User:", user);
    } catch (e) {
        console.error("Error:", e);
    }
}





async function getUser(userId: number): Promise<{ id: number, name: string }> {
    return new Promise((resolve) => {
      setTimeout(() => resolve({ id: userId, name: "Alice" }), 1000);
    });
  }
  
 async function getPostsByUser(userId: number): Promise<string[]> {
    return new Promise((resolve) => {
      setTimeout(() => resolve(["Post 1", "Post 2", "Post 3"]), 1500);
    });
  }
  
async function fetchUserData(userId: number): Promise<object>{
try{
    const user = await getUser(userId);
    const userPosts = await getPostsByUser(userId); //executes only when getUser return result (if error -> catch)
//    [user, userPosts] = Promise.all([user, userPosts]); - best way to run in parallel
    
    const obj = {name: user.name, posts: userPosts};
    return obj;
  } catch (e){
    return e;
  }
}

async function main(): Promise<void>{
    const result = await fetchUserData(3);
    console.log(result);
}

main();


//Microtask and Macrotask queues with async/await

console.log("A");
Promise.resolve().then(() => console.log("Promise.then"));
console.log("B");

// A 
// B
// Promise.then

console.log("A");
await Promise.resolve().then(() => console.log("Promise.then"));
console.log("B");

// A
// Promise.then
// B

//after await the subsequent code goes to microtask queue:

async function getOne(){ //instead of `await getOne();` might be any await `await Promise.resolve().then(() => console.log("Promise.then"));`
    return 1;
}
  
console.log("A");                                           //stack
setTimeout(() => console.log("setTimeout"));                //schedules to macrotask queue
Promise.resolve().then(() => console.log("Promise.then"));  //schedules to microtask queue
console.log(await getOne());                                             //run getOne() and await result - moves subsequent code to microtask queue
console.log("B");

// A
// Promise.then
// B
// setTimeout

//so the B was moved to microtask queue where the Promise.then was first



//summary for async/await:
//awaited code and sync code "in same queue"
//await moves subsequent code to microtask queue, after any await the current microtask queue is executed
//setTimeout always goes to macrotask queue and runs after sync, awaited and microtask code