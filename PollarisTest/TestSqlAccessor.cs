using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Pollaris._3.Accessors;
using Pollaris.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitPollarisTest
{
    public class TestSqlAccessor
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
            string email = "ellennamation@gmail.com";
            bool expected = true;
            bool actual = sql.CheckIfEmailInDatabase(email); 
            Assert.AreEqual(expected, actual);
        }

        [Test] 
        public void TestEmailInDatabaseFalse()
        {
            string email = "apuhgrapeibnrgapinbpiuawns@gmail.com";
            bool expected = false;
            bool actual = sql.CheckIfEmailInDatabase(email);
            Assert.AreEqual(expected, actual);
        }
    }
}