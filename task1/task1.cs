namespace task1;


public class Node
{
    public int Data { get; set; }
    public Node Next { get; set; }

    public Node(int data)
    {
        this.Data = data;
        this.Next = null;
    }
}

class CircleArray
{
    public Node Head { get; set; }

    public CircleArray()
    {
        this.Head = null;
    }

    public void AddNode(int data)
    {
        var newNode = new Node(data);
        
        if (this.Head == null)
        {
            this.Head = newNode;
            this.Head.Next = Head;
        }
        else
        {
            var current = this.Head;
            while (current.Next != Head)
            {
                current = current.Next;
            }
            current.Next = newNode;
            newNode.Next = Head;
        }
    }
    
    public void Traverse(int m)
    {
        if (Head == null)
        {
            Console.WriteLine("Список пуст");
            return;
        }

        Node current = Head;
        do
        {
            Console.Write(current.Data);
            for (var i = 1; i < m; i++)
            {
                current = current.Next;
            }
            
        } while (current != Head);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Введите n и m. Пример: 5 4");
        var input = Console.ReadLine().Split(' ');
        var n = int.Parse(input[0]);
        var m = int.Parse(input[1]);
        
        var circleArray = CreateCircleArray(n);
        circleArray.Traverse(m);
    }

    public static CircleArray CreateCircleArray(int n)
    { 
        var circleArray = new CircleArray();
        for (var i = 1; i <= n; i++)
            circleArray.AddNode(i);
        
        return circleArray;
    }
}