﻿using ConsultationService.Domain.Models;
using Shared.Interfaces;

namespace ConsultationService.Domain.Interfaces
{
	public interface IConsultationRepository : IRepository<Consultation>
	{
		Task AddRangeAsync(IEnumerable<Consultation> entities);
		Task<int> SaveChangesAsync();
	}
}