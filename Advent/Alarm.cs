using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Alarm : IPuzzle
    {
        public virtual string Solve(string input)
        {
            var prog = input.Split(",").Select(x => int.Parse(x)).ToList();
            return RunProgram(prog, 12, 2).ToString();
        }

        protected int RunProgram(List<int> prog, int arg1, int arg2)
        {
            prog[1] = arg1;
            prog[2] = arg2;

            for (int i = 0; i < prog.Count; i += 4)
            {
                if (prog[i] == 99) break;
                if (prog[i] == 1)
                    prog[prog[i + 3]] = prog[prog[i + 1]] + prog[prog[i + 2]];
                else if (prog[i] == 2)
                    prog[prog[i + 3]] = prog[prog[i + 1]] * prog[prog[i + 2]];
            }

            return prog[0];
        }
    }
}
