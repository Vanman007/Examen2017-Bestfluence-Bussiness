using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExamProject2017;
using ExamProject2017.Repository;
using ExamProject2017.Models;
using Microsoft.AspNetCore.Identity;
using ExamProject2017.Data;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ExamUnitTestProject
{
//    [TestClass]
    public class UnitTest1
    {


  //      [TestMethod]
        [Fact]
        public void TestIfRightView()
        {


            var controller = new ExamProject2017.Controllers.InfluencerController(null, null, null, null);
            var result = controller.Test() as ViewResult;
            Assert.AreEqual("Test", result.ViewName);

        }



    }
}
