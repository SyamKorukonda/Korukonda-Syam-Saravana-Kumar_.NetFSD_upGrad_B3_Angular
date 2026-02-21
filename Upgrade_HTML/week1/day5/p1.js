// function keyword (as required)
function generateGreeting() {

    // get value from input box
    var name = document.getElementById("username").value;

    // call another function and pass parameter
    showGreeting(name);
}


// function that accepts parameter
function showGreeting(userName) {

    // LOCAL VARIABLE (only inside this function)
    var message = "Hello, " + userName + "! Welcome to our website.";

    // display in <p>
    document.getElementById("greetMsg").innerHTML = message;
}