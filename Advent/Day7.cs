using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    class Day7 : IPuzzle
    {
        public string SolvePart1(string input)
        {
            var permutations = Permutations(new List<int> { 0,1,2,3,4 });
            var instructions = input.Split(",").Select(x => int.Parse(x));

            int maxOutput = int.MinValue;
            foreach (var sequence in permutations.Select(c => c.ToList()))
            {
                var amps = new List<AmplifierProgram>();
                for (int i = 0; i < 5; i++)
                {
                    amps.Add(new AmplifierProgram()
                    {
                        prog = instructions.ToList(),
                        settings = sequence[i],
                        input = new BlockingCollection<int>(),
                        output = new BlockingCollection<int>()
                    });
                }

                amps[0].input.Add(0);
                for (int i = 0; i < 5; i++)
                {
                    amps[i].Run();

                    if (!amps[i].Equals(amps.Last()))
                        amps[i + 1].input.Add(amps[i].output.Take());
                }
                
                var output = amps.Last().output.Take();

                Console.WriteLine("out:" + output);
                if (output > maxOutput)
                    maxOutput = output;
            }
            return maxOutput.ToString();
        }

        public string SolvePart2(string input)
        {
            var permutations = Permutations(new List<int> { 5, 6, 7, 8, 9 });
            var instructions = input.Split(",").Select(x => int.Parse(x));

            var maxOutput = int.MinValue;
            foreach (var sequence in permutations.Select(c => c.ToList()))
            {
                var amps = new List<AmplifierProgram>();

                for (int i = 0; i < 5; i++)
                {
                    amps.Add(new AmplifierProgram()
                    {
                        prog = instructions.ToList(),
                        settings = sequence[i],
                        input = new BlockingCollection<int>()
                    });
                }

                for (int i = 0; i < 5; i++)
                    amps[i].output = i != 4 ? amps[i + 1].input : amps[0].input;

                amps[0].input.Add(0);

                List<Task> tasks = new List<Task>();
                for (int i = 0; i < 5; i++)
                {
                    tasks.Add(new Task(amps[i].Run));
                    tasks[i].Start();
                }

                Task.WaitAll(tasks.ToArray());

                var output = amps[0].input.Take();
                Console.WriteLine(output);

                if (output > maxOutput)
                    maxOutput = output;
            }
            return maxOutput.ToString();
        }

        public static ICollection<ICollection<T>> Permutations<T>(ICollection<T> list)
        {
            var result = new List<ICollection<T>>();
            if (list.Count == 1)
            { // If only one possible permutation
                result.Add(list); // Add it and return it
                return result;
            }
            foreach (var element in list)
            { // For each element in that list
                var remainingList = new List<T>(list);
                remainingList.Remove(element); // Get a list containing everything except of chosen element
                foreach (var permutation in Permutations<T>(remainingList))
                { // Get all possible sub-permutations
                    permutation.Add(element); // Add that element
                    result.Add(permutation);
                }
            }
            return result;
        }

        class AmplifierProgram
        {
            public List<int> prog = null;

            public int settings;
            private bool settingsUsed = false;

            public BlockingCollection<int> input;
            public BlockingCollection<int> output;           

            public void Run()
            {
                int index = 0;

                while (prog[index] != 99)
                {
                    var inst = prog[index];
                    var argsMode = GetModes(inst).Select(c => int.Parse("" + c)).ToArray();

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
                            var input = ReadInput();
                            Store(input, prog[index + 1]);
                            index += 2;
                            break;
                        case 4:
                            var output = argsMode[0] == 0 ? Read(prog[index + 1]) : prog[index + 1];
                            WriteOutput(output);
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
            }

            private int ReadInput()
            {
                if (!settingsUsed)
                {
                    settingsUsed = true;
                    return settings;
                }
                else
                    return input.Take();
            }

            private void WriteOutput(int v)
            {
                output.Add(v);
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
                    var mode = index < str.Length ? int.Parse(str[index] + "") : 0;
                    modes[i] = mode;
                }
                return modes;
            }
        }
    }
}
