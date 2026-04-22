// Create TypeScript program to demonstrate usage of Functional parameter types

// 1. Optional parameters

function showDetails(sname:string,course:string,age?:number)
{
    console.log(sname);
    console.log(course);
    if(age!=undefined)
    {
        console.log(age);
    }
}

function getTotal(price:number,qty:number=1)
{
    var total:number=price*qty;
    var str:string=`Unit Price= ${price}, Quantity=${qty} total Amount =${total}`;
    console.log(str);
}

function sumArray(...arr:number[])
{
    var s:number=0;
    for(var i in arr)
    {
        s+=arr[i];
    }
    console.log("Sum of Array=" +s);
}

showDetails("syam","C#",21);
console.log();
showDetails("kumar",".Net");
console.log();

getTotal(25,5);
console.log();
getTotal(220);
console.log();

sumArray(10,20);
console.log();
sumArray(10,20,30,40);
console.log();
sumArray(10, 20, 30, 40, 50, 60, 70, 80, 90);
console.log();
sumArray(10);
export{};
