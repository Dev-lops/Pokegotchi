using Pokegotchi.Controllers;
using Pokegotchi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokegotchi.View
{
    internal class InteractionsView
    {
        public void MascotMenu(Mascot mascot)
        {
            Console.Clear();
            Console.WriteLine($"{PokegotchiController.inst.playerName}, o que você deseja fazer?");
            Console.WriteLine($"1-Saber como {FirstName(mascot.name)} está\n2-Alimentar {FirstName(mascot.name)}\n3-Brincar com {FirstName(mascot.name)}\n0-Voltar");
        }

        public void ShowStatus(Mascot mascot, int average)
        {

            Console.Clear();
            Console.WriteLine($"Nome: {FirstName(mascot.name)}");
            Console.WriteLine($"Height: {mascot.height}\nWeight: {mascot.weight}\nAbilities:");

            foreach (var abilities in mascot.abilities)
            {
                Console.Write(FirstName(abilities.ability.name) + " ");
                
            }

            Console.WriteLine($"\n\n{FirstName(mascot.name)} está com:");
            if (mascot.mood > 7) Console.WriteLine($"Bom humor!");
            else if(mascot.mood > 4) Console.WriteLine($"Mau humor.");
            else Console.WriteLine($"Pessimo humor!");

            if (mascot.hunger > 7) Console.WriteLine($"Barriga cheia!");
            else if (mascot.hunger > 4) Console.WriteLine($"Fome.");
            else Console.WriteLine($"Faminto!");

            if(average >= 7)
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.WriteLine(@"(ᵔ ᵕ ᵔ)", Encoding.Unicode);
            }
            else if(average >= 4)
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.WriteLine(@"(,,> _ <,,)", Encoding.Unicode);
            }
            else
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.WriteLine(@"(T ^ T)", Encoding.Unicode);
            }

            Console.ReadKey();
        }

        private string FirstName(string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1);
        }

        public void InputErrorMessage()
        {
            Console.WriteLine("Valor Invalido!");
            Console.ReadKey();
        }
    }
}
