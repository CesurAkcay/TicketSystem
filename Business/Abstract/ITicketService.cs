using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITicketService
    {
        IDataResult<List<Ticket>> GetList();
        IResult Add(Ticket ticket);
        IResult Delete(Ticket ticket);
        IResult Update(Ticket ticket);
    }
}
