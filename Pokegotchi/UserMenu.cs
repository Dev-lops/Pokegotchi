using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokegotchi
{
    internal class UserMenu
    {
            private string playerName;
            private Mascot[] mascots;
            private List<Mascot> adoptedMascots;

            public UserMenu(ref Mascot[] mascots, ref List<Mascot> adoptedMascots)
            {
                this.mascots = mascots;
                this.adoptedMascots = adoptedMascots;
            }


            private void GetName()
            {
                Console.WriteLine("Qual o seu nome?");
                playerName = Console.ReadLine();
            }

            public void Start()
            {
                GetName();

                int input = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"{playerName}, o que você deseja fazer?");
                    Console.WriteLine("1-Adotar Mascote\n2-Ver Mascotes Adotados\n0-Sair");
                    input = int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case 1: SelectMascot(); break;
                        case 2: MyMascots(); break;
                    }
                } while (input != 0);
            }

            private void SelectMascot()
            {
                Console.Clear();
                Console.WriteLine("Escolha um Mascote:");

                ShowMascots();

                int input = int.Parse(Console.ReadLine());

                Mascot mascot = mascots[input - 1];

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Você escolheu o(a) {mascot.name}, o que deseja fazer?");
                    Console.WriteLine("1-Ver Detalhes\n2-Adotar\n3-Escolher outro\n0-Voltar");
                    input = int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case 1: MascotDetails(mascot); break;
                        case 2: AdoptMascot(mascot); break;
                        case 3: SelectMascot(); break;

                    }
                } while (input == 1);
            }

            private void MascotDetails(Mascot mascot)
            {
                var client = new RestClient(mascot.url);
                var response = client.Execute(new RestRequest("", Method.Get));

                JsonElement[] abilities = JsonDocument.Parse(response.Content).RootElement.GetProperty("abilities").EnumerateArray().ToArray();
                mascot = JsonSerializer.Deserialize<Mascot>(response.Content);

                mascot.abilities.Clear();
                for (int i = 0; i < abilities.Length; i++)
                {
                    JsonElement ability = abilities[i].GetProperty("ability");
                    mascot.abilities.Add(JsonSerializer.Deserialize<Ability>(ability));
                }
                Console.Clear();
                Console.WriteLine($"Nome: {mascot.name}\nAltura: {mascot.height}\nPeso: {mascot.weight}\nAbilities:");

                foreach (Ability ability in mascot.abilities)
                {
                    Console.Write(ability.name + ' ');
                }

                Console.WriteLine();
                Console.ReadKey();
            }

            private void AdoptMascot(Mascot mascot)
            {
                if (adoptedMascots.Exists(m => m == mascot))
                {
                    Console.WriteLine($"{mascot.name} já foi adotado!");
                }
                else
                {
                    adoptedMascots.Add(mascot);

                    Console.WriteLine($"{mascot.name} foi adotado com sucesso!");
                }
                Console.ReadKey();
            }

            public void ShowMascots()
            {
                for (int i = 0; i < mascots.Length; i++)
                {
                    Console.WriteLine($"{i + 1}-{mascots[i].name}");
                }
            }

            public void MyMascots()
            {
                Console.Clear();
                Console.WriteLine("Seus Mascotes: ");

                foreach (Mascot mascot in adoptedMascots)
                {
                    Console.WriteLine(mascot.name);
                }
                Console.ReadKey();
            }
    }
}
