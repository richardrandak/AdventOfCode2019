using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day4 : IPuzzle
    {
        public string SolvePart1(string input)
        {
            var range = new Range(245182, 790572);

            int sum = 0;
            for (int i = range.Start.Value; i < range.End.Value; i++)
            {
                var code = i.ToString();

                var rules = new List<ICodeRule>
                {
                    new HasDoubleDigit(),
                    new HasNoDecresedDigit()
                };

                if (FollowsAllRules(code, rules))
                    sum++;
            }
            return sum.ToString();
        }

        public string SolvePart2(string input)
        {
            var range = new Range(245182, 790572);

            int sum = 0;
            for (int i = range.Start.Value; i < range.End.Value; i++)
            {
                var code = i.ToString();

                var rules = new List<ICodeRule>
                {
                    new HasPerfectDoubleDigit(),
                    new HasNoDecresedDigit(),
                };

                if (FollowsAllRules(code, rules))
                    sum++;
            }
            return sum.ToString();
        }

        private bool FollowsAllRules(string code, IList<ICodeRule> rules)
        {
            return rules.All(rules => rules.IsFollowed(code));
        }

        interface ICodeRule
        {
            bool IsFollowed(string code);
        }

        class HasDoubleDigit : ICodeRule
        {
            public bool IsFollowed(string code)
            {
                for (int i = 0; i <= 9; i++)
                {
                    if (code.Contains($"{i}{i}"))
                        return true;
                }
                return false;
            }
        }

        class HasPerfectDoubleDigit : ICodeRule
        {
            public bool IsFollowed(string code)
            {
                for (int i = 0; i <= 9; i++)
                {
                    if (code.Contains($"{i}{i}") && !code.Contains($"{i}{i}{i}"))
                        return true;
                }
                return false;
            }
        }

        class HasNoDecresedDigit : ICodeRule
        {
            public bool IsFollowed(string code)
            {
                for (int i = 1; i < code.Length; i++)
                {
                    if (code[i] < code[i - 1])
                        return false;
                }
                return true;
            }
        }
    }
}
