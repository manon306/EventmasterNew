using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmaster.BLL.ModelVM.Admin
{
    public class DashboardStatsVm
    {
        public int TotalUsers { get; set; }
        public int TotalEvents { get; set; }
        public int PendingEvents { get; set; }
        public int TotalParticipants { get; set; }
    }
}
