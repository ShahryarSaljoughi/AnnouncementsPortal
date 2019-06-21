using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeachersInformationCrawler.Contracts
{
    public interface ICrawler
    {
        Task StartCrawlingAsync();
    }
}
