class Person{

    constructor(public name:string,public age:number){}
    
    showDetails():void
    {
        console.log(`[ Person Details ]  Name :  ${this.name}, Age :  ${this.age}`);
    }
}

class Employee extends Person{
    constructor(name:string,age:number,public empId:number, public job:string){
        super(name,age);// invoke parent class constructor in child class 
    }

    // Override method
    showDetails(): void {
         console.log(`[ Employee Details ] \n Name :  ${this.name}, Age :  ${this.age}, Employee Id :  ${this.empId}, Job :  ${this.job}`);
    }    
}

let e1 = new Employee("Scott", 35, 546455, "Lead");
let e2 = new Employee("Smith", 25, 646455, "Tester");
e1.showDetails();
e2.showDetails();