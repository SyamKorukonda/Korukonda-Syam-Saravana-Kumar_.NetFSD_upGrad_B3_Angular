const userName:string="syam";
let age:number=21;
const email:string="syam@gmail.com";
const isSuscribed:boolean=true;

let city="Sattenapalli"; //Type Inference
let loginCount=5;

age=age+1;

const profileMsg: string= `Hello ${userName}  are ${age} years old and your email is ${email}. `;

const isAdult:boolean= age>18;

const isEligibleForPremimum:boolean= isAdult && isSuscribed;

console.log("User Name:", userName);
console.log("Age:", age);
console.log("Email:", email);
console.log("Subscribed:", isSuscribed);

console.log("City (Type Inference):", city);
console.log("Login Count (Type Inference):", loginCount);

console.log("Profile Message:", profileMsg);

console.log("Is Adult:", isAdult);
console.log("Eligible for Premium:", isEligibleForPremimum);