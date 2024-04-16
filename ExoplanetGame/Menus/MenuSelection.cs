﻿using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Menus
{
    public class MenuSelection
    {
        public static int GetMenuSelection(int minValue, int maxValue)
        {
            int choice;
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.F1)
                {
                    Console.Clear();
                    return (int)keyInfo.Key;
                }

                if (int.TryParse(keyInfo.KeyChar.ToString(), out choice) && choice >= minValue && choice <= maxValue)
                {
                    Console.Clear();
                    return choice;
                }

                Console.WriteLine(keyInfo.KeyChar);
                Console.WriteLine("Invalid input. Please enter a valid option!");
            }
        }

    }
}