﻿using Aletheia.Application.Dtos.Dentist;
using Aletheia.Domain.Entities;
using AutoMapper;

namespace Aletheia.Application.Services.Profiles
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
