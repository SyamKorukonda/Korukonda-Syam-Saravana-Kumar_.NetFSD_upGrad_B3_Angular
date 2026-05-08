import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  BehaviorSubject,
  Subject,
  debounceTime,
  distinctUntilChanged,
  map,
  takeUntil
} from 'rxjs';
import { AuthService } from '../../../services/auth.service';
import { User } from '../../../models/user.model';

@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent implements OnInit, OnDestroy {

  // Full user list

  allUsers: User[] = [];

  // Filtered users for UI

  filteredUsers: User[] = [];

  // UI state

  loading = true;
  searchTerm = '';

  // Search stream with debounce

  private search$ = new BehaviorSubject<string>('');


  // Destroy stream to avoid memory leaks

  private destroy$ = new Subject<void>();

  constructor(private authService: AuthService) { }

  ngOnInit(): void {

    // Fetch users from backend

    this.authService.getAllUsers().subscribe({
      next: users => {
        this.allUsers = users ?? [];
        this.filteredUsers = this.allUsers;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });

    // Handle search with debounce

    this.search$
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        map(term => (term ?? '').trim().toLowerCase()),
        takeUntil(this.destroy$)
      )
      .subscribe(term => {

        // Filter users safely

        this.filteredUsers = this.allUsers.filter(u =>
          (u.userName ?? '').toLowerCase().includes(term) ||
          (u.emailAddress ?? '').toLowerCase().includes(term)
        );
      });
  }

  // Trigger search

  onSearch(term: string): void {
    this.search$.next(term);
  }

  // Clear search input

  clearSearch(): void {
    this.searchTerm = '';
    this.search$.next('');
  }

  // Cleanup subscriptions

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}