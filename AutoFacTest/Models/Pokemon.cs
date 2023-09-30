using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutoFacTest.Models
{
    public class PokemonCollection
    {
        public PokemonCollection()
        {
            Collection = new List<Pokemon>();
        }

        [JsonProperty("results")]
        public List<Pokemon> Collection { get; set; }
    }

    public class Pokemon
    {
        public Pokemon()
        {
            Sprites = new PokemonSprites();
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public PokemonSprites Sprites { get; set; }
    }

    public class PokemonSprites
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }

        [JsonProperty("back_default")]
        public string BackDefault { get; set; }
    }
}
