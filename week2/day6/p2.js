function validatename(){
    let name=document.getElementById("name").value;
    if(name.trim === ""){
        document.getElementById("namemsg").innerText="Name cannot be empty ";
        return false;
    }
    else{
        document.getElementById("namemsg").innerText="valid name";
        return true;
    }

}

function validateemail(){
    let email=document.getElementById("email").value;
    if(email.includes("@")){
        document.getElementById("emailmsg").innerText="valid email";
        return true;
    }
    else{
        document.getElementById("emailmsg").innerText=" Not valid email";
        return false;
    }
}

function validateage(){
    let age=document.getElementById("age").value;
    if(age >18 ){
        document.getElementById("agemsg").textContent = " Age accepted";
        return true;
    }
    else{
        document.getElementById("agemsg").textContent = "Age not accepted";
        return false;
    }

}

function registration(){
    let isvalidname=validatename();
    let isvalidemail=validateemail();
    let isvalidage=validateage();

    if(isvalidname&&isvalidemail&&isvalidage){
        let user={
            name:document.getElementById("name").value,
            email:document.getElementById("email").value,
            age:document.getElementById("age").value

        };
        sessionStorage.setItem("userData", JSON.stringify(user));
        alert("Registration Successful! Data stored in sessionStorage");
    }else{
        alert("Please correct the errors before registering");
    }

}