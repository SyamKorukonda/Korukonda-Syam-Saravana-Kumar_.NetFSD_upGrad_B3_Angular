import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  //  Reactive form object
  
  form: FormGroup;

  //  UI states

  loading = false;   // shows spinner
  error   = '';      // backend error message
  show    = false;   // toggle password visibility

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {

    // Form initialization with validations

    this.form = this.fb.group({
      email: [
        '',
        [
          Validators.required,  // must enter value
          Validators.email      // must be valid email format
        ]
      ],

      password: [
        '',
        [
          Validators.required,   // required
          Validators.minLength(8) // minimum 8 characters
        ]
      ]
    });
  }

  //  Shortcut for accessing form controls in HTML

  get f() {
    return this.form.controls;
  }

  //  Submit Login Form

  submit(): void {

    //  Prevent API call if form invalid

    if (this.form.invalid) return;

    this.loading = true;
    this.error = '';

    //  Call backend login API

    this.auth.login(this.form.value).subscribe({
      next: () => {
        this.loading = false;

        //  Navigate to home after success

        this.router.navigate(['/home']);
      },

      error: err => {
        this.loading = false;

        //  Show error message from backend

        this.error = err.error?.message ?? 'Login failed.';
      }
    });
  }
}