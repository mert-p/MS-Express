using ConsoleApp.Presentation.SubDisplays;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Presentation
{
    internal class MainDisplay
    {        
        private readonly CourierDisplay courierDisplay = new CourierDisplay();
        private readonly ClientDisplay clientDisplay =new ClientDisplay();        
        private readonly ShipmentDisplay shipmentDisplay = new ShipmentDisplay();         
        private readonly ServiceDisplay serviceDisplay =new ServiceDisplay();

        private readonly MishoHelper mishoHelper = new MishoHelper();

        public MainDisplay()
        {

        }
        public void Menu()
        {
            mishoHelper.ShowHeader("MS-Express");
            Console.WriteLine("1. Couriers");
            Console.WriteLine("2. Clients");
            Console.WriteLine("3. Shipments");
            Console.WriteLine("4. Services");
            Console.WriteLine("0. Exit");   
        }
        public async Task Input()
        {
            int input;
            do
            {
                Menu();
                input = mishoHelper.ReadIntInput("Please select an option:");

                switch (input)
                {
                    case 1:
                        await courierDisplay.Input();
                        break;
                    case 2:
                        await clientDisplay.Input();
                        break;
                    case 3:
                        await shipmentDisplay.Input();
                        break;
                    case 4:
                        await serviceDisplay.Input();
                        break;
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option selected, please try again."); 
                        break;
                }
            } while (input != 0);
        }
    }
}
