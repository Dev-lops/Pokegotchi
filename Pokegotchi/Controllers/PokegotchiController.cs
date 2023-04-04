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
        public static PokegotchiController? inst = new PokegotchiController();

        PokegotchiService service = new PokegotchiService();
        PokegotchiView view = new PokegotchiView();
        InteractionsController interaction = new InteractionsController();

        private const int UPDATE_TIME = 2, TIME_SUBTRAHEND = 1, FINISH_TIME = 15;
        private const int HUNGER_SUBTRAHEND = 1, MOOD_SUBTRAHEND = 1;

        public string? playerName;
        private int updateTimeCount = 0;

        private DateTime timeCount;
        public bool gameOver = false;
        private int score = 0, runawayMascotsCount = 0;

        List<Pokemon>? pokemons;
        private List<Mascot> adoptedMascots = new List<Mascot>();

        MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, Mascot>());
        IMapper mapper => config.CreateMapper();

        public void Play()
        {
            pokemons = service.GetPokemons();
            if(pokemons == null)
            {
                view.APIError();
                return;
            }

            timeCount = DateTime.Now;

            view.Welcome();

            string input = "";

            do
            {
                CountTime();

                Clock();
                if (gameOver) break;

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

            EndGame();
        }

        private void SelectPokemon()
        {
            CountTime();

            view.SelectPokemonMenu();
            view.ShowPokemonList(pokemons);

            string input = Console.ReadLine();
            int index;

            if (int.TryParse(input, out index) && index > 0 && index <= pokemons.Count)
            {
                LoadMascot(pokemons, index - 1);

                do
                {

                    view.PokemonSelectedMenu(pokemons[index - 1]);
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1": view.ShowPokemon(pokemons[index - 1]); break;
                        case "2": AdoptPokemon(pokemons[index - 1]); break;
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
                view.AdoptingMessage(pokemon.name, false);
            }
            else
            {
                Mascot mascot = mapper.Map<Mascot>(pokemon);

                adoptedMascots.Add(mascot);
                pokemons.Remove(pokemon);

                view.AdoptingMessage(pokemon.name, true);
            }
            Console.ReadKey();
        }

        private void MyMascot()
        {
            Clock();
            if (gameOver) return;

            string input;

            CountTime();

            view.ShowMyMascots(adoptedMascots);

            input = Console.ReadLine();
            int index;

            if (int.TryParse(input, out index) && index > 0 && index <= adoptedMascots.Count)
            {
                interaction.Interact(adoptedMascots, index - 1);
                MyMascot();
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

        public void Clock()
        {
            TimeSpan timeSpan = timeCount.Subtract(DateTime.Now);
            if (timeSpan.Minutes >= FINISH_TIME) gameOver = true;
        }

        private void EndGame()
        {
            score += adoptedMascots.Count;
            score -= pokemons.Count;
            score = Math.Max(0, score);

            view.GameOver(adoptedMascots.Count, pokemons.Count, runawayMascotsCount, score);
        }

        private void CountTime()
        {
            updateTimeCount = Math.Max(updateTimeCount - TIME_SUBTRAHEND, 0);

            int count = adoptedMascots.Count;

            if(updateTimeCount == 0)
            {
                if (adoptedMascots.Count > 0) UpdateMascots();

                updateTimeCount = UPDATE_TIME;
            }
        }
        
        private void UpdateMascots()
        {
            foreach (Mascot mascot in adoptedMascots)
            {
                CountScore(mascot.hunger, mascot.mood);

                mascot.hunger = Math.Max(0, mascot.hunger - HUNGER_SUBTRAHEND);
                mascot.mood = Math.Max(0, mascot.mood - MOOD_SUBTRAHEND);

                if (mascot.hunger == 0) mascot.tolerance = Math.Max(0, mascot.tolerance - 1);
                if(mascot.mood == 0) mascot.tolerance = Math.Max(0, mascot.tolerance - 1);

                if(mascot.tolerance == 0)
                {
                    MascotRunAway(mascot);
                    break;
                }
            }
        }

        private void CountScore(int hunger, int mood)
        {
            int average = (mood + hunger) / 2;

            if (average >= 7) score += 2;
            else if (average >= 4) score++;
        }

        private void MascotRunAway(Mascot mascot)
        {
            adoptedMascots.Remove(mascot);
            runawayMascotsCount++;

            view.RanAwayMessage(mascot);
        }
    }
}
