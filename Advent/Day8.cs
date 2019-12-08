using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Advent
{
    class Day8 : IPuzzle
    {
        public string SolvePart1(string input)
        {
            var height = 6;
            var width = 25;
            var layerSize = height * width;
            var layersCount = input.Length / layerSize;

            string specialLayer = null;
            var minZeroes = int.MaxValue;
            for (int i = 0; i < layersCount; i++)
            {
                var layer = input.Substring(i * layerSize, layerSize);
                var zeroes = layer.Count(c => c == '0');
                if (minZeroes > zeroes)
                {
                    minZeroes = zeroes;
                    specialLayer = layer;
                }
            }

            var answer = specialLayer.Count(c => c == '1') * specialLayer.Count(c => c == '2');

            return answer.ToString();
        }

        public string SolvePart2(string input)
        {
            var height = 6;
            var width = 25;
            var layerSize = height * width;
            var layersCount = input.Length / layerSize;

            var layers = new List<string>();
            for (int i = 0; i < layersCount; i++)
                layers.Add(input.Substring(i * layerSize, layerSize));

            var finalImage = new byte[layerSize];
            for (int i = 0; i < layerSize; i++)
            {
                for (int j = 0; j < layers.Count; j++)
                    if (layers[j][i] != '2') //not transparent
                    {   
                        finalImage[i] = byte.Parse(""+layers[j][i]);
                        break;
                    }
            }

            PrintImageToConsole(width, height, finalImage);
            
            return "JAFRA"; //manually read from console
        }

        private void PrintImageToConsole(int width, int height, byte[] finalImage)
        {
            var counter = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (finalImage[counter] == 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                    counter++;
                }
                Console.WriteLine();
            }
        }
    }
}
