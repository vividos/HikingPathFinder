using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.UITest;

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

            this.app.WaitForElement(
                "Hiking Path Finder",
                "failed waiting for start page",
                TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Taps hamburger menu button
        /// </summary>
        public void TapMenuButton()
        {
            this.app.Tap(x => x.Class("ImageButton").Marked("OK"));
        }

        /// <summary>
        /// Tests app launch by : take a screenshot of the main interface
        /// </summary>
        [Test]
        public void TestAppLaunch()
        {
            this.app.Screenshot("App main page");
        }

        /// <summary>
        /// Tests "plan tour" page
        /// </summary>
        [Test]
        public void TestPlanRoutePage()
        {
            this.TapMenuButton();

            this.app.Tap(x => x.Marked("Plan Tour"));

            this.app.Screenshot("Plan Tour page");
        }

        /// <summary>
        /// Tests "explore map" page
        /// </summary>
        [Test]
        public void TestExploreMapPage()
        {
            this.TapMenuButton();

            this.app.Tap(x => x.Marked("Explore Map"));

            this.app.Screenshot("Explore Map page");
        }

        /// <summary>
        /// Tests settings page
        /// </summary>
        [Test]
        public void TestSettingsPage()
        {
            this.TapMenuButton();

            this.app.Tap(x => x.Marked("Settings"));

            this.app.Screenshot("Settings page");
        }

        /// <summary>
        /// Tests about page
        /// </summary>
        [Test]
        public void TestAboutPage()
        {
            this.TapMenuButton();

            this.app.Tap(x => x.Marked("About"));

            this.app.Screenshot("About page");

            var buttonResult = this.app.Query(x => x.Marked("AboutVisitHomepageButton"));
            Assert.IsTrue(buttonResult.Any(), "homepage button element must be found");

            var versionLabelResult = this.app.Query(x => x.Marked("AboutVersionNumber"));
            Assert.IsTrue(versionLabelResult.Any(), "version number element must be found");

            Debug.WriteLine("App version is: " + versionLabelResult[0].Label);
        }
    }
}
