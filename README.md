MSTestLooper
============

Provides the ability to run MSTest test cases multiple times, passing a parameter to the unit test method.  This can be combined with data driven tests to provide a great number of permutations of tests.

The reason for this project is that I had suites of tests that I wanted to run mutiple times with different parameters.  Since some of these tests are already data driven, I didn't want to have to build permutations into every test.  The TestLooper project lets me run all of my tests multiple times, passing the selenium web driver to the test allowing me to run all of my tests (even my data driven tests) against multiple web browsers.