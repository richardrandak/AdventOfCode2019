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

            PrintImageToFile(width, height, finalImage, @"C:\Development\AdventOfCode2019\message.bmp");
            
            return "JAFRA"; //manually read from image
        }

        private void PrintImageToFile(int width, int height, byte[] finalImage, string path)
        {
            var bmp = new Bitmap(width, height);
            var counter = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (finalImage[counter] == 1)
                        bmp.SetPixel(j, i, Color.Black);
                    else
                        bmp.SetPixel(j, i, Color.White);
                    counter++;
                }
            }
            bmp.Save(path);
        }
    }
}
