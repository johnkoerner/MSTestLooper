MSTestLooper
============

Provides the ability to run MSTest test cases multiple times, passing a parameter to the unit test method.  This can be combined with data driven tests to provide a great number of permutations of tests.

The reason for this project is that I had suites of tests that I wanted to run mutiple times with different parameters.  Since some of these tests are already data driven, I didn't want to have to build permutations into every test.  The TestLooper project lets me run all of my tests multiple times, passing the selenium web driver to the test as a parameter, allowing me to run all of my tests (even my data driven tests) against multiple web browsers.

This project assumes you are using VS 2012.  You will need to run the RegistySetup.reg file on your machine to put the proper info in the registry to allow the Looper to be used in your projects.

Once you have all the setup work in place, you can attribute your test class with the `TestLooper` attribute.

    [TestLooper()]
    public class SeleniumTests
    {
	   //....
	}

And in your test initialize, set the Looper property:

	[TestInitialize()]
	public void TestInit()
	{
		if (!TestContext.Properties.Contains(TestLooperAttribute.LOOPER_CONTEXT_PROPERTY))
			testContextInstance.Properties.Add(TestLooperAttribute.LOOPER_CONTEXT_PROPERTY, Browsers);
	}
	

	
	
	