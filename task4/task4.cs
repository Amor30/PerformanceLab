namespace task4;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("На вход программы передаются один путь к файлу: numbers.txt");
            return;
        }

        var lines = File.ReadAllLines(args[0]);
        var numbers = lines.Select(x => int.Parse(x)).ToArray();
        var avg = (int)Math.Round(numbers.Average());
        Console.WriteLine(ReductionToSingleNumber(numbers, avg));
    }
    
    public static int ReductionToSingleNumber(int[] array, int avg)
    {
        var count = 0;
        for (var i = 0; i < array.Length; i++)
        {
            while (array[i] != avg)
            {
                if (array[i] > avg)
                    array[i]--;
                else
                    array[i]++;
                count++;
            }
        }

        return count;
    }
}