import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  uname:string ="";
  password:string ="";
  message:string = "";
  count:number = 0;
  isDisabled = false;


  public loginButtonClick():void{

    if(this.uname == "admin" && this.password == "admin123")
    {
      this.message = "Welcome Admin";
    }
    else
    {
     this.message = "Invalid user name or password" ;
     this.count++;

     if(this.count >= 3)
     {
        alert("Only three attempts allowed");
        this.isDisabled = true;
     }
    }

  }
}
