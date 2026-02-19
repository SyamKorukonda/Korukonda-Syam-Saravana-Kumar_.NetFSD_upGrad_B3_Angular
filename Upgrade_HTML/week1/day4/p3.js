function bill(){

let amount = Number(document.getElementById("amt").value);

let discount = 0;
let finalAmount = 0;

if(amount >= 5000){
    discount = amount * 0.20;
}
else if(amount >= 3000){
    discount = amount * 0.10;
}
else{
    discount = 0;
}

finalAmount = amount - discount;

document.getElementById("res").innerHTML =
"Purchase Amount: ₹" + amount + "<br>" +
"Discount: ₹" + discount + "<br>" +
"Final Payable Amount: ₹" + finalAmount;

}
