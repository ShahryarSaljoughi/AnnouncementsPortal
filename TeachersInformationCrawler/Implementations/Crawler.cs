using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Models.Entities;
using TeachersInformationCrawler.Contracts;

namespace TeachersInformationCrawler.Implementations
{
    public class Crawler: ICrawler
    {
        
        public async Task StartCrawling()
        {
            var finished = false;
            int currentPage = 1;
            var teacherInfos = new List<TeacherInfo>();

            while (!finished)
            {
                var htmlDocument = await GetTeachersList(currentPage);
                
                var tableRows =
                    htmlDocument.DocumentNode.SelectNodes(@"//div[@id='zu-teachers-grid']/table/tbody//tr");

                foreach (var tableRow in tableRows)
                {
                    var teacherInfo = new TeacherInfo();
                    var url = "http://www.znu.ac.ir" + 
                        tableRow.Descendants("a").First().Attributes.First(attribute => attribute.Name == "href").Value;
                    teacherInfo.ZnuUrl = url;    
                    teacherInfos.Add(teacherInfo);
                }
                
                currentPage++;
                if (tableRows.Count < 60)
                {
                    finished = true;
                }
            }
        }

        private async Task<HtmlDocument> GetTeachersList(int page = 0)
        {
            // http://www.znu.ac.ir/members/index?ZuTeachers_page=5

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36");

                var url = $@"http://www.znu.ac.ir/members/index?ZuTeachers_page={ page }";
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
