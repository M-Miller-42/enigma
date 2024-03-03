using EnigmaLib;

public static class EnigmaConsole
{
    private const string EnigmaNullMsg = "No enigma created yet. Use the `!create` command.";
    private const bool ExitByEndOfFile = false;
    private static bool isRunning = false;

    public static Enigma? Enigma { get; set; }
    public static void Main()
    {
        isRunning = true;
        Console.WriteLine("Waiting for input, use the command `!info` to see the current state.");
        while (isRunning)
        {
            Console.WriteLine();
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (input == null)
            {
                if (ExitByEndOfFile)
                    break;
                else
                    continue;
            }
            ParseLine(input);
        }
    }



    public static void ParseLine(string inputLine)
    {
        string[] tokensArray = inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        Queue<string> tokens = new(tokensArray);
        string cmd = tokens.First().ToLower();

        switch (cmd)
        {
            case "!create":
                {
                    CreateEnigma(tokens);
                    break;
                }
            case "!rotate":
                {
                    Rotate(tokens);
                    break;
                }
            case "!info":
                {
                    Info();
                    break;
                }
            case "!exit":
                {
                    Environment.Exit(0);
                    break;
                }
            default:
                {
                    Encode(inputLine);
                    break;
                }
        }
    }


    public static void Rotate(Queue<string> command)
    {
        if (Enigma == null)
        {
            Console.WriteLine(EnigmaNullMsg);
            return;
        }

        Rotor rotor;
        command.Dequeue();
        if (!command.Any())
            return;
        rotor = Enigma.GetRotor(int.Parse(command.Dequeue()));
        rotor.Index = 0;

        if (!command.Any())
            return;
        rotor.Index = int.Parse(command.Dequeue());

        if (!command.Any())
            return;
        rotor.TickPos = int.Parse(command.Dequeue());
    }

    public static void Info()
    {
        if (Enigma == null)
            Console.WriteLine(EnigmaNullMsg);
        else
            Console.WriteLine(Enigma);
    }

    private static void CreateEnigma(Queue<string> command)
    {
        const string syntaxError = "Expecting arguments of the form `<rotorCount> <rotorData> <reflectorInv> <patchBoard>`\n" +
                "where <rotorData> is `random` or `<permutation> <tickPos> <index>`\n" +
                "   where <permutation> is a permutation of the alphabet e.g. `CBAFG...`\n" +
                "       or a comma separated permutation of 0,...,n-1\n" +
                "       or `random` or identity`\n" +
                "<reflectorInv> is an involution in the syntax of <permutation>,\n" +
                "<patchBoard> is a <permutation>.\n";

        command.Dequeue();
        if (command.Count < 1)
        {
            Console.WriteLine(syntaxError);
            return;
        }

        int rotCount = int.Parse(command.Dequeue());
        if (command.First() == "random")
        {
            RotorParam[] rps = new RotorParam[rotCount];
            for (int i = 0; i <= rotCount - 1; i++)
                rps[i] = new RotorParam(Permutation.Random(), 0, 0);

            Enigma = new Enigma(rps, Involution.Random(), Permutation.Random());
        }

        if (command.Count < 3 * rotCount + 2)
        {
            Console.WriteLine("Not enough arguments for {rotCount} rotors.");
            Console.WriteLine(syntaxError);
            return;
        }
        else
        {
            RotorParam[] rps = new RotorParam[rotCount];
            for (int i = 0; i <= rotCount - 1; i++)
                rps[i] = RotorParam.Parse(command.Dequeue(), command.Dequeue(), command.Dequeue());
            Involution reflector = Involution.Parse(command.Dequeue());
            Permutation patchBoard = Permutation.Parse(command.Dequeue());
            Enigma = new Enigma(rps, reflector, patchBoard);
        }

        Console.WriteLine("Enigma created.");
    }

    public static void Encode(string input)
    {
        if (Enigma == null)
        {
            Console.WriteLine(EnigmaNullMsg);
            return;
        }
        if (!Enigma.Alphabet.Contains(input))
        {
            Console.WriteLine($"Can only encode inputs from the alphabet {Enigma.Alphabet}.");
            return;
        }

        Console.WriteLine(Enigma.EncodeString(input));
    }

    public static void Reset()
    {
        Enigma = null;
    }
}

