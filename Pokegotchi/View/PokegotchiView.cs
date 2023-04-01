using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Pokegotchi.Model;

namespace Pokegotchi.View
{
    internal class PokegotchiView
    {
        private string playerName;

        public void Welcome()
        {
            Console.WriteLine("Qual o seu nome?");
            playerName = Console.ReadLine();
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"{playerName}, o que você deseja fazer?");
            Console.WriteLine("1-Adotar Mascote\n2-Ver Mascotes Adotados\n0-Sair");
        }

        public void SelectMascotMenu()
        {
            Console.Clear();
            Console.WriteLine("N-Selecionar Mascote 0-Voltar");
            Console.WriteLine("Mascotes:");
        }

        public void MascotSelectedMenu(Mascot mascot)
        {
            Console.Clear();
            Console.WriteLine($"Você escolheu o(a) {mascot.name}, o que deseja fazer?");
            Console.WriteLine("1-Ver Detalhes\n2-Adotar\n3-Escolher Outro\n0-Voltar ao Menu Principal");
        }

        public void ShowMascot(Mascot mascot)
        {
            Console.WriteLine($"Nome: {mascot.name}");
            Console.WriteLine($"Height: {mascot.height}\nWeight: {mascot.weight}\nAbilities:");
            foreach(var abilities in mascot.abilities)
            {
                Console.Write(abilities.ability.name + " ");
            }
            Console.ReadKey();
        }

        public void ShowMascotList(List<Mascot> mascots)
        {
            for(int i = 0; i < mascots.Count; i++)
            {
                Console.WriteLine($"{i + 1}-{mascots[i].name}");
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
                Console.WriteLine($"{i + 1}-{adoptedMascots[i].name}");
            }
        }

        public void InputErrorMessage()
        {
            Console.WriteLine("Valor Invalido!");
            Console.ReadKey();
        }
    }
}
