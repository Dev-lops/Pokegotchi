using Pokegotchi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokegotchi.Services;
using Pokegotchi.View;

namespace Pokegotchi.Controllers
{
    internal class PokegotchiController
    {
        List<Mascot> mascots;
        List<Mascot> adoptedMascots = new List<Mascot>();
        PokegotchiService service = new PokegotchiService();
        PokegotchiView view = new PokegotchiView();

        public void Play()
        {
            mascots = service.GetPokemons();

            view.Welcome();

            string input = "";
            do
            {
                view.MainMenu();
                input = Console.ReadLine();

                switch(input)
                {
                    case "1": SelectMascot(); break;
                    case "2": MyMascots(); break;
                    case "0": break;
                    default: view.InputErrorMessage(); break;
                }
            } while (input != "0");
        }

        private void SelectMascot()
        {
            view.SelectMascotMenu();
            view.ShowMascotList(mascots);

            string input = Console.ReadLine();
            int index;

            if (int.TryParse(input, out index) && index > 0 && index <= mascots.Count)
            {
                do
                {
                    view.MascotSelectedMenu(mascots[index - 1]);
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1": LoadMascot(mascots[index - 1]); break;
                        case "2": AdoptMascot(mascots[index - 1]); break;
                        case "3": SelectMascot(); break;
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
                SelectMascot();
            }
        }

        private void LoadMascot(Mascot mascot)
        {
            if (!mascot.loaded)
            {
                mascot = service.GetDetails<Mascot>(mascot.url);
                mascot.loaded = true;
            }
            view.ShowMascot(mascot);
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
                mascots.Remove(mascot);
                Console.WriteLine($"{mascot.name} foi adotado com sucesso!");
            }
            Console.ReadKey();
        }

        private void MyMascots()
        {
            view.ShowMyMascots(adoptedMascots);

            string input = Console.ReadLine();
            int index;

            if (int.TryParse(input, out index) && index > 0 && index <= adoptedMascots.Count)
            {
                LoadMascot(adoptedMascots[index - 1]);
                MyMascots();
            }
            else if (index == 0)
            {
                return;
            }
            else
            {
                view.InputErrorMessage();
                MyMascots();
            }
        }
    }
}
