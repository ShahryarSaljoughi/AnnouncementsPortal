using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Models;

namespace TeachersInformationCrawler.Implementations
{
    public class TeacherPageCrawler
    {
        public async Task CrawlPageAsync(TeacherInfo teacherInfo)
        {
            var htmlDoc = await GetHtmlDocument(teacherInfo);
            var box = htmlDoc.DocumentNode.SelectSingleNode(
                "//div[@id='art-main']/div[@class='art-sheet clearfix']/div[@class='art-layout-wrapper clearfix']//div[@class='art-layout-cell layout-item-2']");
            if (box is null)
            {
                throw new Exception();
            }

            teacherInfo.ZnuUrl = box.Descendants("a").First().Attributes["href"].Value;

        }

        private async Task<HtmlDocument> GetHtmlDocument(TeacherInfo teacherInfo)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36");
                httpClient.DefaultRequestHeaders.Referrer = new Uri("http://www.znu.ac.ir/members/");
                var url = teacherInfo.ZnuUrl;
                var response = await httpClient.GetAsync(url);
                var htmlString = await response.Content.ReadAsStringAsync();
                var normalizedHtml = WebUtility.HtmlDecode(htmlString);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(normalizedHtml);
                return htmlDoc;
            }
        }
    }
}
