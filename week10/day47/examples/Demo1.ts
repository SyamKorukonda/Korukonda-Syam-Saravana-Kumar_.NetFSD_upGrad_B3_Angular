class Employee
{
    constructor(public name:string,public salary:number ){} //create + assign
                                                    //name: string -> only parameter (no property)
                                                    // this.name=name   this.salary=salary

    getDetails()
    {
        return `${this.name} earns ${this.salary} `;
    }
}

class Manager extends Employee
{
    constructor(name:string,salary:number,public teamsize:number)
    {
        super(name,salary);
    }
    getDetails()
    {
        return `${super.getDetails()} and manages ${this.teamsize} people`;
    }
        
}

const m=new Manager("syam",85000,5);
console.log(m.getDetails());
export {};
