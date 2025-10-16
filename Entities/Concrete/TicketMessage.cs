using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TicketMessage : IEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public byte[]? RowVersion { get; set; }

        public Guid TicketId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? AdminUserId { get; set; }
        public string Message { get; set; }
    }
}
