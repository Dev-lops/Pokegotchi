using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokegotchi.Model
{
    internal class Mascot
    {
        public string? name { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public List<Abilities>? abilities { get; set; }
        public bool loaded = false;
        public int hunger = new Random().Next(6, 9);
        public int mood = new Random().Next(6, 9);
        public int tolerance = 4;
    }
}
