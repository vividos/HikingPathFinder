using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    /// <summary>
    /// All UI tests for the app
    /// </summary>
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        /// <summary>
        /// Platform the test is running under
        /// </summary>
        private readonly Platform platform;

        /// <summary>
        /// App under test
        /// </summary>
        private IApp app;

        /// <summary>
        /// Creates a new tests class, specifying the test platform (Android or iOS)
        /// </summary>
        /// <param name="platform">test platform</param>
        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        /// <summary>
        /// Sets up testing
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.app = AppInitializer.StartApp(this.platform);
        }

        /// <summary>
        /// Tests app launch by : take a screenshot of the main interface
        /// </summary>
        [Test]
        public void TestAppLaunch()
        {
            this.app.WaitForElement(
                "Hiking Path Finder",
                "failed waiting for main screen",
                TimeSpan.FromSeconds(10));

            this.app.Screenshot("App main page");
        }
    }
}
