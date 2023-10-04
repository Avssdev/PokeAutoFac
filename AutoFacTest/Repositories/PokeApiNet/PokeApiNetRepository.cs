using PokeApiNet;
using System.Threading.Tasks;

namespace AutoFacTest.Repositories.PokeApiNet
{
    public class PokeApiNetRepository : IPokeRepository
    {
        private readonly PokeApiClient pokeClient = new PokeApiClient();

        public async Task<Models.PokemonCollection> GetFirstTwentyPokemons()
        {
            NamedApiResourceList<Pokemon> pokemonList = await pokeClient.GetNamedResourcePageAsync<Pokemon>();

            Models.PokemonCollection pokeCollection = new Models.PokemonCollection();

            foreach (var pokemon in pokemonList.Results)
            {
                Models.Pokemon result = await GetPokemonByName(pokemon.Name);
                pokeCollection.Collection.Add(result);
            }

            return pokeCollection;
        }

        public async Task<Models.Pokemon> GetPokemonByName(string name)
        {
            Pokemon pokemon = await pokeClient.GetResourceAsync<Pokemon>(name);

            Models.Pokemon result = new Models.Pokemon();
            result.Name = pokemon.Name;
            result.Sprites.FrontDefault = pokemon.Sprites.FrontDefault;
            result.Sprites.BackDefault = pokemon.Sprites.BackDefault;

            return result;
        }

        public void Dispose()
        {
            pokeClient.Dispose();
        }
    }
}
