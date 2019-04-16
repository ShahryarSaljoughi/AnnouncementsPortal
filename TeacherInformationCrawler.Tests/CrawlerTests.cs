using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Entities;
using TeachersInformationCrawler.Implementations;

namespace TeacherInformationCrawler.Tests
{
    [TestClass]
    public class CrawlerTests
    {
        [TestMethod]
        public async Task CrawlTeacherPage_MustWrok()
        {
            var pageCrawler = new TeacherPageCrawler();
            var teacherInfo = new TeacherInfo()
            {
                ZnuUrl = "http://www.znu.ac.ir/members/abbasi-majid"
            };
            await pageCrawler.CrawlPageAsync(teacherInfo);
        }
    }
}
