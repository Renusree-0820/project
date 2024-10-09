using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;
using AdminLoginCode;
//using AdminLoginCode.Controllers;
using System.Linq;
using AdminLoginCode.Controllers;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var context = new FinalCaseStudyEntities())
            {
                var admin = new AdminLogin
                {
                    AdminId = 602,
                    UserName ="Renusree",
                    Password = "@User123",
                };

                // Act
                context.AdminLogins.Add(admin);
                context.SaveChanges();  // This commits the changes to the database

                // Assert - Check if the data was inserted
                var insertedAdmin = context.AdminLogins.SingleOrDefault(a => a.AdminId == 602);  // Query the database to find the admin by AdminId
                Assert.IsNotNull(insertedAdmin);
                Assert.AreEqual("Renusree", insertedAdmin.UserName);
                Assert.AreEqual("@User123", insertedAdmin.Password);
            }
        }
    }
}