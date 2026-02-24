const marks=[68,85,90,81,88];
const formatmarks=marks.map((marks,index) => `Subject ${index+1}: ${marks}`);
const total=marks.reduce((sum,marks) =>sum+marks,0);   
const avg=total/marks.length;
let result=avg >=40 ?"Pass":"Fail";

const report= `
<h3>Marks Details</h3> ${formatmarks.join("<br>")}
<br></br>
<b>Total Marks:</b> ${total}<br></br>
<b>Average Marks:</b> ${avg.toFixed(2)}<br></br>
<b>Result:</b> ${result} `;

document.getElementById("output").innerHTML=report;