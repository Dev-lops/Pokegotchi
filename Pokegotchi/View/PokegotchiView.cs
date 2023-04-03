using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Pokegotchi.Model;
using Pokegotchi.Controllers;

namespace Pokegotchi.View
{
    internal class PokegotchiView
    {

        public void Welcome()
        {
            Console.WriteLine("Qual o seu nome?");
            PokegotchiController.playerName = Console.ReadLine();
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"{PokegotchiController.playerName}, o que você deseja fazer?");
            Console.WriteLine("1-Adotar Mascote\n2-Ver Mascotes Adotados\n0-Sair");
        }

        public void SelectPokemonMenu()
        {
            Console.Clear();
            Console.WriteLine("N-Selecionar Mascote 0-Voltar");
            Console.WriteLine("Mascotes:");
        }

        public void PokemonSelectedMenu(Pokemon pokemon)
        {
            Console.Clear();
            Console.WriteLine($"Você escolheu o(a) {FirstName(pokemon.name)}, o que deseja fazer?");
            Console.WriteLine("1-Ver Detalhes\n2-Adotar\n3-Escolher Outro\n0-Voltar ao Menu Principal");
        }

        public void ShowPokemon(Pokemon pokemon)
        {
            Console.WriteLine($"Nome: {FirstName(pokemon.name)}");
            Console.WriteLine($"Height: {pokemon.height}\nWeight: {pokemon.weight}\nAbilities:");
            foreach(var abilities in pokemon.abilities)
            {
                Console.Write(FirstName(abilities.ability.name) + " ");
            }
            Console.ReadKey();
        }

        public void ShowPokemonList(List<Pokemon> pokemons)
        {
            for(int i = 0; i < pokemons.Count; i++)
            {
                Console.WriteLine($"{i + 1}-{FirstName(pokemons[i].name)}");
            }
        }

        public void ShowMyMascots(List<Mascot> adoptedMascots)
        {
            if(adoptedMascots.Count == 0)
            {
                Console.WriteLine("Nenhum Mascote Adotado Ainda!");
                return;
            }

            Console.Clear();
            Console.WriteLine("N-Ver Detalhes 0-Voltar");
            Console.WriteLine("Seus Mascotes:");

            for(int i = 0; i < adoptedMascots.Count; i++)
            {
                Console.WriteLine($"{i + 1}-{FirstName(adoptedMascots[i].name)}");
            }
        }

        public void InputErrorMessage()
        {
            Console.WriteLine("Valor Invalido!");
            Console.ReadKey();
        }
        
        public string FirstName(string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1);
        }
    }
}
