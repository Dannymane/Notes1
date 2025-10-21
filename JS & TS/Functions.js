function myFunction(p1, p2) {
	return p1 * p2;
  }

function sayHi() {
	console.log("Hi");
}

let greeter = sayHi; // Assigning function to variable
greeter(); // Calls sayHi

let a = add(5,5);
function add(a, b) { //functions hoisted to the top of the scope
    return a + b;
}

let a1 = multiply(2, 2); //error
const multiply = function(a, b) { //function expressions don't hoisted
    return a * b;
};

function counter() {}
counter.value = 0;
console.log(counter.value);

let str1 = new Object("0");
let str2 = new Object("0");
let zero = 0;
console.log(str1 == zero);              // true
console.log(str2 == zero);                                  // true
console.log(str1 == str2);              // false

//"params" in JS
function sum(...numbers) {
    let sum = 0; //jest let sum; won't work;
    for(let i = 0;i < numbers.length;i++){
        sum += numbers[i];
    }
    return sum;
}
console.log(sum(1, 2, 3, 4)); // 10

//Closure:
function getCounter() {
    let count = 0;
    return () => ++count;
}

const counter = getCounter();
console.log(counter()); // 1
console.log(counter()); // 2

//another closure
function getFunction2(message) {
    let invokeCount = 0;
    
    return function(message) {
        invokeCount++;
        console.log('Function invoked: ' + invokeCount + ' times. Message: ' + message);
    };
}

console.log(this); // In browser: Window object

//inside object
function regularFunction() {
    console.log(this.name);
}

let obj = { name:'Alice', func:regularFunction };
obj.func(); //Alice


//Lost content
const person = {
    name: "Bob",
    greet() {
        console.log(this.name);
    }
};

const sayHi = person.greet;
sayHi(); // ❌ undefined (or global object)


// call/apply, bind
function show() {
    console.log(this);
}

const obj2 = { name: "Dan" };
obj2.show(); // Error - Property 'show' does not exist on type '{ name: string; }'
show.call(obj2);  // { name: 'Dan' }    
show.apply(obj2);  // { name: 'Dan' } // same as call


const bound = show.bind(obj2);
bound(); // sets `this` to obj2

