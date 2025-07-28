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

