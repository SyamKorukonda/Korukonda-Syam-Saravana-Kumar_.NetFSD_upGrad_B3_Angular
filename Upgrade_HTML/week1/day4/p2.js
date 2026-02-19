function calculate(){

let x=Number(document.getElementById("marks").value);
let result;
if(x >=75){
    result="A Grade";
}
else if(x >=60){
    result="B Grade";
}
else if(x >=40){
    result="C Grade";
}
else{
    result="Fail";
}
document.getElementById("res").innerText="Result"+result;
}