using AutoFacTest.Models;
using System;
using System.Threading.Tasks;

namespace AutoFacTest.Repositories
{
    public interface IPokeRepository : IDisposable
    {
        Task<PokemonCollection> GetFirstTwentyPokemons();

        Task<Pokemon> GetPokemonByName(string name);
    }
}
