using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Presentation
{
    internal class MishoHelper
    {
        public void ShowHeader(string title)
        {
            int totalWidth = 40;

            Console.WriteLine(new string('-', totalWidth));
            Console.WriteLine(title.PadLeft((totalWidth + title.Length) / 2).PadRight(totalWidth)); // Centered title
            Console.WriteLine(new string('-', totalWidth)); 
        }
        public int ReadIntInput(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= 0)
                {
                    return result;
                }
                Console.Write("Invalid input! Please enter a number:");
            }
        }
        public decimal ReadDecimalInput(string prompt)
        {
            decimal result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out result) && result >= 0)
                {
                    return result;
                }
                Console.WriteLine("Invalid input! Please enter a non-negative decimal number:");
            }
        }
        public string ReadStringInput(string prompt)
        {
            string input;
            while (true)
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please enter a valid string.");
                    continue;
                }

                return input;
            }
        }
        public DateTime ReadDateInput(string prompt)
        {
            DateTime dateInput;
            while (true)
            {
                Console.WriteLine($"{prompt} (dd-MM-yyyy)");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please enter a valid date.");
                    continue;
                }

                if (DateTime.TryParseExact(input, "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out dateInput))
                {
                    return dateInput;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter the date in dd-MM-yyyy format.");
                }
            }
        }
        public string ReadPhoneInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Phone number cannot be empty.");
                    continue;
                }

                bool isAllDigits = input.All(char.IsDigit);

                if (isAllDigits && input.Length >= 7 && input.Length <= 15)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Phone must contain only digits and be 7–15 characters long.");
                }
            }
        }
        public string ReadGmailInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Email cannot be empty.");
                    continue;
                }

                if (GmailCheck(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Email must end with @gmail.com.");
                }
            }
        }
        static bool GmailCheck(string input)
        {
            input = input.Trim();
            string lower = input.ToLower();

            for (int i = 0; i < lower.Length; i++)
            {
                char c = lower[i];

                if (c == ' ')
                {
                    return false;
                }

                if (c == '@')
                {
                    if (i == 0)
                    { return false; }

                    string domain = lower.Substring(i + 1);

                    return domain == "gmail.com";
                }
            }
            return false;
        }
    }
}
