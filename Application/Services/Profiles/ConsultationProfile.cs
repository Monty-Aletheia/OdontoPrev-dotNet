using Aletheia.Application.Dtos.Consultation;
using Aletheia.Domain.Entities;
using AutoMapper;

namespace Aletheia.Application.Services.Mappers
{
    public class ConsultationProfile : Profile
    {
        public ConsultationProfile()
        {
            CreateMap<Consultation, CreateConsultationDTO>();
            CreateMap<ConsultationResponseDTO, Consultation>();
        }
    }
}
