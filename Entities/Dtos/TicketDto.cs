using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid TicketOwnerId { get; set; }
        public Customer TicketOwner { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
    }
}
