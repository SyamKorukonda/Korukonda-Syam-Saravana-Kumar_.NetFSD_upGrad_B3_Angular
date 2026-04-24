class Person{    

    // Parameter Properties (Shorthand)
    // Declare and initialize class members directly in the constructor.
    constructor(public name:string, public  age:number) {       
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
