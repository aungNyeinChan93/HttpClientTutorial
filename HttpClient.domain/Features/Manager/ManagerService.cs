using HttpClient.Database;
using HttpClient.domain.Mappers;
using HttpClient.shared.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.Manager
{
    public class ManagerService : IManagerService
    {
        private readonly AppDbContext _context;

        public ManagerService(AppDbContext context)
        {
            _context = context;
        }

        #region GetAllManagers
        public async Task<Result<ManagersResponse>> GetAllAsync()
        {
            var responseModel = new Result<ManagersResponse>();

            var managers = await _context.Managers.AsNoTracking().Include(x=>x.Team)
                .ToListAsync();

            if (managers is null)
            {
                responseModel = Result<ManagersResponse>.NotFoundError("Manager Not Found!");
                goto skip;
            }

            responseModel = Result<ManagersResponse>.Success(new ManagersResponse
            {
                Manager = managers
                .Select(m => m.Change()).ToList()
            });

        skip:
            return responseModel;
        }
        #endregion
    }
}
