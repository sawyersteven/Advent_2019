﻿using System;
using ExtensionMethods;

namespace AdventOfCode
{
    using Console = System.Console;
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("\tdotnet run [year] [day]");
            }

            Console.WriteLine($":: Running challenge {args[0]}.{args[1]} ::");

            int challengeId = int.Parse(args[1]);
            string clsName = $"Advent{args[0]}.Challenge{challengeId.ToString("00")}";

            Challenge c = (Challenge)Activator.CreateInstance(Type.GetType(clsName));
            string input = System.IO.File.ReadAllText($"../Input/{args[0]}/{challengeId.ToString("00")}.txt");

            int i = 1;
            foreach (ChallengeResult cr in c.Go(input))
            {
                Console.WriteLine($"Task{i} -");
                Console.WriteLine($"    Time: {cr.Time}ms");
                Console.WriteLine($"    Result: {cr.Answer}");
                i++;
            }
        }
    }

    public class Utils
    {
        public static char[,] InputToCharArray(string[] input, bool addBorder = false, char defaultChar = (char)0)
        {
            char[,] grid = new char[input.Length + (addBorder ? 2 : 0), input[0].Length + (addBorder ? 2 : 0)];
            if (defaultChar != 0)
            {
                grid.Fill(defaultChar);
            }
            int y = (addBorder ? 1 : 0);
            int yEnd = input.Length + (addBorder ? 1 : 0);
            int x = (addBorder ? 1 : 0);
            int xEnd = input[0].Length + (addBorder ? 1 : 0);

            for (int gy = 0; y < yEnd; y++, gy++)
            {
                int gx = 0;
                for (gx = 0, x = (addBorder ? 1 : 0); x < xEnd; x++, gx++)
                {
                    grid[y, x] = input[gy][gx];
                }
            }
            return grid;
        }
    }
}
