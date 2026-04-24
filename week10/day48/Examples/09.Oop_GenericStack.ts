class Stack<T>{
    private items:T[]=[];

    push(item:T):void{
        this.items.push(item);
    }

    pop():T | undefined{
       return  this.items.pop();
    }

    peek():T | undefined{
       return this.items[this.items.length-1];
    }
}

const numberStack = new Stack<number>();
numberStack.push(10);
numberStack.push(20);
numberStack.push(30);
numberStack.push(40);

console.log(numberStack);

console.log(numberStack.peek()); // 40

console.log(numberStack.pop()); // 40
console.log(numberStack.pop()); // 30
console.log(numberStack.pop()); // 20
console.log(numberStack.pop()); // 10
console.log(numberStack.pop()); // undefined