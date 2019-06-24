using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.ApDbContext;
using Portal.Helper;
using Portal.Services.Implementations;

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

        [TestMethod]
        public async Task EmailService_MustWork()
        {
            var factory = new DbContextCreator();
            var db = factory.CreateDbContext();
            var service = new EmailSenderService(db);
            await service.SendEmailVerificationLink(Guid.Parse("02445C50-2D8E-4245-BB78-007503F3CBA0"));
        }
    }
}
