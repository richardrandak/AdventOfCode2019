using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day6 : IPuzzle
    {
        public string SolvePart1(string input)
        {
            List<Planet> planets = new List<Planet>();

            var orbits = input.Split(Environment.NewLine);
            foreach (var str in orbits)
            {
                var p1 = str.Split(")")[0];
                var p2 = str.Split(")")[1];

                AddPlanet(planets, p2).Orbiting.Add(AddPlanet(planets, p1));
            }

            return planets.Sum(p => p.CountOrbits()).ToString();
        }

        public string SolvePart2(string input)
        {
            List<Planet> planets = new List<Planet>();

            var orbits = input.Split(Environment.NewLine);
            foreach (var str in orbits)
            {
                var p1 = str.Split(")")[0];
                var p2 = str.Split(")")[1];

                AddPlanet(planets, p2).Orbiting.Add(AddPlanet(planets, p1));
            }

            return CalculatePath(planets, "YOU", "SAN").ToString();
        }

        private object CalculatePath(List<Planet> planets, string planetName1, string planetName2)
        {
            var you = planets.First(p => p.Name == planetName1);
            var san = planets.First(p => p.Name == planetName2);

            var roadMap1 = BuildRoadMap(you, new Dictionary<Planet, int>(), 0);
            var roadMap2 = BuildRoadMap(san, new Dictionary<Planet, int>(), 0);

            var commonOrbits = roadMap1.Keys.Intersect(roadMap2.Keys);
            var length = commonOrbits.Min(o => roadMap1[o] + roadMap2[o]);
            return length -2;
        }

        private Dictionary<Planet, int> BuildRoadMap(Planet planet, Dictionary<Planet, int> roadMap, int steps)
        {
            steps++;
            foreach (var p in planet.Orbiting) {
                if (!roadMap.ContainsKey(p))
                {
                    roadMap.Add(p, steps);
                    BuildRoadMap(p, roadMap, steps);
                }
            }
            return roadMap;
        }

        private Planet AddPlanet(List<Planet> planets, string p1)
        {
            if (planets.FirstOrDefault(p => p.Name.Equals(p1)) == null)
            {
                planets.Add(new Planet() { Name = p1 });
            }

            return planets.First(p => p.Name.Equals(p1));
        }

        class Planet
        {
            public string Name;
            public List<Planet> Orbiting = new List<Planet>();

            public int CountOrbits()
            {
                int sum = Orbiting.Count;
                foreach (var planet in Orbiting)
                    sum += planet.CountOrbits();
                return sum;
            }

        }
    }
}
