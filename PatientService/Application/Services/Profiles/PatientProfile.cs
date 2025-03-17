using AutoMapper;
using PatientService.Application.Dtos;
using PatientService.Domain.Models;

namespace PatientService.Application.Services.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientDTO, Patient>();
            CreateMap<Patient, PatientResponseDTO>();
            CreateMap<UpdatePatientDTO, Patient>();
        }
    }
}
