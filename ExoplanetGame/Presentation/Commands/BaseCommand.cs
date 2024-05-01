namespace ExoplanetGame.Presentation.Commands;

public abstract class BaseCommand
{
    public abstract void Execute();

    public BaseCommand ReadUserInputWithOptions(Dictionary<string, BaseCommand> options)
    {
        showOptions(options);
        int input = GetMenuSelection(options.Count);

        if (input == (int)ConsoleKey.F1)
        {
            return new HelpCommand(this);
        }

        if (input == (int)ConsoleKey.Escape)
        {
            return new ExitCommand();
        }

        return options.Values.ElementAt(input - 1);
    }

    private void showOptions(Dictionary<string, BaseCommand> options)
    {
        int i = 1;
        foreach (var option in options)
        {
            Console.WriteLine(i + ". " + option.Key);
            i++;
        }
    }

    public static int GetMenuSelection(int maxValue)
    {
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.F1)
            {
                Console.Clear();
                return (int)keyInfo.Key;
            }

            if (int.TryParse(keyInfo.KeyChar.ToString(), out var choice) && choice >= 1 && choice <= maxValue)
            {
                Console.Clear();
                return choice;
            }

            Console.WriteLine(keyInfo.KeyChar);
            Console.WriteLine("Invalid input. Please enter a valid option!");
        }
    }
}