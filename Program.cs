using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StackingCylinders
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // http://poj.org/problem?id=2850
            
            var output = new List<string>();
            var input = await File.ReadAllLinesAsync("input.txt");
            var nProb = Convert.ToInt32(input[0]);
            for (int i = 1; i <= nProb; i++)
            {
                var (x, y) = ComputeTopCylinderCoordinates(input[i]);
                output.Add($"{i}: {x:0.0000} {y:0.0000}");
            }
            await File.WriteAllLinesAsync("output.txt", output);
            Console.WriteLine("Done!");
        }

        private static (double x, double y) ComputeTopCylinderCoordinates(string v)
        {
            var split = v.Split(' ');
            var n = Convert.ToInt32(split[0]);
            var row = new List<(double x, double y)>();
            for (int i = 1; i <= n; i++)
            {
                var x = Convert.ToDouble(split[i]);
                var y = 1.0;
                row.Add((x, y));
            }

            while (row.Count > 1)
            {
                var tmp = new List<(double x, double y)>();
                for (int i = 0; i < row.Count - 1; i++)
                {
                    var distance = Math.Sqrt(Math.Pow(row[i + 1].x - row[i].x, 2) + Math.Pow(row[i + 1].y - row[i].y, 2));
                    var alpha = Math.Acos(distance / 4);
                    var beta = Math.Atan2(row[i + 1].y - row[i].y, row[i + 1].x - row[i].x);
                    var gamma = alpha + beta;
                    var x = 2 * Math.Cos(gamma);
                    var y = 2 * Math.Sin(gamma);
                    tmp.Add((row[i].x + x, row[i].y + y));
                }
                row = tmp;
            }

            return (Math.Round(row[0].x, 4), Math.Round(row[0].y, 4));
        }
    }
}
