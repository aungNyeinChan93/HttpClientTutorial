using HttpClient.domain.Features.Dapper;
using HttpClient.GameStoreDb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.Game
{
    public class GameService
    {
        private GameStoreDbContext _context;

        private readonly IDapperService _dapperService;

        public GameService(GameStoreDbContext context, IDapperService dapperService)
        {
            _context = context;
            _dapperService = dapperService;
        }


        //get all games
        public async Task<List<GameStoreDb.Models.Game>> GetAllAsync()
        {
            var games = await _context.Games.AsNoTracking()
                .ToListAsync();

            return games;
        }

        //Get One 
        public async Task<GameStoreDb.Models.Game> GetOneAsync(int id)
        {
            var query = @"select * from dbo.Games where Games.Id = @gameId";
            var game = await _dapperService.QueryAsync<GameStoreDb.Models.Game>(query,new {gameId = id});
            return game.FirstOrDefault()!;
        }



    }
}
