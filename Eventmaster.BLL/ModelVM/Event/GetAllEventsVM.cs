using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmaster.BLL.ModelVM.Event
{
    public class GetAllEventsVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Eventdate { get; set; }
        public string Venue { get; set; }
    }
}
