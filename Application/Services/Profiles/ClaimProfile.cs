using Aletheia.Application.Dtos.Claim;
using Aletheia.Application.Dtos.Consultation;
using Aletheia.Domain.Entities;
using AutoMapper;

namespace Aletheia.Application.Services.Profiles
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Claim, ClaimResponseDTO>()
                .ForMember(dest => dest.Consultation, opt => opt.MapFrom(src => src.Consultation));


            CreateMap<Consultation, ConsultationSummaryDTO>();

            CreateMap<UpdateClaimDTO, Claim>();

            CreateMap<CreateClaimDTO, Claim>();
        }
    }
}
