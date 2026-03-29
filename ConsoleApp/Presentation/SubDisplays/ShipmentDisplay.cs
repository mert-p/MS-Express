using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Presentation.SubDisplays
{
    internal class ShipmentDisplay
    {
        private readonly ShipmentBusiness shipmentBusiness = new ShipmentBusiness();
        private readonly ClientBusiness clientBusiness = new ClientBusiness();
        private readonly CourierBusiness courierBusiness= new CourierBusiness();
        private readonly ShipmentServiceBusiness shipmentServiceBusiness = new ShipmentServiceBusiness();
        private readonly ServiceBusiness serviceBusiness = new ServiceBusiness();

        private readonly MishoHelper mishoHelper = new MishoHelper();

        private void ShipmentMenu()
        {
            mishoHelper.ShowHeader("Shipment Management");
            Console.WriteLine("1. All Shipments");
            Console.WriteLine("2. Add Shipment");
            Console.WriteLine("3. Update Shipment");
            Console.WriteLine("4. Fetch Shipment by ID");
            Console.WriteLine("5. Delete Shipment");
            Console.WriteLine("5. Calculate Shipment by ID");
            Console.WriteLine("0. <-Back");
        }
        private void ListingMenu()
        {
            mishoHelper.ShowHeader("All Shipments");
            Console.WriteLine("1.Normal");
            Console.WriteLine("2.With Services");
        }

        public async Task Input()
        {
            int input;
            do
            {
                Console.Clear();
                ShipmentMenu();
                input = mishoHelper.ReadIntInput("Please select an option:");
                switch (input)
                {
                    case 1:
                        await Listing();
                        break;
                    case 2:
                        await AddShipment();
                        break;
                    case 3:
                        await UpdateShipment();
                        break;
                    case 4:
                        await FetchShipment();
                        break;
                    case 5:
                        await DeleteShipment();
                        break;
                    case 6:
                        await CalculataPrice();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key..."); Console.ReadKey(); Console.Clear();
            } while (input != 0);
        }
        private async Task Listing()
        {
            ListingMenu();
            int input = mishoHelper.ReadIntInput("Please select an option:");
            switch (input)
            {
                case 1:
                    await ListAllShipments();
                    break;
                case 2:
                    await ListAllShipmentsWithServices();
                    break;
                default:
                    break;
            }
        }
        private async Task ListAllShipments()
        {
            var shipments = await shipmentBusiness.GetAllShipments();
            if (shipments.Count == 0)
            {
                Console.WriteLine("No shipment found.");
                return;
            }
            mishoHelper.ShowHeader("All Shipments");
            foreach (var shipment in shipments)
            {
                Console.WriteLine(shipment);
            }
        }
        private async Task ListAllShipmentsWithServices()
        {
            var shipments = await shipmentBusiness.GetShipmentsWirhService();
            if (shipments.Count == 0)
            {
                Console.WriteLine("No shipment found.");
                return;
            }
            mishoHelper.ShowHeader("All Shipments");
            foreach (var shipment in shipments)
            {
                Console.WriteLine(shipment);
                foreach(var service in shipment.ShipmentServices)
                {
                    Console.Write("Service:");
                    Console.WriteLine(service.Service);
                    Console.Write("Note:");
                    Console.WriteLine(service.Notes);
                }
            }
        }

        private async Task AddShipment()
        {
            bool service_owned=false;
            mishoHelper.ShowHeader($"Creating Shipment");
            Shipment shipment = new Shipment();
            await ListAllClients();
            shipment.SenderId = mishoHelper.ReadIntInput("Enter sender ID:");
            while(clientBusiness.GetById(shipment.SenderId)==null)
            { shipment.SenderId = mishoHelper.ReadIntInput("Wrong! Plese new ID:"); }
            shipment.ReceiverId = mishoHelper.ReadIntInput("Enter receiver ID:");
            while (clientBusiness.GetById(shipment.ReceiverId) == null)
            { shipment.SenderId = mishoHelper.ReadIntInput("Wrong! Plese new ID:"); }
            if (shipment.ReceiverId == shipment.SenderId)
            {
                Console.WriteLine("Sender and Reciver cant be the same");
                return;
            }
            await ListAllAvailabeleCouriers();
            shipment.CourierId = mishoHelper.ReadIntInput("Enter courier ID:");
            while (clientBusiness.GetById(shipment.CourierId) == null)
            { shipment.SenderId = mishoHelper.ReadIntInput("Wrong! Plese new ID:"); }
            Courier courier = await courierBusiness.GetById(shipment.CourierId);
            if(courier.Available==false)
            {
                Console.WriteLine("Courier is busy right now!");
                return;
            }
            courier.Available = false;
            await courierBusiness.Update(courier);
            shipment.Weight = mishoHelper.ReadDecimalInput("Enter weight:");
            shipment.Price = mishoHelper.ReadDecimalInput("Enter price:");
            shipment.Type = mishoHelper.ReadStringInput("Enter type:");
            shipment.Date = DateTime.Now.AddDays(10);
            shipment.Status = mishoHelper.ReadStringInput("Enter status:");  
            int shipmentId=await shipmentBusiness.AddWithId(shipment); 
            Console.WriteLine("Shipment added successfully.");
            Console.WriteLine("1.Without service");
            Console.WriteLine("2.With service");
            int input = mishoHelper.ReadIntInput("Please select an option:");
            switch (input)
            {
                case 1:
                    service_owned = false;
                    break;
                case 2:
                    service_owned = true;
                    break;
                default:
                    break;
            }
            if(service_owned)
            {
                int n = mishoHelper.ReadIntInput("How many services:");
                if(n>0)
                {
                    await ListAllServices();
                    mishoHelper.ShowHeader("Adding Services");
                    for (int i = 1; i <= n; i++)
                    {
                        Console.WriteLine($"{i} Sevice");
                        ShipmentService shipmentService = new ShipmentService();
                        shipmentService.ShipmentId = shipmentId;
                        shipmentService.ServiceId = mishoHelper.ReadIntInput("Enter service ID:");
                        shipmentService.Notes = mishoHelper.ReadStringInput("Enter notes:");
                        var existing = await shipmentServiceBusiness.GetShipmentServiceByIds(shipmentService.ShipmentId, shipmentService.ServiceId);
                        if (existing != null)
                        {
                            Console.WriteLine("This service is already added to this shipment!");
                            continue; 
                        }
                        await shipmentServiceBusiness.AddShipmentService(shipmentService);
                        Console.WriteLine("ShipmentService added successfully.");
                    }
                }
            }


        }

        private async Task UpdateShipment()
        {
            var shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID to update:");
            var shipment = await shipmentBusiness.GetById(shipmentId);

            if (shipment == null)
            {
                Console.WriteLine("Shipment not found.");
                return;
            }

            await FetchShipmentById(shipmentId);

            shipment.SenderId = mishoHelper.ReadIntInput("Enter new sender ID:");
            while (clientBusiness.GetById(shipment.SenderId) == null)
            { shipment.SenderId = mishoHelper.ReadIntInput("Wrong! Plese new ID:"); }
            shipment.ReceiverId = mishoHelper.ReadIntInput("Enter new receiver ID:");
            while (clientBusiness.GetById(shipment.ReceiverId) == null)
            { shipment.SenderId = mishoHelper.ReadIntInput("Wrong! Plese new ID:"); }
            if(shipment.ReceiverId == shipment.SenderId)
            { 
                Console.WriteLine("Sender and Reciver cant be the same");
                return;
            }
            await ListAllAvailabeleCouriers();
            shipment.CourierId = mishoHelper.ReadIntInput("Enter new courier ID:");
            while (clientBusiness.GetById(shipment.CourierId) == null)
            { shipment.SenderId = mishoHelper.ReadIntInput("Wrong! Plese new ID:"); }
            shipment.Weight = mishoHelper.ReadDecimalInput("Enter new weight:");
            shipment.Price = mishoHelper.ReadDecimalInput("Enter new price:");
            shipment.Type = mishoHelper.ReadStringInput("Enter new type:");
            shipment.Status = mishoHelper.ReadStringInput("Enter new status:");
            await shipmentBusiness.Update(shipment);
            Console.WriteLine("Shipment updated successfully.");
            Console.WriteLine("Wanna change the servise in the shipment?"); 
            Console.WriteLine("1.Add service by ID");
            Console.WriteLine("2.Update service by ID");
            Console.WriteLine("3.Delete service by ID");
            Console.WriteLine("0.NO!!!");
            int input = mishoHelper.ReadIntInput("Please select an option:");
            switch(input)
            {
                case 1:
                    await UpdateShipmentService(shipmentId);
                    break;
                case 2:
                    await DeleteShipmentService(shipmentId);
                    break;
                case 3:
                    await AddShipmentService(shipmentId);
                    break;
                default:
                    break;
            }
        }
        private async Task DeleteShipmentService(int shipmentId)
        {
            Console.WriteLine("Deleting until you press 0!");
            int serviceId;
            while ((serviceId = mishoHelper.ReadIntInput("Enter Service ID to delete:"))!=0)
            {
            var shipmentService = await shipmentServiceBusiness.GetShipmentServiceByIds(shipmentId, serviceId);

            if (shipmentService == null)
            {
                Console.WriteLine("Service not found.");
                return;
            }

            await shipmentServiceBusiness.DeleteShipmentService(shipmentId, serviceId);
            Console.WriteLine("Service deleted successfully.");
            }


        }
        private async Task UpdateShipmentService(int shipmentId)
        {
            Console.WriteLine("Updating until you press 0!");
            ShipmentService shipmentService = new ShipmentService();
            shipmentService.ShipmentId = shipmentId;

            while ((shipmentService.ServiceId = mishoHelper.ReadIntInput("Enter Service ID to update (0 to stop):")) != 0)
            {
                var existing = await shipmentServiceBusiness.GetShipmentServiceByIds(shipmentService.ShipmentId, shipmentService.ServiceId);

                if (existing == null)
                {
                    Console.WriteLine("This service is not added to this shipment!");
                    continue;
                }
                existing.Notes = mishoHelper.ReadStringInput("Enter new notes:");
                await shipmentServiceBusiness.UpdateShipmentService(existing);
                Console.WriteLine("Service updated successfully.");
            }
        }
        private async Task AddShipmentService(int shipmentId)
        {
            Console.WriteLine("Adding until you press 0!");
            int serviceId;
            while ((serviceId = mishoHelper.ReadIntInput("Enter Service ID:")) != 0)
            {
                ShipmentService shipmentService = new ShipmentService();
                shipmentService.ShipmentId = shipmentId;
                shipmentService.ServiceId = serviceId;
                var existing = await shipmentServiceBusiness.GetShipmentServiceByIds(shipmentService.ShipmentId, shipmentService.ServiceId);
                if (existing != null)
                {
                    Console.WriteLine("This service is already added to this shipment!");
                    continue;
                }
                shipmentService.Notes = mishoHelper.ReadStringInput("Enter notes:");
                await shipmentServiceBusiness.AddShipmentService(shipmentService);
                Console.WriteLine("Service added successfully.");
            }
        }

        private async Task FetchShipment()
        {
            var shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID to fetch:");
            await FetchShipmentById(shipmentId);
        }

        private async Task DeleteShipment()
        {
            var shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID to delete:");
            var shipment = await shipmentBusiness.GetById(shipmentId);
            if (shipment == null)
            {
                Console.WriteLine("Shipment not found.");
                return;
            }
            await shipmentBusiness.Delete(shipmentId);
            Console.WriteLine("Shipment deleted successfully.");
        }

        public async Task FetchShipmentById(int shipmentId)
        {
            var shipment = await shipmentBusiness.GetShipmentWirhService(shipmentId);
            if (shipment == null)
            {
                Console.WriteLine("Shipment not found.");
                return;
            }
            Console.WriteLine(shipment);
            foreach (var service in shipment.ShipmentServices)
            {
                Console.Write("Service:");
                Console.WriteLine(service.Service);
                Console.WriteLine("Note:");
                Console.WriteLine(service.Notes);
            }
        }
        private async Task ListAllClients()
        {
            var clients = await clientBusiness.GetAll();
            if (clients.Count == 0)
            {
                Console.WriteLine("No clients found.");
                return;
            }
            mishoHelper.ShowHeader("All Clients");
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }
        }
        private async Task ListAllAvailabeleCouriers()
        {
            var couriers = await courierBusiness.GetAvailableCouriers();
            if (couriers.Count == 0)
            {
                Console.WriteLine("No courier found.");
                return;
            }
            mishoHelper.ShowHeader("All Available Couriers");
            foreach (var courier in couriers)
            {
                Console.WriteLine(courier);
            }
        }
        private async Task ListAllServices()
        {
            var services = await serviceBusiness.GetAll();

            if (services.Count == 0)
            {
                Console.WriteLine("No services found.");
                return;
            }

            mishoHelper.ShowHeader("All Services");

            foreach (var service in services)
            {
                Console.WriteLine(service);
            }
        }
        private async Task CalculataPrice()
        {
            await ListAllShipments();
            int shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID to calculate:");
            var shipment = await shipmentBusiness.GetShipmentWirhService(shipmentId);    
            if (shipment == null)
            {
                Console.WriteLine("Shipment not found.");
                return;
            }
            decimal sum = shipment.Price;
            foreach(var i in shipment.ShipmentServices)
            {
                sum += i.Shipment.Price;
            }
            Console.WriteLine($"Total price{sum}");
        }
    }
}
