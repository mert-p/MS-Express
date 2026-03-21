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
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= 0)
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a number.");
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
    }
}
