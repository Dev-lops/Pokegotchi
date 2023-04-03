using Pokegotchi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokegotchi.Services;
using Pokegotchi.View;
using AutoMapper;

namespace Pokegotchi.Controllers
{
    internal class PokegotchiController
    {
        PokegotchiService service = new PokegotchiService();
        PokegotchiView view = new PokegotchiView();
        InteractionsController interaction = new InteractionsController();

        public static string playerName;
        List<Pokemon> Pokemons;
        List<Mascot> adoptedMascots = new List<Mascot>();

        MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, Mascot>());
        IMapper mapper => config.CreateMapper();

        public void Play()
        {
            Pokemons = service.GetPokemons();

            view.Welcome();

            string input = "";
            do
            {
                view.MainMenu();
                input = Console.ReadLine();

                switch(input)
                {
                    case "1": SelectPokemon(); break;
                    case "2": MyMascot(); break;
                    case "0": break;
                    default: view.InputErrorMessage(); break;
                }
            } while (input != "0");
        }

        private void SelectPokemon()
        {
            view.SelectPokemonMenu();
            view.ShowPokemonList(Pokemons);

            string input = Console.ReadLine();
            int index;

            if (int.TryParse(input, out index) && index > 0 && index <= Pokemons.Count)
            {
                LoadMascot(Pokemons, index - 1);

                do
                {
                    view.PokemonSelectedMenu(Pokemons[index - 1]);
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1": view.ShowPokemon(Pokemons[index - 1]); break;
                        case "2": AdoptPokemon(Pokemons[index - 1]); break;
                        case "3": SelectPokemon(); break;
                        case "0": break;
                        default: 
                            view.InputErrorMessage();
                            input = "1";
                            break;
                    }
                } while (input == "1");
            }
            else if (index == 0)
            {
                return;
            }
            else
            {
                view.InputErrorMessage();
                SelectPokemon();
            }
        }

        public void LoadMascot(List<Pokemon> pokemons, int index)
        {
            if (!pokemons[index].loaded)
            {
                pokemons[index] = service.GetDetails<Pokemon>(pokemons[index].url);
                pokemons[index].loaded = true;
            }
        }

        private void AdoptPokemon(Pokemon pokemon)
        {
            if (adoptedMascots.Exists(m => m.name == pokemon.name))
            {
                Console.WriteLine($"{view.FirstName(pokemon.name)} já foi adotado!");
            }
            else
            {
                Mascot mascot = mapper.Map<Mascot>(pokemon);

                adoptedMascots.Add(mascot);
                Pokemons.Remove(pokemon);

                Console.WriteLine($"{view.FirstName(pokemon.name)} foi adotado com sucesso!");
            }
            Console.ReadKey();
        }

        private void MyMascot()
        {
            view.ShowMyMascots(adoptedMascots);

            string input = Console.ReadLine();
            int index;

            if (int.TryParse(input, out index) && index > 0 && index <= adoptedMascots.Count)
            {
                interaction.Interact(adoptedMascots, index - 1);
            }
            else if (index == 0)
            {
                return;
            }
            else
            {
                view.InputErrorMessage();
                MyMascot();
            }
        }
    }
}
