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

            Node current = head; // Встаем в начало поезда
            int count = 0;       // Это наш счетчик: мы сейчас на 0-м вагоне

            // 2. Шагаем по вагонам, пока не дойдем до нужного индекса
            // И пока поезд вообще не кончился (current != null)
            while (current != null && count < index)
            {
                current = current.Next; // Переходим к следующему вагону
                count++;                // Увеличиваем счетчик шагов
            }
            // 3. Проверяем, куда мы пришли после цикла.
            // Если мы дошли до конца поезда, а нужный индекс так и не нашли
            // (например, в поезде 3 вагона, а попросили 10-й), то current станет null
            if (current == null)
            {
                throw new IndexOutOfRangeException();
            }

            // 4. Если всё хорошо и мы стоим на нужном вагоне — просто отдаем его текст!
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
        // 1. Создаем пустой список
        LinkedList list = new LinkedList();

        // 2. Добавляем элементы в конец списка
        list.Add("Nürnberg");
        list.Add("Croatia");
        list.Add("Budapest");

        // 3. Выводим элементы на экран с помощью индексатора
        // Мы добавили 3 элемента, значит индексы будут 0, 1, 2
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Element bei Index {i}: {list[i]}");
        }
        // 4. (Опционально) Давай протестируем Schreibzugriff (блок set),
        // о котором просил профессор, и изменим один элемент:
        list[1] = "Deutschland";

        Console.WriteLine("\nNach der Änderung:");
        Console.WriteLine($"Element bei Index 1 ist jetzt: {list[1]}");
    }
}