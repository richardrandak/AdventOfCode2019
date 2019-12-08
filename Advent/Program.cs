using System;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzle = new Day8();
            var answer = puzzle.SolvePart2(Resource.Input);
            Console.WriteLine("Answer: " + answer);
            Console.ReadLine();
        }
    }
}
