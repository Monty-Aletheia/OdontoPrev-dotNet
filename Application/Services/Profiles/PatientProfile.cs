using Aletheia.Application.Dtos.Patient;
using Aletheia.Domain.Entities;
using AutoMapper;

namespace Aletheia.Application.Services.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientDTO, Patient>();
            CreateMap<Patient, PatientResponseDTO>();
        }
    }
}
