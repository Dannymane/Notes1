const person = { name: "Alice" };

person.name = "Bob";           // ✅ OK
person.age = 30;               // ✅ OK — adding new property
// person = { name: "Carol" }; // ❌ Error — reassignment


//returns an object same as item but with additional isEndOfShelf property (if exist - replaces the original value)
return {
    ...item,
    isEndOfShelf
};


//---
let a = 2; 
let a = 3; //error
