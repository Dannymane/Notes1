//Arrays
const cars = ["Saab", "Volvo", "BMW"];

const cars2 = [];
cars2[0]= "Saab";
cars2[1]= "Volvo";
cars2[2]= "BMW";

cars2.push("Toyota");//and a lot of other methods

const cars3 = new Array("Saab", "Volvo", "BMW");

//Object 
const person = {
	name: "Alice",
	age: 30
};

  
//Maps
//Maintains insertion order
const map = new Map(); //.set, .get, .has, .delete, .clear
map.set("key", 123);
map.set({ id: 1 }, "value");

console.log(map.get("key")); //123

// List all entries
let text = "";
map.forEach (function(value, key) {
  text += key + ' = ' + value;
})
console.log(text); //key = 123[object Object] = value

//Sets
const set = new Set([1, 2, 3, 3]); //.add, .has, .delete
console.log(set); // 1, 2, 3

//loops
//c# foreach equivalent
for(let e of set) {}
//same as for(let i = 0;i < array.length;i++)
for(let i in array) {}

