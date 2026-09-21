using System;

class PrinterQueue
{
    private PrintJob head;
    private PrintJob tail;

   
    public void InsertPrintJob(string text, int priority)
    {
        PrintJob newJob = new PrintJob(text, priority);

        if (head == null)
        {
            head = newJob;
            tail = newJob;
            return;
        }

    
        if (priority < head.Priority)
        {
            newJob.Next = head;
            head.Prev = newJob;
            head = newJob;
            return;
        }

      
        PrintJob current = head;
        while (current != null && current.Priority <= priority)
        {
            current = current.Next;
        }

      
        if (current == null)
        {
            tail.Next = newJob;
            newJob.Prev = tail;
            tail = newJob;
        }
        
        else
        {
            PrintJob previous = current.Prev;

            newJob.Next = current;
            newJob.Prev = previous;

            previous.Next = newJob;
            current.Prev = newJob;
        }
    }

  
    public void Print()
    {
        if (head == null)
        {
            Console.WriteLine("Keine Druckaufträge in der Warteschlange.");
            return;
        }

    
        Console.WriteLine($"Drucke: {head.Text} (Priorität: {head.Priority})");

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

   
    public int RemoveLowPriorityJobs(int priority)
    {
        int removedCount = 0;
        PrintJob current = head;

        while (current != null)
        {
          
       
            PrintJob nextNode = current.Next;

            if (current.Priority >= priority)
            {
                removedCount++;

                if (current == head)
                {
                    head = current.Next;
                    if (head != null) head.Prev = null;
                }
              
                else if (current == tail)
                {
                    tail = current.Prev;
                    if (tail != null) tail.Next = null;
                }
              
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


class Program
{
    static void Main()
    {
        PrinterQueue queue = new PrinterQueue();

       queue.InsertPrintJob("Urlaubsantrag.pdf", 5);
       queue.InsertPrintJob("Chef_Bericht.docx", 1);
       queue.InsertPrintJob("Katzenfoto.png", 10);
       queue.InsertPrintJob("Skript_Informatik.pdf", 5);

       Console.WriteLine("--- Die ersten beiden Druckaufträge (erwartet: Chef_Bericht, danach Urlaubsantrag) ---");
       queue.Print();
       queue.Print();

       Console.WriteLine("\n--- Entferne alle Druckaufträge mit Priorität 8 oder schlechter (Katzenfoto) ---");
       int removed = queue.RemoveLowPriorityJobs(8);
       Console.WriteLine($"Anzahl der entfernten Druckaufträge: {removed}");

       Console.WriteLine("\n--- Verbleibende Druckaufträge werden gedruckt ---");
       queue.Print(); // Sollte Skript_Informatik drucken
       queue.Print(); // Die Warteschlange sollte jetzt leer sein
    }
}
