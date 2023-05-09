using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Pollaris._3.Accessors;
using Pollaris.Controllers;
using Pollaris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitPollarisTest
{
    internal class TestSqlAccessor
    {
        private static string connectionString = "Data Source=tcp:pollarissql.database.windows.net,1433;Initial Catalog=Pollaris;User Id=sqladmin;Password=iajdfij#29dfkjb(fj;";
        private SqlConnection connection;
        private SQLAccessor sql;

        [SetUp]
        public void Setup()
        {
            connection = new SqlConnection(connectionString);
            sql = new SQLAccessor();
        }

        [Test]
        public void TestEmailInDatabaseTrue()
        {
            //Setup
            string email = "ellennamation@gmail.com";
            //Execute
            bool actual = sql.CheckIfEmailInDatabase(email);
            //Test
            Assert.IsTrue(actual);
        }

        [Test]
        public void TestEmailInDatabaseFalse()
        {
            //Setup
            string email = "apuhgrapeibnrgapinbpiuawns@gmail.com";
            //Execute
            bool actual = sql.CheckIfEmailInDatabase(email);
            //Test
            Assert.IsFalse(actual);
        }

        [Test]
        public void TestGetUserIdFromEmailValid()
        {
            //Setup
            string email = "ellennamation@gmail.com";
            int expected = 32;
            //Execute
            int actual = sql.GetUserIdFromEmail(email);
            bool result = actual == expected;
            //Test
            Assert.IsTrue(result);
        }

        [Test]
        public void TestChangePasswordWithValidInputs()
        {
            //Setup
            int userId = 32;
            string newPassword = "wassup";
            //Execute
            bool result = sql.ChangePassword(userId, newPassword);
            //Test
            Assert.IsTrue(result); 
        }

        [Test]
        public void TestChangePasswordInvalidUserId()
        {
            //Setup
            int userId = 0;
            string newPassword = "hi";
            //Execute
            bool result = sql.ChangePassword(userId, newPassword);
            //Test
            Assert.IsFalse(result); 
        }

        [Test]
        public void TestGetUsersFromIdsValidIdsId0()
        {
            //Setup
            List<int> ids = new List<int> { 31, 32 };
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(31, "Tan", "Phan", ""));
            expected.Add(new UserInfo(32, "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[0].Id, Is.EqualTo(expected[0].Id));
        }

        [Test]
        public void TestGetUsersFromIdsValidIdsFirstName0()
        {
            //Setup
            List<int> ids = new List<int> { 31, 32 };
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(31, "Tan", "Phan", ""));
            expected.Add(new UserInfo(32, "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[0].FirstName, Is.EqualTo(expected[0].FirstName));
        }
        [Test]
        public void TestGetUsersFromIdsValidIdsLastName0()
        {
            //Setup
            List<int> ids = new List<int> { 31, 32 };
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(31, "Tan", "Phan", ""));
            expected.Add(new UserInfo(32, "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[0].LastName, Is.EqualTo(expected[0].LastName));
        }
        [Test]
        public void TestGetUsersFromIdsValidIdsPhoto0()
        {
            //Setup
            List<int> ids = new List<int> { 31, 32 };
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(31, "Tan", "Phan", ""));
            expected.Add(new UserInfo(32, "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[0].ProfilePhoto, Is.EqualTo(expected[0].ProfilePhoto));
        }

        [Test]
        public void TestGetUsersFromIdsValidIdsId1()
        {
            //Setup
            List<int> ids = new List<int> { 31, 32 };
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(31, "Tan", "Phan", ""));
            expected.Add(new UserInfo(32, "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[1].Id, Is.EqualTo(expected[1].Id));
        }

        [Test]
        public void TestGetUsersFromIdsValidIdsFirstName1()
        {
            //Setup
            List<int> ids = new List<int> { 31, 32 };
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(31, "Tan", "Phan", ""));
            expected.Add(new UserInfo(32, "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[1].FirstName, Is.EqualTo(expected[1].FirstName));
        }
        [Test]
        public void TestGetUsersFromIdsValidIdsLastName1()
        {
            //Setup
            List<int> ids = new List<int> { 31, 32 };
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(31, "Tan", "Phan", ""));
            expected.Add(new UserInfo(32, "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[1].LastName, Is.EqualTo(expected[1].LastName));
        }
        [Test]
        public void TestGetUsersFromIdsValidIdsPhoto1()
        {
            //Setup
            List<int> ids = new List<int> { 31, 32 };
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(31, "Tan", "Phan", ""));
            expected.Add(new UserInfo(32, "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[1].ProfilePhoto, Is.EqualTo(expected[1].ProfilePhoto));
        }

        [Test] 
        public void TestListToSqlStringHelperFunction()
        {
            //Setup
            List<int> ids = new List<int> { 31, 32 };
            string expected = "('31','32')";
            //Execute
            string actual = sql.ListToSqlString(ids);
            //Test
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test] 
        public void TestGetUsersFromIdsInvalidIdList()
        {
            //Setup
            List<int> ids = new List<int> { 0 };
            int expected = 0; 
            //Execute
            List<UserInfo> result = sql.GetUsersFromIds(ids);
            int actual = result.Count;
            //Test
            Assert.That(actual, Is.EqualTo(expected)); 
        }

        [Test]
        public void TestGetUsersFromIdsEmptyIdList()
        {
            //Setup
            List<int> ids = new List<int>();
            int expected = 0;
            //Execute
            List<UserInfo> result = sql.GetUsersFromIds(ids);
            int actual = result.Count;
            //Test
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestSaveProfileInfoValid()
        {
            //Setup
            int userId = 32;
            string newFirstName = "Ellenna";
            string newLastName = "Divingnzzo";
            //Execute
            bool result = sql.SaveProfileInfo(userId, newFirstName, newLastName);
            //Test
            Assert.IsTrue(result); 
        }

        [Test]
        public void TestSaveProfileInfoInvalid()
        {
            //Setup
            int userId = 0;
            string newFirstName = "Ellenna";
            string newLastName = "Divingnzzo";
            //Execute
            bool result = sql.SaveProfileInfo(userId, newFirstName, newLastName);
            //Test
            Assert.IsFalse(result);
        }

        [Test]
        public void TestGetUserFromIdWhenIdExists()
        {
            // Setup/execute
            int userId = 32;
            UserInfo? user = sql.GetUserFromId(userId);
            string actual = user.FirstName;
            string expected = "Ellenna";
            bool result = String.Equals(actual, expected);

            // Test
            Assert.IsTrue(result, "Did not get the member object correctly based on id.");
        }
    }
}
