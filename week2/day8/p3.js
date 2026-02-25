// get elements
const btn = document.getElementById("toggleBtn");
const body = document.body;


// load saved theme when page opens
let savedTheme = localStorage.getItem("theme");

if(savedTheme){
    body.className = savedTheme;
}


// button click
btn.addEventListener("click", () => {

    // if currently light -> make dark
    if(body.classList.contains("light")){
        body.classList.remove("light");
        body.classList.add("dark");
        localStorage.setItem("theme","dark");
    }
    else{
        body.classList.remove("dark");
        body.classList.add("light");
        localStorage.setItem("theme","light");
    }

});