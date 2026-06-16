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

        public GameService(GameStoreDbContext context)
        {
            _context = context;
        }


        //get all games
        public async Task<List<GameStoreDb.Models.Game>> GetAllAsync()
        {
            var games = await _context.Games.AsNoTracking()
                .ToListAsync();

            return games;
        }



    }
}
