using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pokegotchi.Controllers;

internal class Program
{
    public static void Main(string[] args)
    {
        PokegotchiController controller = new PokegotchiController();
        controller.Play();
    }
}