using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class TicketListDto
    {
        //public string Id{ get; set; }
        public string Code { get; set; }
        public string CreatedAtStr { get; set; }
        public string CustomerFullName { get; set; }
        public string StatusStr { get; set; }
        public string PriorityStr { get; set; }
    }
}
