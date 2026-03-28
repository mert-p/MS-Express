using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace ConsoleApp.Presentation.SubDisplays
{
    internal class CourierDisplay
    {
        private readonly CourierBusiness courierBusiness = new CourierBusiness();

        private readonly MishoHelper mishoHelper = new MishoHelper();


        private void CourierMenu()
        {
            mishoHelper.ShowHeader("Courier Management");
            Console.WriteLine("1. All Courier");
            Console.WriteLine("2. Add Courier");
            Console.WriteLine("3. Update Courier");
            Console.WriteLine("4. Fetch Courier by ID");
            Console.WriteLine("5. Delete Courier");
            Console.WriteLine("0. <-Back");
        }
        private void FetchCourierMenu()
        {
            mishoHelper.ShowHeader("Courier Info");
            Console.WriteLine("1.Only Courier");
            Console.WriteLine("2.Courier Shipments");
            Console.WriteLine("3.Courier Shipments with Adreses");
        }

        public async Task Input()
        {

            int input;
            do
            {
                Console.Clear();
                CourierMenu();
                input = mishoHelper.ReadIntInput("Please select an option:");
                switch (input)
                {
                    case 1:
                        await ListAllCouriers();
                        break;
                    case 2:
                        await AddCourier();
                        break;
                    case 3:
                        await UpdateCourier();
                        break;
                    case 4:
                        await FetchCourier();
                        break;
                    case 5:
                        await DeleteCourier();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key..."); Console.ReadKey(); Console.Clear();
            } while (input != 0);
        }
        private async Task ListAllCouriers()
        {
            var couriers = await courierBusiness.GetAllCouriersViews();
            if (couriers.Count == 0)
            {
                Console.WriteLine("No courier found.");
                return;
            }
            mishoHelper.ShowHeader("All Couriers");
            foreach (var courier in couriers)
            {
                Console.WriteLine(courier);
            }
        }
        private async Task AddCourier()
        {
            Courier courier = new Courier();
            courier.FirstName = mishoHelper.ReadStringInput("Enter first name:");
            courier.LastName = mishoHelper.ReadStringInput("Enter last name:");
            courier.Phone = mishoHelper.ReadPhoneInput("Enter phone number:");
            courier.Salary = mishoHelper.ReadDecimalInput("Enter salary:");
            await courierBusiness.Add(courier);
            Console.WriteLine("Courier added successfully.");
        }
        private async Task UpdateCourier()
        {
            var courierId = mishoHelper.ReadIntInput("Enter Author ID to update:");
            var courier = await courierBusiness.GetById(courierId);
            if (courier == null)
            {
                Console.WriteLine("Courier not found.");
                return;
            }
            await FetchCourierById(courierId);
            courier.FirstName = mishoHelper.ReadStringInput("Enter first name:");
            courier.LastName = mishoHelper.ReadStringInput("Enter last name:");
            courier.Phone = mishoHelper.ReadPhoneInput("Enter phone number:");
            courier.Salary = mishoHelper.ReadDecimalInput("Enter salary:");
            await courierBusiness.Update(courier);

            Console.WriteLine("Courier updated successfully.");
        }
        private async Task FetchCourier()
        {
            int input;
            do
            {
                FetchCourierMenu();
                input = mishoHelper.ReadIntInput("Please select an option:");
                switch (input)
                {
                    case 1:
                        await FetchCourierById();
                        break;
                    case 2:
                        await AddCourier();
                        break;
                    case 3:
                        await UpdateCourier();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key..."); Console.ReadKey(); Console.Clear();
            } while (input != 0);
        }
        private async Task DeleteCourier()
        {
            var courierId = mishoHelper.ReadIntInput("Enter Courier ID to delete:");
            var courier = await courierBusiness.GetById(courierId);
            if (courier == null)
            {
                Console.WriteLine("Courier not found.");
                return;
            }
            await courierBusiness.Delete(courierId);
            Console.WriteLine("Courier deleted successfully.");
        }
        public async Task FetchCourierById()
        {
            int courierId = mishoHelper.ReadIntInput("Enter Courier ID");
            var courier = await courierBusiness.GetCourierViewById(courierId);
            if (courier == null)
            {
                Console.WriteLine("Courier not found.");
                return;
            }
            Console.WriteLine(courier);
        }
        public async Task FetchCourierShipmentsById()
        {
            int courierId = mishoHelper.ReadIntInput("Enter Courier ID");
            var courier = await courierBusiness.GetCourierByIdWithShipments(courierId);
            if (courier == null)
            {
                Console.WriteLine("Courier not found.");
                return;
            }
            //foreach(var shipment in courier)
            Console.WriteLine(courier);
        }
        public async Task FetchCourierById(int courierId)
        {
            var courier = await courierBusiness.GetCourierViewById(courierId);
            if (courier == null)
            {
                Console.WriteLine("Courier not found.");
                return;
            }
            Console.WriteLine(courier);
        }
    }
}
