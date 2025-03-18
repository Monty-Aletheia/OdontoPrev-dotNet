using AutoMapper;
using DentistService.Application.Dtos;
using DentistService.Domain.Models;

namespace DentistService.Application.Services.Profiles
{
    public class DentistProfile : Profile
    {
        public DentistProfile()
        {
            CreateMap<CreateDentistDTO, Dentist>();
            CreateMap<Dentist, DentistResponseDTO>();
            CreateMap<UpdateDentistDTO, Dentist>();
        }
    }
}
