class Person
{
    public name:string;
    public age:number;

    constructor()
    {
        this.name="syam";
        this.age=21;
    }
    showDetails():void
    {
        console.log(`Person Details = Name: ${this.name} , age: ${this.age}`);
    }
}
 const p1=new Person();
    p1.showDetails();