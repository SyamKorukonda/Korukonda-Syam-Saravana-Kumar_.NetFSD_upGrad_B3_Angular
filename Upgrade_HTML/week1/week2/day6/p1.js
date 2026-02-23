let savedNote=localStorage.getItem("dailynote");
if(savedNote !==null){
    document.getElementById("notebox").value=savedNote;
}

function savenote(){
    let note=document.getElementById("notebox").value;
    localStorage.setItem("dailynote",note);

    alert("note saved sucessfully");
}

function clearnote(){
    localStorage.removeItem("dailynote");

    document.getElementById("notebox").value = "";

    alert("Note cleared!");
}