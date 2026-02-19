function signal(){

let color = document.getElementById("sgnl").value;
let message;

switch(color){

    case "red":
        message = "Stop";
        break;

    case "yellow":
        message = "Get Ready";
        break;

    case "green":
        message = "Go";
        break;

    default:
        message = "Invalid Signal";
}

document.getElementById("res").innerText = message;
}
