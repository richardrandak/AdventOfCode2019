using System;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            IPuzzle puzzle = new RocketEquationAdvanced();

            Console.WriteLine("====TEST====");
            var testAnswer = puzzle.Solve(Resource.TestInput);
            Console.WriteLine("Answer: " + testAnswer);

            Console.WriteLine("====REAL====");
            var answer = puzzle.Solve(Resource.Input);
            Console.WriteLine("Answer: " + answer);

            Console.ReadLine();
        }
    }
}
