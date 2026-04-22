let arr:number[]=[1,2,3,4,5];
console.log("array length : "+arr.length);
for(let i in arr)
{
    console.log(i);
}

arr.push(6);
console.log("array length : "+arr.length);
console.log(arr);

console.log("-------------------")
let mapResult= arr.map(n=>n*2);
console.log("MapResilt =" +mapResult);
console.log("-------------------")

let filterResult=arr.filter(n=>n%2==0);
console.log("FilterResult ="+filterResult);
console.log("-------------------")
let total=arr.reduce((sum,n)=>sum+n,0);
console.log("total sum of array =" + total);

console.log("-------------------")
arr.pop();
console.log(arr);
console.log("-------------------")
arr.shift();
console.log(arr);
console.log("-------------------")
arr.unshift(1);
console.log(arr)

console.log("---string array--");
let names: string[] = ["Syam", "saravana"];

names.push("kumar");
console.log(names);

console.log("----array as object---");

type Users=
{
    id:number;
    name:string;
};

let users:Users[]=
[
    {id:1,name:"syam"},
    {id:2,name:"kumar"},
];

console.log(users[0]);
console.log("-------------------")
console.log(users);


export{};
