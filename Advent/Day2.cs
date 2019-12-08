using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day2 : IPuzzle
    {
        public virtual string SolvePart1(string input)
        {
            var program = new AlarmProgram() {
                prog = input.Split(",").Select(x => int.Parse(x)).ToList(),
                input = new List<int>() { 12, 2 }
            };
            return program.Run().ToString();
        }

        public string SolvePart2(string input)
        {
            for (int i = 0; i < 99; i++)
                for (int j = 0; j < 99; j++)
                {
                    var program = new AlarmProgram()
                    {
                        prog = input.Split(",").Select(x => int.Parse(x)).ToList(),
                        input = new List<int>() { i, j }
                    };

                    var result = program.Run();
                    if (result == 19690720)
                        return (100 * i + j).ToString();
                }

            return null;
        }

        class AlarmProgram
        {
            public List<int> prog = null;
            public List<int> input;

            public int Run()
            {
                prog[1] = input[0];
                prog[2] = input[1];

                int index = 0;

                while (prog[index] != 99)
                {
                    var inst = prog[index];
                    switch (inst % 100)
                    {
                        case 1:
                            Store(Read(prog[index + 1]) + Read(prog[index + 2]), prog[index + 3]);
                            index += 4;
                            break;
                        case 2:
                            Store(Read(prog[index + 1]) * Read(prog[index + 2]), prog[index + 3]);
                            index += 4;
                            break;
                    }
                }

                return prog[0];
            }

            int Read(int address)
            {
                return prog[address];
            }

            void Store(int val, int address)
            {
                prog[address] = val;
            }
        }
    }
}
