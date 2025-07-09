using System;
using System.IO;
using System.Globalization;

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("На вход программы передаются два пути к файлам: circle.txt points.txt");
            return;
        }
        
        var circleLines = File.ReadAllLines(args[0]);
        var centerCoords = circleLines[0].Split(' ');
        var circlePos = new []{ double.Parse(centerCoords[0], CultureInfo.InvariantCulture), double.Parse(centerCoords[1], CultureInfo.InvariantCulture) };
        var radius = double.Parse(circleLines[1], CultureInfo.InvariantCulture);
        var pointsLines = File.ReadAllLines(args[1]);
        foreach (var line in pointsLines)
        {
            var coords = line.Split(' ');
            var pointPos = new [] {double.Parse(coords[0], CultureInfo.InvariantCulture), double.Parse(coords[1], CultureInfo.InvariantCulture)};
            Console.WriteLine(PowerOfPoint(pointPos, circlePos, radius));
        }
    }

    public static int PowerOfPoint(double[] pointPos, double[] circlePos, double r)
    {
        var d = Math.Sqrt(Math.Pow(pointPos[0] - circlePos[0], 2) + Math.Pow(pointPos[1] - circlePos[1], 2));
        if (d < r)
        {
            return 1; // точка внутри
        }
        else if (Math.Abs(d - r) < 1e-6)
        {
            return 0; // точка лежит на окружности
        }
        return 2; // точка снаружи
    }
}