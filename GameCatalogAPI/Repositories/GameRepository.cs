using GameCatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("d1978b2e-ca1e-4602-a96f-c28ca95c91d4"), new Game{ Id = Guid.Parse("d1978b2e-ca1e-4602-a96f-c28ca95c91d4"), Name = "GTA 5", Producer = "EA", Price = 100} },
            {Guid.Parse("e2957700-6c1a-415e-8ab6-3366fe987d90"), new Game{ Id = Guid.Parse("e2957700-6c1a-415e-8ab6-3366fe987d90"), Name = "Call of Duty: Modern Warfare", Producer = "EA", Price = 200} },
            {Guid.Parse("803d3654-87dc-4c4a-91bc-a5080a792cc3"), new Game{ Id = Guid.Parse("803d3654-87dc-4c4a-91bc-a5080a792cc3"), Name = "Dota 2", Producer = "EA", Price = 155} },
            {Guid.Parse("0ffa06d8-fc52-48c5-9011-d8ee8d478e7a"), new Game{ Id = Guid.Parse("0ffa06d8-fc52-48c5-9011-d8ee8d478e7a"), Name = "Valorant", Producer = "EA", Price = 200} },
            {Guid.Parse("84627e8c-bdeb-4069-8a2a-b4e9e6229542"), new Game{ Id = Guid.Parse("84627e8c-bdeb-4069-8a2a-b4e9e6229542"), Name = "World of Warcraft", Producer = "Capcom", Price = 180} },
            {Guid.Parse("c705a60b-eaf6-4492-a014-eca4be6a7314"), new Game{ Id = Guid.Parse("c705a60b-eaf6-4492-a014-eca4be6a7314"), Name = "Minecraft ", Producer = "Rockstar", Price = 220} }
        };

        public Task<List<Game>> Get(int page, int amount)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * amount).Take(amount).ToList());
        }

        public Task<Game> Get(Guid id)
        {
            if (!games.ContainsKey(id))
                return Task.FromResult<Game>(null);

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Get(string nome, string producer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(nome) && game.Producer.Equals(producer)).ToList());
        }

        public Task<List<Game>> ObterSemLambda(string nome, string producer)
        {
            var retorno = new List<Game>();

            foreach(var game in games.Values)
            {
                if (game.Name.Equals(nome) && game.Producer.Equals(producer))
                    retorno.Add(game);
            }

            return Task.FromResult(retorno);
        }

        public Task Insert(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            
        }
    }
}
