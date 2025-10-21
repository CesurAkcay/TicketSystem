using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class TicketPostDto
    {
        public TicketPostDto()
        {
            CreatedAt = DateTimeOffset.Now;
        }
        public string Code { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public Guid TicketOwnerId { get; set; }
        public int? Status { get; set; }
        public int? Priority { get; set; }
    }
}
