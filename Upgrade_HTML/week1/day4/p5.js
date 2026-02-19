function analyze(){

let n = Number(document.getElementById("num").value);

let pn = (n >= 0) ? "Positive Number" : "Negative Number";

let eo;

if(n % 2 === 0){
    eo = "Even Number";
}
else{
    eo = "Odd Number";
}

let numbers = "";

for(let i = 1; i <= n; i++){
    numbers += i + " ";
}

document.getElementById("res").innerHTML =
"Type: " + pn + "<br>" +
"Parity: " + eo;

document.getElementById("list").innerText =
"Numbers from 1 to " + n + " : " + numbers;

}
