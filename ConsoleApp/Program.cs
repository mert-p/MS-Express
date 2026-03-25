using ConsoleApp.Presentation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ConsoleApp.Presentation.SubDisplays;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MainDisplay display = new MainDisplay();
            await display.Input();
        }
    }
}
