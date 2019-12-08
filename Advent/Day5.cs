using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day5 : IPuzzle
    {
        public virtual string SolvePart1(string input)
        {
            var program = new DiagnosticProgram() {
                prog = input.Split(",").Select(x => int.Parse(x)).ToList(),
                input = 1
            };
            return program.Run().ToString();
        }

        public string SolvePart2(string input)
        {
            var program = new DiagnosticProgram()
            {
                prog = input.Split(",").Select(x => int.Parse(x)).ToList(),
                input = 5
            };
            return program.Run().ToString();
        }

        class DiagnosticProgram
        {
            public List<int> prog = null;
            public int input;

            public int Run()
            {
                int output = 0;
                int index = 0;

                while (prog[index] != 99)
                {
                    var inst = prog[index];
                    var argsMode = GetModes(inst).Select(c => int.Parse(""+c)).ToArray();

                    switch (inst % 100)
                    {
                        case 1:
                            var arg1 = argsMode[0] == 0 ? Read(prog[index + 1]) : prog[index + 1];
                            var arg2 = argsMode[1] == 0 ? Read(prog[index + 2]) : prog[index + 2];
                            Store(arg1 + arg2, prog[index + 3]);
                            index += 4;
                            break;
                        case 2:
                            arg1 = argsMode[0] == 0 ? Read(prog[index + 1]) : prog[index + 1];
                            arg2 = argsMode[1] == 0 ? Read(prog[index + 2]) : prog[index + 2];
                            Store(arg1 * arg2, prog[index + 3]);
                            index += 4;
                            break;
                        case 3:
                            Store(input, prog[index + 1]);
                            index += 2;
                            break;
                        case 4:
                            output = argsMode[0] == 0 ? Read(prog[index + 1]) : prog[index + 1];
                            index += 2;
                            break;
                        case 5:
                            arg1 = argsMode[0] == 0 ? Read(prog[index + 1]) : prog[index + 1];
                            if (arg1 != 0)
                                index = argsMode[1] == 0 ? Read(prog[index + 2]) : prog[index + 2];
                            else
                                index += 3;
                            break;
                        case 6:
                            arg1 = argsMode[0] == 0 ? Read(prog[index + 1]) : prog[index + 1];
                            if (arg1 == 0)
                                index = argsMode[1] == 0 ? Read(prog[index + 2]) : prog[index + 2];
                            else
                                index += 3;
                            break;
                        case 7:
                            arg1 = argsMode[0] == 0 ? Read(prog[index + 1]) : prog[index + 1];
                            arg2 = argsMode[1] == 0 ? Read(prog[index + 2]) : prog[index + 2];
                            if (arg1 < arg2)
                                Store(1, prog[index + 3]);
                            else
                                Store(0, prog[index + 3]);
                            index += 4;
                            break;
                        case 8:
                            arg1 = argsMode[0] == 0 ? Read(prog[index + 1]) : prog[index + 1];
                            arg2 = argsMode[1] == 0 ? Read(prog[index + 2]) : prog[index + 2];
                            if (arg1 == arg2)
                                Store(1, prog[index + 3]);
                            else
                                Store(0, prog[index + 3]);
                            index += 4;
                            break;
                        default:
                            throw new Exception("Unsupported opperation");
                    }
                }

                return output;
            }

            int Read(int address)
            {
                return prog[address];
            }

            void Store(int val, int address)
            {
                prog[address] = val;
            }

            int[] GetModes(int inst) 
            {
                // "0103" => [001][03] => modes: { 1,0,0 }
                int MaxArgs = 3;
                var modes = new int[MaxArgs];

                var str = new string(inst.ToString().Reverse().ToArray());
                for (int i = 0; i < MaxArgs; i++)
                {
                    int index = 2 + i;
                    var mode = index < str.Length ? int.Parse(str[index]+"") : 0;
                    modes[i] = mode;
                }
                return modes;
            }
        }
    }
}
