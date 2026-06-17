using Httpclient.AuthDatabase.Models;
using HttpClient.domain.Features.User.ReqResModels;
using HttpClient.domain.Mappers;
using HttpClient.shared.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.User
{
    public class UserService
    {
        private readonly AuthDatabase _context;

        public UserService(AuthDatabase context)
        {
            _context = context;
        }

        //Get All Users
        public async Task<Result<UserListResponse>> GetAllUsersAsync()
        {
            var responseModel = new Result<UserListResponse>();

            var users = await _context.Users.AsNoTracking().Select(x=>x.Change()).ToListAsync();

            if (users is null || users.Count <=0)
            {
                responseModel = Result<UserListResponse>.NotFoundError("User is not found!");
                goto skip;
            }

            var data = new UserListResponse
            {
                Users = users
            };

            responseModel = Result<UserListResponse>.Success(data,"Users List success!");

        skip:
            return responseModel;
        }
    }
}
