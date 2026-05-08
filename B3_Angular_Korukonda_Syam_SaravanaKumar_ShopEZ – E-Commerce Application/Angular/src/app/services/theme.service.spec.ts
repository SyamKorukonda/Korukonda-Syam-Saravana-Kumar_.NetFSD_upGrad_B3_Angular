import { TestBed } from '@angular/core/testing';
import { ThemeService } from '../services/theme.service';

describe('ThemeService', () => {
  let service: ThemeService;

  //  Setup before each test 
  beforeEach(() => {

    // Clear previous theme data
    localStorage.clear();

    // Remove any existing classes from body
    document.body.classList.remove('dark-mode', 'light-mode');

    // Configure testing module
    TestBed.configureTestingModule({
      providers: [ThemeService]
    });

    // Inject service
    service = TestBed.inject(ThemeService);
  });

  //  Cleanup after each test 
  afterEach(() => {
    localStorage.clear();
    document.body.classList.remove('dark-mode', 'light-mode');
  });

  //  Test: Service creation 
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  //  Test: Default state (light mode) 
  it('should default to light mode', (done) => {

    // Subscribe to observable
    service.isDarkMode.subscribe(isDark => {

      // Expect false (light mode)
      expect(isDark).toBeFalse();

      done();
    });
  });

  //  Test: Getter method 
  it('isDark getter should return false initially', () => {

    // Direct getter check
    expect(service.isDark).toBeFalse();
  });

  //  Test: DOM class applied 
  it('should apply light-mode class to body on init', () => {

    // Check body class
    expect(
      document.body.classList.contains('light-mode')
    ).toBeTrue();
  });

  //  Test: Toggle functionality 
  it('should switch to dark mode on toggle()', (done) => {

    // Trigger toggle
    service.toggle();

    // Verify observable update
    service.isDarkMode.subscribe(isDark => {
      expect(isDark).toBeTrue();
      done();
    });
  });

  it('should switch back to light on second toggle()', (done) => {

    // Toggle twice
    service.toggle(); // → dark
    service.toggle(); // → light

    service.isDarkMode.subscribe(isDark => {
      expect(isDark).toBeFalse();
      done();
    });
  });

  //  Test: Getter after toggle 
  it('isDark getter returns true after toggle', () => {

    service.toggle();

    expect(service.isDark).toBeTrue();
  });

  //  Test: DOM update on toggle 
  it('should add dark-mode class to body after toggle', () => {

    service.toggle();

    expect(
      document.body.classList.contains('dark-mode')
    ).toBeTrue();
  });

  it('should remove light-mode class after toggle to dark', () => {

    service.toggle();

    expect(
      document.body.classList.contains('light-mode')
    ).toBeFalse();
  });

  it('should restore light-mode class after two toggles', () => {

    service.toggle();
    service.toggle();

    expect(
      document.body.classList.contains('light-mode')
    ).toBeTrue();
  });

  //  Test: localStorage persistence 
  it('should save "dark" in localStorage after toggle', () => {

    service.toggle();

    expect(
      localStorage.getItem('shopez_theme')
    ).toBe('dark');
  });

  it('should save "light" in localStorage after two toggles', () => {

    service.toggle();
    service.toggle();

    expect(
      localStorage.getItem('shopez_theme')
    ).toBe('light');
  });

  //  Test: Restore state from localStorage 
  it('should restore dark mode from localStorage on init', () => {

    // Set stored value
    localStorage.setItem('shopez_theme', 'dark');

    // Create fresh instance
    const freshService = new ThemeService();

    expect(freshService.isDark).toBeTrue();
  });

  it('should apply dark-mode class when localStorage is dark', () => {

    // Set stored value
    localStorage.setItem('shopez_theme', 'dark');

    // Initialize service → applies theme
    new ThemeService();

    expect(
      document.body.classList.contains('dark-mode')
    ).toBeTrue();
  });
});