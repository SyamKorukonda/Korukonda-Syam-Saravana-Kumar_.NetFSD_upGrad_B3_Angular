// Function triggered by button
function showData() {

    // OBJECT (data stored together)
    var user = {
        name: "Syam",
        age: 21,
        city: "Sattenapalle"
    };

    // pass object to another function
    displayUserInfo(user);
}


// function receives OBJECT as parameter
function displayUserInfo(userObj) {

    // Access using DOT NOTATION
    document.getElementById("uName").innerHTML =
        "Name: " + userObj.name;

    document.getElementById("uAge").innerHTML =
        "Age: " + userObj.age;

    document.getElementById("uCity").innerHTML =
        "City: " + userObj.city;
}