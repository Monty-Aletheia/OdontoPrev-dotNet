using Aletheia.Application.Dtos.Patient;
using Aletheia.Domain.Entities;
using AutoMapper;

namespace Aletheia.Application.Services.Mappers
{
    public class PatientProfile: Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, CreatePatientDTO>();
            CreateMap<PatientResponseDTO, Patient>();
        }
    }
}
