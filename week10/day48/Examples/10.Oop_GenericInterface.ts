interface Product {
    id: number;
    name: string;
    price: number;
    inStock: boolean;
}



let  p1:Product = {
    id : 54545,
    name : "Laptop",
    price :  56000,
    inStock : true
};



console.log(p1.id);
console.log(p1.name);
console.log(p1.price);
console.log(p1.inStock);
