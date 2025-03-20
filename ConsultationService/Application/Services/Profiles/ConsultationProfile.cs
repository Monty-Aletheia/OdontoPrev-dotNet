using AutoMapper;
using ConsultationService.Application.Dtos;
using ConsultationService.Domain.Models;

namespace ConsultationService.Application.Services.Profiles
{
	public class ConsultationProfile : Profile
	{
		public ConsultationProfile()
		{
			CreateMap<CreateConsultationDTO, Consultation>();
			CreateMap<Consultation, ConsultationResponseDTO>()
				.ForMember(dest => dest.DentistIds, opt => opt.MapFrom(src => src.ConsultationDentists.Select(cd => cd.DentistId)));

			CreateMap<UpdateConsultationDTO, Consultation>();
		}
	}
}