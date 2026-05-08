import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',

  // Standalone component (no NgModule required)
  standalone: true,

  // Required Angular modules for this component
  imports: [CommonModule, ReactiveFormsModule, RouterModule],

  // External HTML file
  templateUrl: './register.component.html',

  // External CSS file
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  // Reactive form object
  form: FormGroup;

  // UI states
  loading = false; // spinner state
  error   = '';    // error message
  success = '';    // success message

  // Toggle password visibility
  show = false;

  constructor(
    private fb: FormBuilder,   // used to create reactive form
    private auth: AuthService, // API service
    private router: Router     // navigation
  ) {

    // Form initialization with validations
    this.form = this.fb.group({

      // Name: required, min 3 chars, max 100 chars
      name:     ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],

      // Email: required and valid email format
      email:    ['', [Validators.required, Validators.email]],

      // Password: minimum 8 characters
      password: ['', [Validators.required, Validators.minLength(8)]],

      // Role is fixed (not shown in UI)
      role:     ['Customer']
    });
  }

  // Shortcut to access form controls
  get f() {
    return this.form.controls;
  }

  // Submit form
  submit(): void {

    // Stop if form is invalid
    if (this.form.invalid) return;

    // Set loading state
    this.loading = true;
    this.error   = '';
    this.success = '';

    // Call register API
    this.auth.register(this.form.value).subscribe({

      // Success case
      next: () => {
        this.loading = false;

        // Show success message
        this.success = 'Account created successfully! Redirecting to login...';

        // Redirect after delay
        setTimeout(() => this.router.navigate(['/login']), 1500);
      },

      // Error case
      error: err => {
        this.loading = false;

        // Show backend error or fallback message
        this.error = err.error?.message ?? 'Registration failed. Please try again.';
      }
    });
  }
}