// GLOBAL VARIABLE (stored outside functions)
var counter = 0;


// function to update UI
function updateDisplay(){
    document.getElementById("count").innerHTML = counter;
}


// function with parameter
function incrementCounter(step){
    counter = counter + step;   // using parameter
    updateDisplay();
}


// reset function
function resetCounter(){
    counter = 0;
    updateDisplay();
}


// attach events (NO inline JS rule)
document.getElementById("incBtn").addEventListener("click", function(){
    incrementCounter(1);  // step value passed
});

document.getElementById("resetBtn").addEventListener("click", resetCounter);

// initial display
updateDisplay();