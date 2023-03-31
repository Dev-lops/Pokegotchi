using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{
    const int NUMBER_OF_POKEMONS = 20;

    public static void Main(string[] args)
    {
        Mascote[] mascotes = new Mascote[NUMBER_OF_POKEMONS];

        for (int i = 0; i < NUMBER_OF_POKEMONS; i++)
        {
            RestResponse response = new RestClient($"https://pokeapi.co/api/v2/pokemon/{i+1}/").Execute(new RestRequest("", Method.Get));
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                mascotes[i] = JsonSerializer.Deserialize<Mascote>(response.Content); 
                Console.WriteLine($"Nome: {mascotes[i].name}\n" +
                    $"Height: {mascotes[i].height}\n" +
                    $"Weight: {mascotes[i].weight}");
                Console.WriteLine("Abilities:");
                for (int j = 0; j < mascotes[i].abilities.Count; j++)
                {
                    JsonElement json = JsonDocument.Parse(response.Content).RootElement.GetProperty($"abilities").EnumerateArray().ToArray()[j].GetProperty("ability");
                    mascotes[i].abilities[j] = JsonSerializer.Deserialize<Ability>(json);
                    Console.WriteLine($"{mascotes[i].abilities[j].name}");
                    
                }
                Console.WriteLine();
            }
        }
    }
}

public class Mascote
{
    public string? name { get; set; }
    public string? url { get; set; }
    public int? height { get; set; }
    public int? weight { get; set; }
    public List<Ability>? abilities { get; set; }
}

public struct Ability
{
    public string name { get; set; }
}
