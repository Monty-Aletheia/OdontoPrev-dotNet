using Aletheia.Application.Dtos.Consultation;
using Aletheia.Application.Dtos.Dentist;
using Aletheia.Application.Dtos.Patient;
using Aletheia.Domain.Entities;
using AutoMapper;

namespace Aletheia.Application.Services.Profiles
{
    public class ConsultationProfile : Profile
    {
        public ConsultationProfile()
        {
            CreateMap<CreateConsultationDTO, Consultation>();
            CreateMap<Consultation, ConsultationResponseDTO>()
             .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
             .ForMember(dest => dest.Dentists, opt => opt.MapFrom(src => src.Dentists));

            CreateMap<Patient, PatientResponseDTO>();
            CreateMap<Dentist, DentistResponseDTO>();
            CreateMap<UpdateConsultationDTO, Consultation>();
        }
    }
}
