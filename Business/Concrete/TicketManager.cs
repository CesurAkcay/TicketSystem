using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Caching;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TicketManager : ITicketService
    {
        private readonly ITicketDal _ticketDal;
        private readonly IMapper _mapper;
        public TicketManager(ITicketDal ticketDal, IMapper mapper)
        {
            _ticketDal = ticketDal;
            _mapper = mapper;
        }

        //[ValidationAspect(typeof(TicketValidator), Priority = 1)]
        public IResult Add(TicketPostDto ticketDto)
        {
            ;
            var ticket = _mapper.Map<Ticket>(ticketDto);
            _ticketDal.Add(ticket);
            return new SuccessResult(Messages.TicketAdded);
        }

        public IResult Delete(TicketPostDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            _ticketDal.Delete(ticket);
            return new SuccessResult(Messages.TicketDeleted);
        }
        [CacheAspect(1)]
        public IDataResult<List<TicketListDto>> GetList()
        {
            var ticketDtos = _mapper.Map<List<TicketListDto>>(_ticketDal.GetList(). ToList());
            return new SuccessDataResult<List<TicketListDto>>(ticketDtos);
        }

     

        public IResult Update(TicketPostDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            _ticketDal.Update(ticket);
            return new SuccessResult(Messages.TicketUpdated);
        }  
    }
}
