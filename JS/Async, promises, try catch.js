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
});

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
const resolvePromise = new Promise((resolve, reject) => {
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

// proper using try-catch:
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
// Caught error with message: bad request
// End of try-catch

//Promise.all
try{
    await Promise.all([
        resolvePromise.then((result) => console.log(result)).catch((error) => {throw new Error(error);}), 
        rejectPromise.then((result) => console.log(result)).catch((error) => {throw new Error(error);})
    ]);
} catch (e){
    console.log(`Caught error with message: ${e.message}`);
} finally {
    console.log("End of try-catch");
}
//Output if timeout resolvePromise is longer than rejectPromise:
// Caught error with message: bad request
// End of try-catch
// ok


//Async await replaces this:
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
  

//you can mix await with .then:
await fetch(url).then(res => res.json());

//but it's better to use 
const res = await fetch(url);
const data = await res.json();
  