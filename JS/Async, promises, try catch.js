//setTimeout is a JS function that runs the passed function after delay (ms)
setTimeout(() => console.log("Delayed"), 1000); //doesn't block the program, just execute this later

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
setTimeout(() => Promise.resolve().then(() => console.log("setTimeout Promise.then")), 0);
setTimeout(() => console.log("setTimeout"), 0); //schedules it to macrotask queue - runs after microtask queue
Promise.resolve().then(() => console.log("Promise.then"));
console.log("End");

//Output
// Start
// End
// Promise.then
// setTimeout Promise.then
// setTimeout

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
//- awaits fro fetch and then awaits for res.json()

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
} catch (e){                                              // call stack reach this line, in this case await Promise.all
    console.log(`Caught error with message: ${e}`);       // will be finished immediately 
} finally {
    console.log("End of try-catch");
}
//Output after 1 s (waits for longer rejectPromise2)
// Caught error with message: bad request
// End of try-catch

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
    const userPosts = await getPostsByUser(userId);
//    Promise.all([user, userPosts]);
    
//    const awaitedU = await user;
//    const awaitedUP = await userPosts;
    
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


  