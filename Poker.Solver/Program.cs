using System;

namespace Evolution.Poker.Solver
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var solvingResult = Solver.Process(input);
            Console.WriteLine(solvingResult);
        }
    }
}