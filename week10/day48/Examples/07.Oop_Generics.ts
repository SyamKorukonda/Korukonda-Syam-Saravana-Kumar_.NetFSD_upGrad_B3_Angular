function identity<T>(args:T):T{
    return args;
}

let output1=identity<string>("Hello");  //explicit type
let output2=identity(45);               // type inference → number     
let output3=identity<boolean>(true);
let output4=identity("syam");
let output5=identity(false);

console.log(typeof output1);
console.log(typeof output2);
console.log(typeof output3);
console.log(typeof output4);
console.log(typeof output5);

console.log("-----------------------");
console.log(output1);
console.log(output2);
console.log(output3);
console.log(output4);
console.log(output5);