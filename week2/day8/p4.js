const input=document.getElementById("taskInput");
const addbt=document.getElementById("addBtn");
const list=document.getElementById("taskList");

addbt.addEventListener("click", ()=>{
    let text=input.value.trim();
    if(text ==="")
        return;
    let li=document.createElement("li");
    li.innerHTML=`
                    <span>${text}</span> 
                   <button class="complete">Complete </button>
                   <button class="delete"> Delete </button>
                   `;
                   /* <span> is s a small inline HTML tag used to hold text or a small part of content.
                   inline (same line)*/ 
    list.appendChild(li);
    input.value="";

});

list.addEventListener("click", (e)=> {

        if(e.target.classList.contains("delete")){
                e.target.parentElement.remove();
            }
        
        if(e.target.classList.contains("complete")){
        e.target.parentElement.querySelector("span")//
                .classList.toggle("done"); // if present -> add,if absent -> remove
        }

});
