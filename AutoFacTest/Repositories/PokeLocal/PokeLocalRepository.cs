using AutoFacTest.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoFacTest.Repositories.PokeLocal
{
    public class PokeLocalRepository : IPokeRepository
    {
        private readonly HttpClient _client;
        private readonly Uri _baseUri = new Uri("https://pokeapi.co/api/v2/");

        public PokeLocalRepository()
        {
            _client = new HttpClient() { BaseAddress = _baseUri };
        }

        public async Task<PokemonCollection> GetFirstTwentyPokemons()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "pokemon"))
            {
                using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                {
                    var code = response.EnsureSuccessStatusCode();
                    var results = DeserializeStream<PokemonCollection>(await response.Content.ReadAsStreamAsync());
                    foreach (var pokemon in results.Collection)
                    {
                        pokemon.Sprites = (await this.GetPokemonByName(pokemon.Name)).Sprites;
                    }
                    return results;
                } 
            }
        }

        public async Task<Pokemon> GetPokemonByName(string name)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"pokemon/{name}"))
            {
                using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                {
                    var code = response.EnsureSuccessStatusCode();
                    return DeserializeStream<Pokemon>(await response.Content.ReadAsStreamAsync());
                }
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private T DeserializeStream<T>(System.IO.Stream stream)
        {
            using (var sr = new System.IO.StreamReader(stream))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    var serializer = JsonSerializer.Create();
                    return serializer.Deserialize<T>(reader);
                }
            }
        }
    }
}
