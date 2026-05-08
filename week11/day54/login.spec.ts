import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Login } from './login';

describe('Login', () => {
  let component: Login;
  let fixture: ComponentFixture<Login>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Login],
    }).compileComponents();

    fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create login component', () => {
    expect(component).toBeTruthy();
  });

  //  Testing  Component Class members   --- properties, methods.

   it('should initialize result property with empty value', () => {
      expect(component.result).toBe("");
      expect(component.uid).toBe("");
  });

  it('should be valid when correct values(uid, pwd) are provdeds', () => {

    component.uid = "admin";
    component.pwd = "admin123";
    component.login_click();
    expect(component.result).toBe("Welcome to Admin");   
});

 it('should be invalid when incorrect values(uid, pwd) are provdeds', () => {

    component.uid = "hello";
    component.pwd = "hello123";
    component.login_click();
    expect(component.result).toBe("Invalid user id or password");   
});


// Testing  Component Template ---   UI  Elements 
 it('should handle user id textbox', () => {

    // fixture.nativeElement.querySelector("#t1");
        
    let  inputsArray:Array<HTMLInputElement> = fixture.nativeElement.querySelectorAll("input");
    inputsArray[0].value = "admin";
    inputsArray[0].dispatchEvent(new Event("input"));
    expect(component.uid).toBe("admin");   
});


// Testing  Component Template ---   UI  Elements 
 it('should generate "Welcome" message for valid credentials', () => {
       
    let  inputsArray:Array<HTMLInputElement> = fixture.nativeElement.querySelectorAll("input");
    inputsArray[0].value = "admin"; // uid textbox
    inputsArray[1].value = "admin123"; // pwd textbox 

    inputsArray[0].dispatchEvent(new Event("input")); // trigger input event 
    inputsArray[1].dispatchEvent(new Event("input")); // trigger input event 

    const button:HTMLButtonElement = fixture.nativeElement.querySelector("button");
    button.dispatchEvent(new Event("click"));

    const para:HTMLParagraphElement = fixture.nativeElement.querySelector("#p1");
    fixture.detectChanges();

    expect(para.textContent).toBe("Welcome to Admin");   
    expect(component.result).toBe("Welcome to Admin");   
});



// Testing  Component Template ---   UI  Elements 
 it('should generate "Invalid" message for invalid credentials', () => {
       
    let  inputsArray:Array<HTMLInputElement> = fixture.nativeElement.querySelectorAll("input");
    inputsArray[0].value = "hello"; // uid textbox
    inputsArray[1].value = "hello123"; // pwd textbox 

    inputsArray[0].dispatchEvent(new Event("input")); // trigger input event 
    inputsArray[1].dispatchEvent(new Event("input")); // trigger input event 

    const button:HTMLButtonElement = fixture.nativeElement.querySelector("button");
    button.dispatchEvent(new Event("click"));

    const para:HTMLParagraphElement = fixture.nativeElement.querySelector("#p1");
    fixture.detectChanges();

   // console.log(para.textContent);

    expect(para.textContent).toBe("Invalid user id or password");   
    expect(component.result).toBe("Invalid user id or password");   
});

});
