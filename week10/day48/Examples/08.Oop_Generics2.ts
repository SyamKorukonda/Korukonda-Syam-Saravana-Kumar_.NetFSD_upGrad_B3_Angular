interface Box<T> {
    value:T;
    getValue():T;
}

class StringBox implements Box<string>{
    constructor(public value:string){}

    getValue(): string {
        return this.value;
    }
}

class NumberBox implements Box<number>{

    constructor(public value:number){}

    getValue(): number {
        return this.value;
    }
}

let ob1=new StringBox("syam");
let ob2=new NumberBox(21);

// console.log(ob1.value);   // same output as getValue()
// console.log(ob2.value);

console.log(ob1.getValue())
console.log(ob2.getValue());