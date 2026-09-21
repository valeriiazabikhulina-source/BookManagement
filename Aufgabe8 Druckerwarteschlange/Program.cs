using System;

class PrinterQueue
{
    private PrintJob head;
    private PrintJob tail;

    // 1) Метод вставки с учетом приоритета (1 - топ, 10 - хвост)
    public void InsertPrintJob(string text, int priority)
    {
        PrintJob newJob = new PrintJob(text, priority);

        // Случай 1: Очередь пуста
        if (head == null)
        {
            head = newJob;
            tail = newJob;
            return;
        }

        // Случай 2: Новый элемент имеет высший приоритет, чем текущая голова
        if (priority < head.Priority)
        {
            newJob.Next = head;
            head.Prev = newJob;
            head = newJob;
            return;
        }

        // Ищем место для вставки в середине или в конце
        PrintJob current = head;
        while (current != null && current.Priority <= priority)
        {
            current = current.Next;
        }

        // Случай 3: Вставка в самый конец (current дошел до null)
        if (current == null)
        {
            tail.Next = newJob;
            newJob.Prev = tail;
            tail = newJob;
        }
        // Случай 4: Вставка в середину (перед узлом current)
        else
        {
            PrintJob previous = current.Prev;

            newJob.Next = current;
            newJob.Prev = previous;

            previous.Next = newJob;
            current.Prev = newJob;
        }
    }

    // 2) Метод печати (удаление самого первого элемента)
    public void Print()
    {
        if (head == null)
        {
            Console.WriteLine("Keine Druckaufträge in der Warteschlange.");
            return;
        }

        // Выводим текст на консоль
        Console.WriteLine($"Drucke: {head.Text} (Priorität: {head.Priority})");

        // Сдвигаем голову вперед
        head = head.Next;

        if (head != null)
        {
            head.Prev = null; // У новой головы больше нет предыдущего элемента
        }
        else
        {
            tail = null; // Если очередь стала пустой, обнуляем и хвост
        }
    }

    // 3) Метод удаления заданий с низким приоритетом
    public int RemoveLowPriorityJobs(int priority)
    {
        int removedCount = 0;
        PrintJob current = head;

        while (current != null)
        {
            // Сохраняем ссылку на следующий узел заранее, 
            // так как текущий мы можем сейчас удалить
            PrintJob nextNode = current.Next;

            // Если число приоритета больше или равно заданному (т.е. важность ниже)
            if (current.Priority >= priority)
            {
                removedCount++;

                // Если удаляем голову
                if (current == head)
                {
                    head = current.Next;
                    if (head != null) head.Prev = null;
                }
                // Если удаляем хвост
                else if (current == tail)
                {
                    tail = current.Prev;
                    if (tail != null) tail.Next = null;
                }
                // Если удаляем из середины
                else
                {
                    current.Prev.Next = current.Next;
                    current.Next.Prev = current.Prev;
                }
            }

            current = nextNode;
        }

        return removedCount;
    }
}

class PrintJob
{
    public string Text { get; set; }
    public int Priority { get; set; }
    public PrintJob Next { get; set; }
    public PrintJob Prev { get; set; }

    public PrintJob(string text, int priority)
    {
        Text = text;
        Priority = priority;
    }
}

// Тестируем логику в Main
class Program
{
    static void Main()
    {
        PrinterQueue queue = new PrinterQueue();

        // Добавляем задания в случайном порядке
        queue.InsertPrintJob("Urlaubsantrag.pdf", 5);
        queue.InsertPrintJob("Chef_Bericht.docx", 1); // Самый важный, пойдет вперед
        queue.InsertPrintJob("Katzenfoto.png", 10);   // Низкий приоритет, пойдет в хвост
        queue.InsertPrintJob("Skript_Informatik.pdf", 5);

        Console.WriteLine("--- Первые две печати (Ожидаем Chef_Bericht и затем Urlaubsantrag) ---");
        queue.Print();
        queue.Print();

        Console.WriteLine("\n--- Удаляем все задания с приоритетом 8 и хуже (наше Katzenfoto) ---");
        int removed = queue.RemoveLowPriorityJobs(8);
        Console.WriteLine($"Удалено неважных заданий: {removed}");

        Console.WriteLine("\n--- Печатаем то, что осталось ---");
        queue.Print(); // Должен напечатать Skript_Informatik
        queue.Print(); // Очередь должна быть пуста
    }
}