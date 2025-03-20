using AutoMapper;
using ClaimService.Application.Dtos;
using ClaimService.Domain.Models;

namespace ClaimService.Application.Services.Profiles
{
	public class ClaimProfile : Profile
	{
		public ClaimProfile()
		{
			CreateMap<Claim, ClaimResponseDTO>()
				.ForMember(dest => dest.Consultation, opt => opt.MapFrom(src => src.Consultation));



			CreateMap<UpdateClaimDTO, Claim>();

			CreateMap<CreateClaimDTO, Claim>();
		}
	}
}