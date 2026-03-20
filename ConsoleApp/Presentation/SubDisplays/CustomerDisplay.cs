using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Presentation.SubDisplays
{
    internal class CustomerDisplay
    {
        private int closeOperationId = 6;
        CustomerBusiness customerBusiness = new CustomerBusiness();
        public CustomerDisplay()
        {
            Input();
        }

        private void ShowMenu()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 18) + "MENU");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. List all customers");
            Console.WriteLine("2. Add new customer");
            Console.WriteLine("3. Update customer");
            Console.WriteLine("4. Fetch customer by ID");
            Console.WriteLine("5. Delete customer by ID");
            Console.WriteLine("6. Exit!");
        }

        private void Input()
        {
            var operation = -1;
            do
            {
                ShowMenu();
                operation = int.Parse(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        ListAllCustomers();
                        break;
                    case 2:
                        AddCustomer();
                        break;
                    case 3:
                        Update();
                        break;
                    case 4:
                        Fetch();
                        break;
                    case 5:
                        Delete();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key..."); Console.ReadKey(); Console.Clear();
            } while (operation != closeOperationId);

        }
        private async Task ListAllCustomers()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 18) + "CUSTOMERS");
            Console.WriteLine(new string('-', 40));
            var customers = await customerBusiness.GetAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id} {customer.FisrtName} {customer.LastName} {customer.Address}");
            }

        }
        private async Task AddCustomer()
        {
            Customer customer = new Customer();
            Console.Write("FisrtName: ");
            customer.FisrtName = Console.ReadLine();
            Console.Write("LastName: ");
            customer.LastName = Console.ReadLine();
            Console.Write("Address: ");
            customer.Address = Console.ReadLine();
            customerBusiness.AddCustomer(customer);
            Console.WriteLine("The customer has been added!");
        }
        private async Task Update()
        {
            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine());
            Customer customer = await customerBusiness.GetCustomerById(id);
            if (customer != null)
            {
                Console.WriteLine($"{customer.Id} {customer.FisrtName} {customer.LastName} {customer.Address}");
                Console.Write("FisrtName: ");
                customer.FisrtName = Console.ReadLine();
                Console.Write("LastName: ");
                customer.LastName = Console.ReadLine();
                Console.Write("Stock: ");
                customer.Address = Console.ReadLine();
                customerBusiness.UpdateCustomer(customer);
                Console.WriteLine("The customer has been updated!");
            }
            else
            {
                Console.WriteLine("Customer not found!");
            }
        }
        private async Task Fetch()
        {
            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine());
            Customer customer = await customerBusiness.GetCustomerById(id);
            if (customer != null)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("ID: " + customer.Id);
                Console.WriteLine("FisrtName: " + customer.FisrtName);
                Console.WriteLine("LastName: " + customer.LastName);
                Console.WriteLine("Address: " + customer.Address);
                Console.WriteLine(new string('-', 40));
            }
            else
            {
                Console.WriteLine("Customer not found!");
            }

        }
        private async Task Delete()
        {
            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine());
            Customer customer = await customerBusiness.GetCustomerById(id);
            if (customer != null)
            {
                customerBusiness.DeleteCustomer(id);
                Console.WriteLine("The customer has been deleted!");
            }
            else
            {
                Console.WriteLine("Customer not found!");
            }
        }
    }
}
