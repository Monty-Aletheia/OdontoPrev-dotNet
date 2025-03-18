using AutoMapper;
using ConsultationService.Application.Dtos;
using ConsultationService.Domain.Models;

namespace ConsultationService.Application.Services.Profiles
{
    public class ConsultationProfile : Profile
    {
        public ConsultationProfile()
        {
            CreateMap<CreateConsultationDTO, Consultation>();
            CreateMap<Consultation, ConsultationResponseDTO>();
            CreateMap<UpdateConsultationDTO, Consultation>();
        }
    }
}
