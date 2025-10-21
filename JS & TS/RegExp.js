const s = "a1good111example";
//there are two ways do initialize regExp:
//First way:
const regExp = /1+/; // 1+ "1"

//this will replace only first occurrence:
let sArray = s.trim().replace(regExp, " ").split(" ");//[ 'a', 'good111example' ]

//to replace all occurrence use:
const regExpAll = /\s/g;
let sArray2 = s.trim().replace(regExpAll, " ").split(" "); // ["a", "good", "example"]

//you can use regExp in split:
let sArray3 = s.trim().split(regExpAll);// ["a", "good", "example"]

//Second way:
const regExp2 = new RegExp("\\s+"); //in this case js string "\s+" converts into "s+", so you need double \
//it works as "const regExp2 = /\s/g;" (for all occurrence)
