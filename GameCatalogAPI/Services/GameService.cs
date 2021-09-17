using GameCatalogAPI.Entities;
using GameCatalogAPI.Exceptions;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.Repositories;
using GameCatalogAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _jogoRepository;

        public GameService(IGameRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int amount)
        {
            var games = await _jogoRepository.Get(page, amount);

            return games.Select(game => new GameViewModel
                                {
                                    Id = game.Id,
                                    Name = game.Name,
                                    Producer = game.Producer,
                                    Price = game.Price
                                })
                               .ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var game = await _jogoRepository.Get(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var entidadeJogo = await _jogoRepository.Get(game.Name, game.Producer);

            if (entidadeJogo.Count > 0)
                throw new ExistsGameException();

            var jogoInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };

            await _jogoRepository.Insert(jogoInsert);

            return new GameViewModel
            {
                Id = jogoInsert.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var entidadeJogo = await _jogoRepository.Get(id);

            if (entidadeJogo == null)
                throw new NotExistsGameException();

            entidadeJogo.Name = game.Name;
            entidadeJogo.Producer = game.Producer;
            entidadeJogo.Price = game.Price;

            await _jogoRepository.Update(entidadeJogo);
        }

        public async Task Update(Guid id, double price)
        {
            var entidadeJogo = await _jogoRepository.Get(id);

            if (entidadeJogo == null)
                throw new NotExistsGameException();

            entidadeJogo.Price = price;

            await _jogoRepository.Update(entidadeJogo);
        }

        public async Task Delete(Guid id)
        {
            var game = await _jogoRepository.Get(id);

            if (game == null)
                throw new NotExistsGameException();

            await _jogoRepository.Delete(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
