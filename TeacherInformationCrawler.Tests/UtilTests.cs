using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Helper;
namespace TeacherInformationCrawler.Tests
{
    [TestClass]
    public class UtilTests
    {

        [TestMethod]
        public void TestDateConversion()
        {
            var input = DateTimeOffset.UtcNow;
            var result = input.ToPersianDate();
            // Assert.That(input == ""); whenever you run the test, insert the local date as parameter: 4/3/98
        }

        [TestMethod]
        public void TestNumTranslation()
        {
            var input = "salam shomare man hast 09211183140";
            var result = input.ToPersianNum();
        }
    }
}
