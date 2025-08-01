const s = "a good   example";
//two ways do initialize regExp:
const regExp = /\s+/; // 1+ whitespaces
const regExp2 = new RegExp("\\s+"); //in this case js string "\s+" converts into "s+", so you need double \

//this will replace only first occurrence:
let sArray = s.trim().replace(regExp, " ").split(" ");

//to replace all occurrence use:
const regExpAll = /\s/g;

//you can use regExp in split:
let sArray2 = s.trim().split(/\s+/);// ["a", "good", "example"]