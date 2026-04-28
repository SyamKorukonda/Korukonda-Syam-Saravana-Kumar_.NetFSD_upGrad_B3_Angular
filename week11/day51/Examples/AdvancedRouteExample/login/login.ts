import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute,Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  uname:string="admin";
  password:string="admin123";
  message:string="";

  constructor(private router:Router,private route:ActivatedRoute){}

  public loginButtonClick():void{
    //  We suppose to get the token from server- api ( for the example purpose we take this from google)
    const token = "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ";

    let str=this.route.snapshot.queryParams["rerurnUrl"]; // which mathod you select  it gives the its url

    if(str==null ||!str){
      str="/";
    }

    if(this.uname=="admin" && this.password=="admin123"){
      sessionStorage.setItem("AUTH_TOKEN",token); // it set AUTH_TOKEN as above given token
      this.router.navigate([str]); // it navigate to that url
    }
    else{
      this.message="Invalid USer Name Or Password";
    }


  }

}
