using HttpClient.Database;
using HttpClient.domain.Features.Manager.ReqResModel;
using HttpClient.domain.Mappers;
using HttpClient.shared.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HttpClient.Database.Entities;

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

            var managers = await _context.Managers.AsNoTracking().Include(x => x.Team)
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

        #region Create Manager
        public async Task<Result<CreateManagerResponse>> CreateAsync(CreateManagerRequest request)
        {
            var responseModel = new Result<CreateManagerResponse>();

            var newManager = new HttpClient.Database.Entities.Manager
            {
                Age = request.Age,
                Country = request.Country,
                Name = request.Name,
            };

            await _context.Managers.AddAsync(newManager);

            var result = await _context.SaveChangesAsync();

            var data = new CreateManagerResponse
            {
                Age = newManager.Age,
                Country = newManager.Country,
                Name = newManager.Name,
            };

            responseModel = result >= 1
                 ? Result<CreateManagerResponse>.Success(data, "Create Success")
                 : Result<CreateManagerResponse>.SystemError("Create Failed");

            return responseModel;
        }
        #endregion

        #region
        public async Task<Result<ManagerResponse>> GetByIdAsync(int id)
        {
            var responseModel = new Result<ManagerResponse>();
            var manager = await _context.Managers.AsNoTracking()
                .Include(x => x.Team)
                .FirstOrDefaultAsync(m => m.ManagerId == id);

            if (manager is null)
            {
                responseModel = Result<ManagerResponse>.NotFoundError("Manager Not Found!");
                goto skip;
            }

            responseModel = Result<ManagerResponse>.Success(new ManagerResponse
            {
                Manager = manager.Change()
            });

        skip:
            return responseModel;
        }
        #endregion

        #region update
        public async Task<Result<ManagerResponse>> UpdateAsync(int id, UpdateManagerRequest request)
        {
            var responseModel = new Result<ManagerResponse>();
            var manager = await _context.Managers.FirstOrDefaultAsync(m => m.ManagerId == id);

            if (manager is null)
            {
                responseModel = Result<ManagerResponse>.NotFoundError("Manager Not Found!");
                goto skip;
            }

            manager.Name = request.Name ?? manager.Name;
            manager.Country = request.Country ?? manager.Country;
            manager.Age = request.Age ?? manager.Age;

            _context.Managers.Update(manager);
            var result = await _context.SaveChangesAsync();
            responseModel = result >= 1
                 ? Result<ManagerResponse>.Success(new ManagerResponse { Manager = manager.Change() }, "Update Success")
                 : Result<ManagerResponse>.SystemError("Update Failed");

        skip:
            return responseModel;
        }
        #endregion

        #region
        public async Task<Result<DeleteManagerResponse>> DeleteAsync(int id)
        {
            var responseModel = new Result<DeleteManagerResponse>();

            var manager = await _context.Managers.AsNoTracking().FirstOrDefaultAsync(m => m.ManagerId == id);

            if (manager is null)
            {
                responseModel = Result<DeleteManagerResponse>.NotFoundError();
                goto skip;
            }

            _context.Managers.Remove(manager);
            _context.Entry(manager).State = EntityState.Deleted;

            var result = await _context.SaveChangesAsync();

            responseModel = result >= 1
                ? Result<DeleteManagerResponse>.Success(new DeleteManagerResponse { IsDeleteSuccess = true })
                : Result<DeleteManagerResponse>.SystemError("Delete Fail!");

        skip:
            return responseModel;
        }
        #endregion
    }
}