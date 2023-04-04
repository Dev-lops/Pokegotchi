using Pokegotchi.Model;
using Pokegotchi.Services;
using Pokegotchi.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokegotchi.Controllers
{
    internal class InteractionsController
    {
        InteractionsView view = new InteractionsView();

        private Mascot mascot;

        public void Interact(List<Mascot> mascots, int index)
        {
            mascot = mascots[index];

            string? input = "0";

            do
            {
                PokegotchiController.inst.Clock();
                if (PokegotchiController.inst.gameOver) break;

                mascots[index] = mascot;

                view.MascotMenu(mascot);

                
                input = Console.ReadLine();

                switch (input)
                {
                    case "1": view.ShowStatus(mascot, (mascot.hunger + mascot.mood) / 2); break;
                    case "2": Feed(); break;
                    case "3": Play(); break;
                    case "0": break;
                    default: view.InputErrorMessage(); break;
                }
            } while (input != "0");
        }
        
        private void Feed()
        {
            mascot.hunger = Math.Min(9, mascot.hunger + 3);
        }

        private void Play()
        {
            mascot.hunger = Math.Max(0, mascot.hunger - 3);
            mascot.mood = Math.Min(9, mascot.mood + 3);
        }
    }
}
