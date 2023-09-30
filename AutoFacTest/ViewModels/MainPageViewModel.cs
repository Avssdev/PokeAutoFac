using AutoFacTest.Commands;
using AutoFacTest.Models;
using AutoFacTest.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AutoFacTest.ViewModels
{
    public class MainPageViewModel
    {
        // Mantém uma instância readonly do repositório
        private readonly IPokeRepository _pokeRepository;

        //
        private readonly IEnumerable<ToolbarButton> _toolbarButtons;

        public ObservableCollection<Pokemon> PokemonsCollection = new ObservableCollection<Pokemon>();

        public string SearchText { get; set; }

        // MainPageViewModel não se preocupa com qual a implementação concreta do repositório,
        // enxerga apenas a interface tornando o código menos acoplado
        public MainPageViewModel(IPokeRepository pokeRepository, IEnumerable<ToolbarButton> toolbarButtons)
        {
            _pokeRepository = pokeRepository;
            _toolbarButtons = toolbarButtons;
        }

        public async Task LoadPokemons()
        {
            var result = await _pokeRepository.GetFirstTwentyPokemons();
            foreach (var pokemon in result.Collection)
                PokemonsCollection.Add(pokemon);
        }

        public async Task SearchPokemon()
        {
            var result = await _pokeRepository.GetPokemonByName(SearchText);
            PokemonsCollection.Clear();
            PokemonsCollection.Add(result);
        }

        public IEnumerable<ToolbarButton> GetToolbarButtons()
        {
            return _toolbarButtons;
        }
    }
}
