using System;

namespace Advent
{
    //Day 1 ; Part 1
    class RocketEquation : IPuzzle
    {
        public string Solve(string input)
        {
            var sum = 0;
            foreach (var line in input.Split(Environment.NewLine))
            {
                var mass = int.Parse(line);
                var fuelNeeded = CalculateFuel(mass);
                Console.WriteLine(fuelNeeded);
                sum += fuelNeeded;
            }

            return sum.ToString();
        }

        protected virtual int CalculateFuel(int mass)
        {
            return mass / 3 - 2;
        }
    }
}
