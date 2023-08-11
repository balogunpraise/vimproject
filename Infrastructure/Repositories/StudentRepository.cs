using Core.Application.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	internal class StudentRepository : IStudentRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<StudentRepository> _logger;

		public StudentRepository(ApplicationDbContext context, ILogger<StudentRepository> logger)
		{
			_context = context;
			_logger = logger;
		}
	}
}
