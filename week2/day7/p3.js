    const employees = [
    { id: 101, name: "Syam", job: "Manager", salary: 68000 },
    { id: 102, name: "Ravi", job: "Salesman", salary: 30000 },
    { id: 103, name: "Kiran", job: "Clerk", salary: 25000 },
    { id: 104, name: "Anu", job: "Salesman", salary: 32000 },
    { id: 105, name: "Hari", job: "Manager", salary: 65000 }
];

function showEmployees(){

    const selectjob=document.getElementById("jobselect").value ;

    const tablebody=document.querySelector("#emptable tbody"); 
    /* element with id = empTable (the table)  tbody => tbody inside that table
    selects the first element that matches a CSS selector */

    tablebody.innerHTML="";
    let result;

    if(selectjob=== "All"){
        result=employees;
    }
    else{
        result=employees.filter(emp =>emp.job===selectjob);
    }
    
    result.forEach(emp => {
        let row=`
         <tr>
            <td>${emp.id}</td>
            <td>${emp.name}</td>
            <td>${emp.job}</td>
            <td>${emp.salary}</td>
        </tr>
        `;
        tablebody.innerHTML +=row;
    });


}