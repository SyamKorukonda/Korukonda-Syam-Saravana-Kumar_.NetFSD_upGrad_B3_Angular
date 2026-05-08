// Karma configuration file
// Run tests with: ng test

module.exports = function (config) {
  config.set({
    // Base path used to resolve all patterns
    basePath: '',

    // Testing frameworks — Jasmine + Angular
    frameworks: ['jasmine', '@angular-devkit/build-angular'],

    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'),
      require('karma-jasmine-html-reporter'),
      require('karma-coverage'),
      require('@angular-devkit/build-angular/plugins/karma')
    ],

    client: {
      jasmine: {
        // Can add Jasmine options here
      },
      clearContext: false // Leave Jasmine Spec Runner output visible
    },

    // Coverage reporter
    jasmineHtmlReporter: {
      suppressAll: true // removes the duplicated traces
    },

    coverageReporter: {
      dir: require('path').join(__dirname, './coverage/shopez-angular'),
      subdir: '.',
      reporters: [
        { type: 'html' },   // HTML report
        { type: 'text-summary' }, // Console summary
        { type: 'lcovonly' } // For CI/CD
      ]
    },

    reporters: ['progress', 'kjhtml'],

    // Web server port
    port: 9876,

    // Enable colors in output
    colors: true,

    // Log level
    logLevel: config.LOG_INFO,

    // Watch files and run tests on change
    autoWatch: true,

    // Browsers to launch
    browsers: ['Chrome'],

    // Run once and exit (CI mode: ng test --watch=false)
    singleRun: false,

    // Restart on file change
    restartOnFileChange: true
  });
};