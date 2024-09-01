using Microsoft.EntityFrameworkCore;
using projsysinf.Application.Dto;
using projsysinf.Infrastructure;

namespace projsysinf.Application.Services
{
    public interface ILogService
    {
        Task<List<LogEntryDto>> GetUserLogEntriesAsync(int userId);
    }

    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LogEntryDto>> GetUserLogEntriesAsync(int userId)
        {
            return await (from log in _context.Logs
                join operationType in _context.DicOperationTypes on log.OperationTypeId equals operationType.IdOperationType
                join user in _context.Users on log.UserId equals user.IdUser
                where log.UserId == userId
                orderby log.Tmstmp descending
                select new LogEntryDto
                {
                    OperationTypeName = operationType.OperationTypeName,
                    Email = user.Email,
                    Tmstmp = log.Tmstmp
                }).ToListAsync();
        }
    }
}