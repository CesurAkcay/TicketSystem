using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MappingProfiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketListDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.CreatedAtStr, opt => opt.MapFrom(src => src.CreatedAt.HasValue ? src.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm") : string.Empty))
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.TicketOwner != null ? src.TicketOwner.FullName : string.Empty))
                .ForMember(dest => dest.StatusStr, opt => opt.MapFrom(src => MapStatusToString(src.Status)))
                .ForMember(dest => dest.PriorityStr, opt => opt.MapFrom(src => MapPriorityToString(src.Priority)));

            CreateMap<TicketListDto, Ticket>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.CreatedAtStr) ? (DateTimeOffset?)null : DateTimeOffset.ParseExact(src.CreatedAtStr, "dd.MM.yyyy HH:mm", null)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MapStringToStatus(src.StatusStr)))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => MapStringToPriority(src.PriorityStr)))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Diğer üyeleri manuel olarak yok sayın
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore())
                .ForMember(dest => dest.TicketOwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.TicketOwner, opt => opt.Ignore());

            CreateMap<TicketPostDto, Ticket>();
        }

        private string MapStatusToString(int? status)
        {
            return status switch
            {
                0 => "New",
                1 => "In Progress",
                2 => "Closed",
                _ => "Unknown"
            };
        }
        private static int? MapStringToStatus(string statusStr)
        {
            return statusStr switch
            {
                "New" => 0,
                "In Progress" => 1,
                "Closed" => 2,
                _ => null
            };
        }

        private static string MapPriorityToString(int? priority)
        {
            return priority switch
            {
                0 => "Low",
                1 => "Medium",
                2 => "High",
                _ => "Unknown"
            };
        }

        private static int? MapStringToPriority(string priorityStr)
        {
            return priorityStr switch
            {
                "Low" => 0,
                "Medium" => 1,
                "High" => 2,
                _ => null
            };
        }

    }
}
