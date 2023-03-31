using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pokegotchi;

internal class Program
{
    private static Mascot[] mascots;
    private static List<Mascot> adoptedMascots = new List<Mascot>();

    public static void Main(string[] args)
    {
        var client = new RestClient("https://pokeapi.co/api/v2/pokemon");
        var response = client.Execute(new RestRequest("", Method.Get));
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            JsonElement result = JsonDocument.Parse(response.Content).RootElement.GetProperty("results");
            mascots = JsonSerializer.Deserialize<Mascot[]>(result);
        }
        else Console.WriteLine(response.ErrorMessage);

        UserMenu menu = new UserMenu(ref mascots, ref adoptedMascots);
        menu.Start();
    }

    
}



public class Mascot
{
    public string? name { get; set; }
    public string? url { get; set; }
    public int? height { get; set; }
    public int? weight { get; set; }
    public List<Ability>? abilities { get; set; }
}

public class Ability
{
    public string name { get; set; }
    public string url { get; set; }
}
