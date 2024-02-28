using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public static class EnigmaConsole
{
    private static bool isRunning = false;
    private static bool exitByEndOfFile = false;
    public static Enigma enigma;

    public static void Main()
    {
        string input;
        isRunning = true;
        Console.WriteLine("Waiting for input, use command `info` to see the current state.");
        while (isRunning)
        {
            Console.WriteLine();
            Console.Write("> ");
            input = Console.ReadLine();
            if (input == null)
            {
                if (exitByEndOfFile)
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
        Queue<string> tokens = new Queue<string>(tokensArray);
        string cmd = tokens.First().ToLower();
        try
        {
            switch (cmd)
            {
                case "create":
                    {
                        CreateEnigma(tokens);
                        break;
                    }
                case "en":
                    {
                        break;
                    }
                case "rot":
                    {
                        Rotate(tokens);
                        break;
                    }
                case "info":
                    {
                        Info();
                        break;
                    }
                case "exit":
                    {
                        System.Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Encode(tokens);
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    public static void Rotate(Queue<string> command)
    {
        Rotor rotor;
        command.Dequeue();
        if (!command.Any())
            return;
        rotor = enigma.getRotor(int.Parse(command.Dequeue()));
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
        if (enigma == null)
            Console.WriteLine("No enigma created yet! Use the `create` command.");
        else
            Console.WriteLine(enigma);
    }
    private static void CreateEnigma(Queue<string> command)
    {
        command.Dequeue();
        if (command.Count < 3)
        {
            Console.WriteLine("Expecting at least 2 arguments of the form `create <rotorCount> <rotorData>`, " +
                "where <rotorData> is a space separated list of permutations or `random`.");
            return;
        }
        int rotCount = int.Parse(command.Dequeue());
        if (command.First() == "random")
        {
            RotorParam[] rps = new RotorParam[rotCount];
            for (int i = 0; i <= rotCount - 1; i++)
                rps[i] = new RotorParam(Permutation.Random(), 0, 0);

            enigma = new Enigma(rps, Involution.Random(), Permutation.Random());
        }
        else
        {
            if (command.Count < 3 * rotCount + 2)
                throw new ArgumentOutOfRangeException("Zu wenige Argumente für create");
            RotorParam[] rps = new RotorParam[rotCount];
            for (int i = 0; i <= rotCount - 1; i++)
                rps[i] = RotorParam.Parse(command.Dequeue(), command.Dequeue(), command.Dequeue());
            Involution reflector = Involution.Parse(command.Dequeue());
            Permutation patchBoard = Permutation.Parse(command.Dequeue());
            enigma = new Enigma(rps, reflector, patchBoard);
        }
        Console.WriteLine("Enigma created.");
    }

    public static void Encode(Queue<string> split)
    {
        if (enigma == null)
            Console.WriteLine("Noch keine Enigma erstellt!");
        else if (split.Count == 1)
        {
            foreach (string s in split)
                Console.WriteLine(enigma.EncodeString(s));
        }
    }
}
