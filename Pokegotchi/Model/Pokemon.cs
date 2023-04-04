using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokegotchi.Model
{
    internal class Pokemon
    {
        public string? name { get; set; }
        public string? url { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public List<Abilities>? abilities { get; set; }
        public bool loaded = false;
    }
}
