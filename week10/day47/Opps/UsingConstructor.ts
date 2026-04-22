class Student
{
    constructor(public sid:number, public sname:string,public email:string)
    {}
    

    // Methods
    public showDetails():void
    {
        console.log("Student Id : "  +  this.sid);
        console.log("Student Name : "  +  this.sname);
        console.log("Email Id : "  +  this.email);
    }
}

 
var s1   = new Student(4455, "Kumar","Kumar@gmail.com"); 
var s2  = new Student(5566, "Saravana","saravana@gmail.com"); 

s1.showDetails();
console.log("--------------------------");
s2.showDetails();
export{};
