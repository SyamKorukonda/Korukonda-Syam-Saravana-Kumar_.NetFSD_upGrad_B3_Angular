class Student
{
    // Props
    public sid:number  =  0;
    public sname:string   = "";
    public email:string  = "";

    // Methods
    public showDetails():void
    {
        console.log("Student Id : "  +  this.sid);
        console.log("Student Name : "  +  this.sname);
        console.log("Email Id : "  +  this.email);
    }
}

 
var s1   = new Student();
s1.sid = 4455;
s1.sname   = "Syam";
s1.email   = "Syam@gmail.com";

var s2   = new Student();
s2.sid = 5566;
s2.sname   = "Kumar";
s2.email   = "Kumar@gmail.com";

s1.showDetails();
s2.showDetails();
export{};