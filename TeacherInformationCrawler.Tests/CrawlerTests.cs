using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TeachersInformationCrawler.Implementations;

namespace TeacherInformationCrawler.Tests
{
    [TestClass]
    public class CrawlerTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var pageCrawler = new TeacherPageCrawler();
            var teacherInfo = new TeacherInfo()
            {
                ZnuUrl = "http://www.znu.ac.ir/members/safari-leila"
            };
            await pageCrawler.CrawlPageAsync(teacherInfo);
        }
    }
}
