using Aletheia.Application.Dtos.Claim;
using Aletheia.Application.Dtos.Consultation;
using Aletheia.Domain.Entities;
using AutoMapper;

namespace Aletheia.Application.Services.Mappers
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Claim, ClaimResponseDTO>()
                .ForMember(dest => dest.Consultation, opt => opt.MapFrom(src => src.Consultation));

            CreateMap<Consultation, ConsultationSummaryDTO>();

            CreateMap<CreateClaimDTO, Claim>();
        }
    }
}
