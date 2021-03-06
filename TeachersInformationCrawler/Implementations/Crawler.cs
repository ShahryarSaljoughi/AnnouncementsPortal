﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Models.ApDbContext;
using Models.Entities;
using Models.Enums;
using TeachersInformationCrawler.Contracts;

namespace TeachersInformationCrawler.Implementations
{
    public class Crawler: ICrawler
    {
        public APDbContext DbContext { get; set; }

        public Crawler(APDbContext db)
        {
            DbContext = db;
        }
        
        public async Task StartCrawlingAsync()
        {
            var pageCrawler = new TeacherPageCrawler();
            var finished = false;
            int currentPage = 1;
            var teacherInfos = new List<Teacher>();

            while (!finished)
            {
                var htmlDocument = await GetTeachersList(currentPage);
                
                var tableRows =
                    htmlDocument.DocumentNode.SelectNodes(@"//div[@id='zu-teachers-grid']/table/tbody//tr");

                foreach (var tableRow in tableRows)
                {
                    var teacherInfo = new Teacher();
                    var url = "http://www.znu.ac.ir" + 
                        tableRow.Descendants("a").First().Attributes.First(attribute => attribute.Name == "href").Value;

                    var college = tableRow.Elements("td").ElementAt(tableRow.Elements("td").Count() - 2).InnerText;
                    var department = tableRow.Elements("td").Last().InnerText;
                    var departmentEntity = await DbContext.Set<Department>()
                        .FirstOrDefaultAsync(i =>
                            (department.Contains(i.PersianName) || i.PersianName.Contains(department))
                            && i.College.GetPersianTranslation().Contains(college));
                    teacherInfo.Department = departmentEntity;
                    teacherInfo.DepartmentId = departmentEntity?.Id;
                    teacherInfo.ZnuUrl = url;
                    await pageCrawler.CrawlPageAsync(teacherInfo);
                    teacherInfos.Add(teacherInfo);
                }
                currentPage++;
                if (tableRows.Count < 60)
                {
                    finished = true;
                }
                
            }

            await SaveRecordsAsync(teacherInfos);
        }

        private async Task SaveRecordsAsync(List<Teacher> teacherInfos)
        {
            string connectoinString;
            using (var db = new DbContextCreator().CreateDbContext())
            {
                connectoinString = db.Database.GetDbConnection().ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectoinString))
                {
                    connection.Open();
                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName =
                            $"AP.{nameof(Teacher)}";

                        bulkCopy.ColumnMappings.Add("ZnuUrl", "ZnuUrl");
                        bulkCopy.ColumnMappings.Add("AcademicRank", "AcademicRank");
                        bulkCopy.ColumnMappings.Add("Address", "Address");
                        bulkCopy.ColumnMappings.Add("Email", "Email");
                        bulkCopy.ColumnMappings.Add("Phone", "Phone");
                        bulkCopy.ColumnMappings.Add("Firstname", "Firstname");
                        bulkCopy.ColumnMappings.Add("Lastname", "Lastname");
                        bulkCopy.ColumnMappings.Add("Id", "Id");
                        bulkCopy.ColumnMappings.Add("AccountActivated", "AccountActivated");
                        bulkCopy.ColumnMappings.Add("DepartmentId", "DepartmentId");

                        var dataTable = new DataTable();
                        dataTable.Columns.Add("ZnuUrl", typeof(string));
                        dataTable.Columns.Add("AcademicRank", typeof(string));
                        dataTable.Columns.Add("Address", typeof(string));
                        dataTable.Columns.Add("Email", typeof(string));
                        dataTable.Columns.Add("Phone", typeof(string));
                        dataTable.Columns.Add("Firstname", typeof(string));
                        dataTable.Columns.Add("Lastname", typeof(string));
                        dataTable.Columns.Add("Id", typeof(Guid));
                        dataTable.Columns.Add("AccountActivated", typeof(bool));
                        dataTable.Columns.Add("DepartmentId", typeof(Guid));

                        var rows = teacherInfos.Select(ti =>
                        {
                            var row = dataTable.NewRow();
                            row["ZnuUrl"] = ti.ZnuUrl;
                            row["AcademicRank"] = ti.AcademicRank;
                            row["Address"] = ti.Address;
                            row["Email"] = ti.Email;
                            row["Phone"] = ti.Phone;
                            row["Firstname"] = ti.Firstname;
                            row["Lastname"] = ti.Lastname;
                            row["Id"] = ti.Id == default(Guid) ? Guid.NewGuid(): ti.Id;
                            row["AccountActivated"] = false;
                            row["DepartmentId"] = (object) ti.DepartmentId ?? DBNull.Value;
                            return row;
                        });
                        foreach (var dataRow in rows)
                        {
                            dataTable.Rows.Add(dataRow);
                        }
                        await bulkCopy.WriteToServerAsync(dataTable);
                    }
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
                var normalizedHtml = WebUtility.HtmlDecode(htmlString)?.Replace('ي', 'ی')?.Replace('ك', 'ک');
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(normalizedHtml);
                return htmlDoc;
            }
        }
    }
}
