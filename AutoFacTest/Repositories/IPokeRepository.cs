using AutoFacTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoFacTest.Repositories
{
    public interface IPokeRepository
    {
        Task<PokemonCollection> GetFirstTwentyPokemons();

        Task<Pokemon> GetPokemonByName(string name);
    }
}
