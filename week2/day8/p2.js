// array (database)
let tasks = [];

// async save
const saveTask = (t) => {
    return new Promise(resolve => {
        setTimeout(() => {
            tasks.push(t);   // push into array
            resolve();
        }, 1000);
    });
};

// called from HTML button
async function addTask() {
    let t = document.getElementById("task").value;

    if(t === "") {
        alert("Enter a task");
        return;
    }

    await saveTask(t);
    alert("Task Added!");

    document.getElementById("task").value = "";
}

// show tasks
function showTasks() {
    let ul = document.getElementById("list");
    ul.innerHTML = "";

    tasks.forEach(item => {
        ul.innerHTML += `<li>${item}</li>`;
    });
}