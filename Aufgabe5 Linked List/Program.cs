using System;
class LinkedList
{
    Node head;

    class Node
    {
        public string text { get; set; }
        public Node Next { get; set; }
        public Node (string text)
        {
            this.text = text;
        }
    }
    public void Add(string listElement)
    {
        Node node = new Node(listElement);
        if (head == null)
        {
            head = node;
        }
        else
        {
            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = node;
        }
    }
    public string this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Node current = head; 
            int count = 0;       

           
            while (current != null && count < index)
            {
                current = current.Next; 
                count++;                
            }
            
           
            if (current == null)
            {
                throw new IndexOutOfRangeException();
            }

            
            return current.text;
        }
        set
        {
            // В блоке set будет ТОЧНО ТАКОЙ ЖЕ поиск (пункты 1, 2 и 3).
            // Только вместо "return current.text;" в самом конце 
            // мы заменим текст вагона на то, что нам передали:
            // current.text = value;

            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Node current = head; 
            int count = 0;       

            while (current != null && count < index)
            {
                current = current.Next; 
                count++;                
            }

            if (current == null)
            {
                throw new IndexOutOfRangeException();
            }

            current.text = value;
        }
    }
}
class Programm
{
    public static void Main()
    {
      
        LinkedList list = new LinkedList();

       
        list.Add("Nürnberg");
        list.Add("Croatia");
        list.Add("Budapest");

       
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Element bei Index {i}: {list[i]}");
        }
       
        list[1] = "Deutschland";

        Console.WriteLine("\nNach der Änderung:");
        Console.WriteLine($"Element bei Index 1 ist jetzt: {list[1]}");
    }
}
