using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneCompaniApp.Services.Interfaces
{
    internal interface IReportCreatorService
    {
        public void CreateReport(IEnumerable<MainDataGridItem> items);
    }
}
