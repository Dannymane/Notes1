const person = { name: "Alice" };

person.name = "Bob";           // ✅ OK
person.age = 30;               // ✅ OK — adding new property
// person = { name: "Carol" }; // ❌ Error — reassignment