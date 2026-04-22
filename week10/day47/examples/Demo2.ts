function f1()
{
    console.log("Welcome to Type Script of Angular");
}

function greeting(uname:string):void
{
    var str:string="Welcome to "  + uname;
    console.log(str);
}

function sum(x:number, y:number):number
{
    var z:number;
    z=x+y;
    return z;
}

f1();
greeting("Syam");
var n:number= sum(10,5);
console.log("Sum Result is "+n);
export{};

