﻿using NUnit.Framework;
using System.Diagnostics;
using Xamarin.Forms;

namespace HikingPathFinder.App.UnitTest
{
    /// <summary>
    /// Tests for Database classes
    /// </summary>
    [TestFixture]
    public class TestDatabase
    {
        /// <summary>
        /// Tests default ctor of Database class
        /// </summary>
        [Test]
        public void TestDefaultCtor()
        {
            var platform = DependencyService.Get<IPlatform>();

            string databaseFilename = platform.PathCombine(platform.AppDataFolder, "newdatabase.db");

            using (var database = new HikingPathFinder.App.Database.Database(databaseFilename))
            {
                var connection = database.GetConnection();

                int libVersionNumber = connection.Platform.SQLiteApi.LibVersionNumber();

                Debug.WriteLine("LibVersionNumber = {0}", libVersionNumber);
            }
        }
    }
}
