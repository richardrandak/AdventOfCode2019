using System.Linq;

namespace Advent
{
    class AlarmAdvanced : Alarm
    {
        public override string Solve(string input)
        {
            for (int i = 0; i < 99; i++)
                for (int j = 0; j < 99; j++)
                {
                    var prog = input.Split(",").Select(x => int.Parse(x)).ToList();
                    var result = RunProgram(prog, i, j);
                    if (result == 19690720)
                        return (100 * i + j).ToString();
                }

            return null;
        }
    }
}
