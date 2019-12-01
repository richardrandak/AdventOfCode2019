namespace Advent
{
    //Day 1 ; Part 2
    class RocketEquationAdvanced : RocketEquation
    {
        protected override int CalculateFuel(int mass)
        {
            var fuel = base.CalculateFuel(mass);
            if (fuel <= 0)
                return 0;
            else
                return fuel + CalculateFuel(fuel);
        }
    }
}
