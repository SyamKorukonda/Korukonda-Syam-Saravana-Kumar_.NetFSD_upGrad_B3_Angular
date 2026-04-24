// Enumerations
enum Status {
  Success = 200,
  NotFound = 404,
  Error = 500
}

enum Role {
  Admin = "ADMIN",
  User = "USER"
}


let userRole = "Admin";

if(Role.Admin == userRole.toUpperCase()) {
    console.log("You can perform Advnaced operations");
}
else{
     console.log("You can perform Basic operations");
}


console.log("--------------------------");


let serverResposne = { status : 200, message : "Request process successfully at server" };

if(serverResposne.status == Status.Success)
{
    console.log("Success Message :" + serverResposne.message);
}


console.log("--------------------------");




// Union
function printId(id: string | number) {
  if (typeof id === "string") {
        console.log(id.toUpperCase());
  } else {
        console.log(id.toFixed(2));
  }
}


printId(123454);
printId("abn565654");

