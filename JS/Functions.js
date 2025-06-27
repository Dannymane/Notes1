function myFunction(p1, p2) {
	return p1 * p2;
  }

function sayHi() {
	console.log("Hi");
}

let greeter = sayHi; // Assigning function to variable
greeter(); // Calls sayHi



let a = multiply(2, 2); //error
const multiply = function(a, b) { //function expression
    return a * b;
};

