using Aletheia.Application.Dtos.Claim;
using Aletheia.Domain.Entities;
using AutoMapper;

namespace Aletheia.Application.Services.Mappers
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Claim, CreateClaimDTO>();
            CreateMap<ClaimResponseDTO, Claim>();
        }
    }
}
