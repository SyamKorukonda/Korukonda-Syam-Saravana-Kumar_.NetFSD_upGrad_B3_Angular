// Example to demonstrate usage of constructor
class Student
{
    // Props
    public sid:number  =  0;
    public sname:string   = "";
    public email:string  = "";

    //constructor(sid:number, sname?:string, email?:string)
    constructor(sid:number, sname:string = "", email:string = "")
    {
           this.sid =  sid; 
           this.sname  = sname;
           this.email  = email;
    }

    // Methods
    public showDetails():void
    {
        console.log("Student Id : "  +  this.sid);
        console.log("Student Name : "  +  this.sname);
        console.log("Email Id : "  +  this.email);
    }
}

 
var s1   = new Student(4455, "saravana","saravana@gmail.com"); 
var s2  = new Student(5566, "kumar"); 
var s3  = new Student(85454); 

s1.showDetails();
console.log("--------------------------");
s2.showDetails();
console.log("--------------------------");

s3.showDetails();

export{};