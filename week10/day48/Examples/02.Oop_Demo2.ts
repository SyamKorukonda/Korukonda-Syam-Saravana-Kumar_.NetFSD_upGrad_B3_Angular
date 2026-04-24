class Person{
    public name:string;
    public age:number;

    // Multiple constructor implementations are not allowed.
     // constructor(){}

    constructor(name:string, age:number)
    {
        this.name = name;
        this.age = age;
    }

    showDetails():void
    {
        console.log(`[ Person Details ]  Name :  ${this.name}, Age :  ${this.age}`);
    }
}


 let p1 = new Person("Syam", 20);
 let p2 = new Person("Saravana", 21);
 let p3 = new Person("Kumar", 22);
 
p1.showDetails();
p2.showDetails();
p3.showDetails();
