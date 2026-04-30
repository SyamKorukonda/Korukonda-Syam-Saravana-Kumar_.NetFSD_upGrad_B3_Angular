import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute,Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';
import { AuthServices } from '../services/auth-services';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  uname:string="admin";
  password:string="123";
  message:string="";

  constructor(private router:Router,private route:ActivatedRoute,
        private authService:AuthServices,private changeDetectorRef:ChangeDetectorRef){}

  public loginButtonClick():void{

    //  We suppose to get the token from server- api ( for the example purpose we take this from google)
    //const token = "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ";

    let str=this.route.snapshot.queryParams["rerurnUrl"]; // which mathod you select  it gives the its url

    if(str==null ||!str)
      {
          str="/";  
      }


      this.authService.checkUserCredentails(this.uname,this.password).subscribe(
        {
          next:(res:any)=>
          {
            sessionStorage.setItem("AUTH_TOKEN",res.token);
            this.router.navigate([str]);
          },
          error:(err:any)=>
          {
            this.message="Invalid Credentials";
            this.changeDetectorRef.detectChanges();
          }
        }
      );


  }

}
