using SolarFarm.Core.DTO;
using System;

namespace SolarFarm
{
    class ConsoleIO
    {
        public int GetInt(string prompt)
        {
            int result = -1;
            bool valid = false;
            while (!valid)
            {
                Console.Write(prompt);
                if (!int.TryParse(Console.ReadLine(), out result))
                {
                    Error("Please input a proper integer\n\n");
                }
                else
                {
                    valid = true;
                }
            }
            return result;
        }

        public string GetString(string prompt)
        {
            string result = "";
            bool valid = false;
            while (!valid)
            {
                Console.Write(prompt);
                result = Console.ReadLine().Trim();
                if (result.Length == 0)
                {
                    Error("Please input a response\n\n");
                }
                else
                {
                    valid = true;
                }
            }
            return result;
        }

        public void Display(string message)
        {
            Console.WriteLine(message);
        }
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Display(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void DisplayTitle(string message)
        {
            string lineout = $"* {message} *";
            string bar = new string('=', lineout.Length);

            Display(bar);
            Display(lineout);
            Display(bar);
            Display("");
        }
        public SolarPanelRecord CreateRecord(string prompt)
        {
            bool valid = false;
            SolarPanelRecord record = new();
            while (!valid)
            {
                record.Section = GetString("Section: ");
                record.Row = GetInt("Row: ");
                record.Column = GetInt("Column: ");
                record.Material = GetMaterial("Material: ");
                record.Year = GetInt("Installation Year: ");
                record.IsTracking = YesOrNo("Tracked [y/n]: ").ToLower();

                valid = true;
            }
            return record;
        }
        public MaterialMode GetMaterial(string prompt)
        {
            MaterialMode mode = new();
            string material = GetString(prompt);

            if (Enum.IsDefined(typeof(MaterialMode), material))
            {
                mode = Enum.Parse<MaterialMode>(material);

            }
            return mode;
        }
        public string YesOrNo(string prompt)
        {
            string result = "";
            bool valid = false;
            while (!valid)
            {
                Console.Write(prompt);
                result = Console.ReadLine().ToLower();
                if (result == "y")
                {
                    return result = "yes";
                    valid = true;
                }
                else if (result == "n")
                {
                    return result = "no";
                    valid = true;
                }
                else
                {
                    Error("Invalid input. Try again!");
                    valid = false;
                }

            }
            return result;

        }
        public int GetIntOrNull(string prompt)
        {
            int value;

            while (true)
            {
                Console.Write(prompt);

                string input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    return value;
                }
                else
                {
                    return -1;
                }
            }
        }
        public string GetStringOrNull(string prompt)
        {

            while (true)
            {
                Console.Write(prompt);

                string input = Console.ReadLine();

                if (input == "")
                {
                    return input;
                }
                else
                {
                    return input;
                }
            }
        }
        public MaterialMode GetMaterialOrNull(string prompt)
        {
            MaterialMode mode = new();

            while (true)
            {
                Console.Write(prompt);

                string material = Console.ReadLine();

                if (Enum.IsDefined(typeof(MaterialMode), material))
                {
                    mode = Enum.Parse<MaterialMode>(material);
                    return mode;

                }
                else
                {

                    return mode = 0;
                }
            }
        }

        public string YesOrNoOrNull(string prompt)
        {
            string result = "";
            bool valid = false;
            while (!valid)
            {
                Console.Write(prompt);
                result = Console.ReadLine();
                if (result == "Y")
                {
                    return result = "Yes";
                    valid = true;
                }
                else if (result == "N")
                {
                    return result = "No";
                    valid = true;
                }
                else
                {
                    return result = "";
                    valid = true;
                }

            }
            return result;

        }

    }
}
