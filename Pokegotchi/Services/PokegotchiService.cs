using Pokegotchi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokegotchi.Services
{
    internal class PokegotchiService
    {
        public List<Mascot> GetPokemons()
        {
            var client = new RestClient("https://pokeapi.co/api/v2/pokemon?limit=20");
            var response = client.Execute(new RestRequest("", Method.Get));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JsonElement result = JsonDocument.Parse(response.Content).RootElement.GetProperty("results");
                List<Mascot> mascots = JsonSerializer.Deserialize<List<Mascot>>(result);
                return mascots;
            }
            else Console.WriteLine(response.ErrorMessage);
            return new List<Mascot>();
        }

        public T GetDetails<T>(string url)
        {
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest("", Method.Get));
            return JsonSerializer.Deserialize<T>(response.Content);
        }
    }
}
