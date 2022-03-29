using System;
using SolarFarm.BLL;
using SolarFarm.Core.DTO;
using SolarFarm.Core.Interfaces;

namespace SolarFarm
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleIO ui = new();
            MenuController menu = new MenuController(ui);
            menu.Run();
        }
    }
}