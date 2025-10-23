using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITicketService
    {
        IDataResult<List<TicketListDto>> GetList();
        IResult Add(TicketPostDto ticketDto);
        IResult Delete(TicketPostDto ticketDto);
        IResult Update(TicketPostDto ticketDto);

       // IResult TransactionalOperation(TicketPostDto ticketDto);
    }
}
