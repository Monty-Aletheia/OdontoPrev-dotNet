using AutoMapper;
using ClaimService.Application.Dtos;
using ClaimService.Domain.Models;

namespace ClaimService.Application.Services.Profiles
{
	public class ClaimProfile : Profile
	{
		public ClaimProfile()
		{
			CreateMap<Claim, ClaimResponseDTO>();

			CreateMap<UpdateClaimDTO, Claim>();

			CreateMap<CreateClaimDTO, Claim>();
		}
	}
}