import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ThemeService {

  private readonly THEME_KEY = 'shopez_theme';

  // BehaviorSubject — holds dark mode state, persists across components
  
  private isDarkMode$ = new BehaviorSubject<boolean>(
    localStorage.getItem(this.THEME_KEY) === 'dark'
  );

  // Expose as Observable — all components subscribe to theme changes
  
  isDarkMode = this.isDarkMode$.asObservable();

  constructor() {
    // Apply saved theme on service startup

    this.applyTheme(this.isDarkMode$.value);
  }

  // Toggle Light ↔ Dark — called from Navbar button

  toggle(): void {
    const newVal = !this.isDarkMode$.value;
    this.isDarkMode$.next(newVal);
    localStorage.setItem(this.THEME_KEY, newVal ? 'dark' : 'light');
    this.applyTheme(newVal);
  }

  // Apply class to <body> — CSS variables switch based on body class

  private applyTheme(isDark: boolean): void {
    document.body.classList.toggle('dark-mode', isDark);
    document.body.classList.toggle('light-mode', !isDark);
  }

  get isDark(): boolean {
    return this.isDarkMode$.value;
  }
}
