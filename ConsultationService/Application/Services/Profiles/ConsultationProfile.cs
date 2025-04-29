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
				.ForMember(dest => dest.Dentists, opt => opt.Ignore())
				.ForMember(dest => dest.Patient, opt => opt.Ignore());


			CreateMap<UpdateConsultationDTO, Consultation>();
		}
	}
}