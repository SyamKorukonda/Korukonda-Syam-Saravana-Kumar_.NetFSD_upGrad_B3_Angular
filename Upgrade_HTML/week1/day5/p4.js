// GLOBAL OBJECT (must be global as per requirement)
var student = {
    name: "Syam",
    rollNo: 101,
    marks: 85
};


// Function to display profile
function updateStudentProfile(studentObj){

    document.getElementById("sName").innerHTML =
        "Name: " + studentObj.name;

    document.getElementById("sRoll").innerHTML =
        "Roll No: " + studentObj.rollNo;

    document.getElementById("sMarks").innerHTML =
        "Marks: " + studentObj.marks;
}


// Function to update marks
function updateMarks(newMarks){

    // Update global object value
    student.marks = newMarks;

    // refresh UI
    updateStudentProfile(student);
}


// Button event (NO inline JS)
document.getElementById("updateBtn").addEventListener("click", function(){

    var enteredMarks = document.getElementById("newMarks").value;

    if(enteredMarks !== ""){
        updateMarks(Number(enteredMarks));
    }
});


// Initial display when page loads
updateStudentProfile(student);