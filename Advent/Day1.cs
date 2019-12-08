using System;

namespace Advent
{
    class Day1 : IPuzzle
    {
        public string SolvePart1(string input)
        {
            var sum = 0;
            foreach (var line in input.Split(Environment.NewLine))
            {
                var mass = int.Parse(line);
                var fuelNeeded = CalculateFuel(mass);
                sum += fuelNeeded;
            }

            return sum.ToString();
        }

        public string SolvePart2(string input)
        {
            var sum = 0;
            foreach (var line in input.Split(Environment.NewLine))
            {
                var mass = int.Parse(line);
                var fuelNeeded = CalculateTotalFuel(mass);
                sum += fuelNeeded;
            }

            return sum.ToString();
        }

        private int CalculateFuel(int mass)
        {
            return mass / 3 - 2;
        }

        private int CalculateTotalFuel(int mass)
        {
            var fuel = CalculateFuel(mass);
            if (fuel <= 0)
                return 0;
            else
                return fuel + CalculateTotalFuel(fuel);
        }
    }
}
