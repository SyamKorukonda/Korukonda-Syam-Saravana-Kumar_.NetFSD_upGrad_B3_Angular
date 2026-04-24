// Getters and Setters
// Encapsulate logic for accessing or modifying properties.

class Person
{
    private _name:string="";
    private _age:number=0;

    get name():string{
        return this._name;
    }
    
    set name(value:string){       //void method 
        this._name=value;
    }

    get age():number{
        return this._age;
    }

    set age(value:number){
        this._age=value;
    }
}

let p1 = new Person();
p1.name = "Syam Saravan";     // set
p1.age = 21;        //  set 

console.log(p1.name);   // get
console.log(p1.age);   // get 
