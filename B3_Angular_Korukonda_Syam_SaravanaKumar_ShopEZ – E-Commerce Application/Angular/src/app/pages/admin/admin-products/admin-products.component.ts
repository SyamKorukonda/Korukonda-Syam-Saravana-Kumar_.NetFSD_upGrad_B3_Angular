import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product.model';

@Component({
  selector: 'app-admin-products',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-products.component.html',
  styleUrls: ['./admin-products.component.css']
})
export class AdminProductsComponent implements OnInit {

  // Stream of products from service

  products$!: Observable<Product[]>;

  // Loading state observable

  loading$!: Observable<boolean>;

  // Reactive form for create/edit

  form!: FormGroup;

  // UI states

  showForm = false;
  saving = false;
  editingId: number | null = null;

  // Messages

  error = '';
  success = '';

  constructor(
    private ps: ProductService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {

    // Bind observables
    this.products$ = this.ps.products;
    this.loading$ = this.ps.loading;

    // Fetch products from backend

    this.ps.getAll().subscribe();

    // Initialize form

    this.buildForm();
  }

  // Build form with validation rules

  buildForm(p?: Product): void {
    this.form = this.fb.group({

      // Product name required with minimum length

      name: [
        p?.name ?? '',
        [Validators.required, Validators.minLength(3)]
      ],

      // Description required

      description: [
        p?.description ?? '',
        [Validators.required]
      ],

      // Price must be greater than zero

      price: [
        p?.price ?? null,
        [Validators.required, Validators.min(0.01)]
      ],

      // Stock must be zero or more

      stock: [
        p?.stock ?? null,
        [Validators.required, Validators.min(0)]
      ],

      // Category required
      category: [
        p?.category ?? '',
        [Validators.required]
      ],

      // Optional image
      imageUrl: [p?.imageUrl ?? '']
    });
  }

  // Open form for creating new product

  openForm(): void {
    this.editingId = null;
    this.error = '';
    this.success = '';
    this.buildForm();
    this.showForm = true;
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  // Open form for editing existing product

  edit(p: Product): void {
    if (!p || !p.productId) return;

    this.editingId = p.productId;
    this.error = '';
    this.success = '';
    this.buildForm(p);
    this.showForm = true;
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  // Cancel form and reset state

  cancelForm(): void {
    this.showForm = false;
    this.editingId = null;
    this.error = '';
    this.success = '';
  }

  // Submit form (create or update)

  submit(): void {

    // Prevent submission if form is invalid

    if (this.form.invalid) return;

    this.saving = true;
    this.error = '';
    this.success = '';

    const dto = this.form.value;

    // If editing → update product

    if (this.editingId) {

      this.ps.update(this.editingId, dto).subscribe({
        next: () => {
          this.saving = false;
          this.success = 'Product updated successfully';

          // Reset UI after success

          setTimeout(() => {
            this.cancelForm();
            this.form.reset();
          }, 1500);
        },
        error: err => {
          this.saving = false;
          this.error = err.error?.message ?? 'Update failed';
        }
      });

    } else {

      // If creating → add product

      this.ps.create(dto).subscribe({
        next: () => {
          this.saving = false;
          this.success = 'Product created successfully';

          // Reset UI after success

          setTimeout(() => {
            this.cancelForm();
            this.form.reset();
          }, 1500);
        },
        error: err => {
          this.saving = false;
          this.error = err.error?.message ?? 'Create failed';
        }
      });

    }
  }

  // Delete product with validation

  delete(id: number): void {

    // Prevent invalid delete

    if (!id) return;

    // Confirm before delete

    if (!confirm('Delete this product? This action cannot be undone.')) return;

    this.ps.delete(id).subscribe();
  }

  // Handle broken image fallback

  onImgErr(e: Event): void {
    (e.target as HTMLImageElement).src = 'assets/images/no-image.png';
  }
}