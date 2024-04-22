namespace ExoplanetGame.Menus
{
    public class MenuSelection
    {
        public static int GetMenuSelection(int minValue, int maxValue, bool withHelp)
        {
            int choice;
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (withHelp && (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.F1))
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