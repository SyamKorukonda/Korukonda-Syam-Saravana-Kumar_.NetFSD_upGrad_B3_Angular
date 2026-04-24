class Employee{

    constructor(public id:number,protected name:string,private _salary:number){}

    public getSalary():number{
        return this._salary;
    }

    public setSalary(value:number){ // not mentioned   :void ,because it id default void
        if(value>0){
            this._salary=value;
        }
        else{
            console.log("invalid value to set the salary");
        }
    }

    public diaplayDetails(){
        console.log(`Employee ID: ${this.id}`);
        console.log(`Name: ${this.name}`);
        console.log(`Salary: ${this._salary}`);
    }
}

class Manager extends Employee{

    constructor(id:number,name:string,salary:number,public teamSize:number){
        super(id,name,salary);
    }

     public displayDetails(): void {
        console.log("Manager Details:");
        console.log(`Employee ID: ${this.id}`);
        console.log(`Name: ${this.name}`); // protected accessible
        console.log(`Salary: ${this.getSalary()}`); // private via getter
        console.log(`Team Size: ${this.teamSize}`);
  }
} 

const emp1=new Employee(1,"syam",55000);
emp1.diaplayDetails();

emp1.setSalary(85000);
console.log("Updated Salary:", emp1.getSalary());
console.log("----------------------");

const mng=new Manager(2,"Saravana",85000,5);
mng.displayDetails();