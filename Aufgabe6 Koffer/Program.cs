using System;
using System.Xml.Linq;
class Suitcase
{
    Luggage head;
    private double maxWeight;
    public double currentWeight;
    public Suitcase (double maxWeight)
    {
        this.maxWeight = maxWeight;
        currentWeight = 0;
    }
    public double Weight
    {
        get
        {
            return currentWeight; 
        }
    }

    public bool InsertLuggage(Luggage item)
    {
        
        if (currentWeight + item.weight > maxWeight)
        {
            return false; 
        }

       
        currentWeight += item.weight;

       
        item.Next = head;
        head = item;

        return true; 
    }

    public Luggage RemoveLuggage(string label)
    {
        if (head != null && head.label == label)
        {
            Luggage target = head;          
            head = head.Next;               
            currentWeight -= target.weight; 
            target.Next = null;             
            return target;                  
        }
        Luggage current = head;

        while (current.Next != null)
        {
            if (current.Next.label == label)
            {
                Luggage target = current.Next; 

               
                current.Next = current.Next.Next;

                currentWeight -= target.weight; 
                target.Next = null;             
                return target;                  
            }

            current = current.Next; 
        }
        return null;
    }
    public void GetAllLuggage()
    {
      
        Luggage current = head;

       
        if (current == null)
        {
            Console.WriteLine("Der Koffer ist leer.");
            return;
        }

    
        while (current != null)
        {
         
            Console.WriteLine($"- {current.label} ({current.weight} kg)");

           
            current = current.Next;
        }
    }
}
class Luggage
{
    public double weight { get; set; }
    public string label { get; set; }
    public Luggage Next { get; set; }

    public Luggage(double weight, string label)
    {
        this.weight = weight;
        this.label = label;
    }
}
class Program
{
    static void Main()
    {
        
        Suitcase mySuitcase = new Suitcase(20.0);

        Console.WriteLine($"--- Packen wir den Koffer (Max: {mySuitcase.Weight}/20 kg) ---");

    
        mySuitcase.InsertLuggage(new Luggage(5.5, "Kleidung"));
        mySuitcase.InsertLuggage(new Luggage(3.2, "Schuhe"));
        mySuitcase.InsertLuggage(new Luggage(10.0, "Bücher"));

        // Эта вещь не должна поместиться (5.5 + 3.2 + 10 + 4.5 = 23.2 кг -> перебор!)
        bool fits = mySuitcase.InsertLuggage(new Luggage(4.5, "Kosmetik"));
        Console.WriteLine($"Passt Kosmetik rein? {fits} (Aktuelles Gewicht: {mySuitcase.Weight} kg)");

        Console.WriteLine("\nInhalt des Koffers:");
        mySuitcase.GetAllLuggage();

      
        Console.WriteLine("\n--- Wir nehmen 'Schuhe' raus ---");
        Luggage removed = mySuitcase.RemoveLuggage("Schuhe");
        if (removed != null)
        {
            Console.WriteLine($"Entfernt: {removed.label} ({removed.weight} kg)");
        }

        Console.WriteLine($"\nNeues Gesamtgewicht: {mySuitcase.Weight} kg");
        Console.WriteLine("\nInhalt nach dem Entfernen:");
        mySuitcase.GetAllLuggage();
    }
}

