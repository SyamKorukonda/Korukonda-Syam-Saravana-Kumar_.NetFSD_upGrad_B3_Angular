class Student
{
    public sid:number=786;
    public sname:string="Syam";
    public email:string="syam@gmail.com";

    public showDetails():void
    {
        console.log("student id=" +this.sid);
        console.log("Student Name ="+this.sname);
        console.log("Email="+this.email);
    }
}

var stObj:Student=new Student();
stObj.showDetails();

