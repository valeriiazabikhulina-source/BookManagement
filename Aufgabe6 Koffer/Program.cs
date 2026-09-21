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
            return currentWeight; // просто отдаем наше внутреннее число
        }
    }

    public bool InsertLuggage(Luggage item)
    {
        // 1. Проверяем, не будет ли перевеса
        if (currentWeight + item.weight > maxWeight)
        {
            return false; // Вещь слишком тяжелая, отказываем
        }

        // 2. Увеличиваем текущий вес чемодана
        currentWeight += item.weight;

        // 3. Кладем вещь в начало связного списка
        item.Next = head;
        head = item;

        return true; // Успешно добавлено!
    }

    public Luggage RemoveLuggage(string label)
    {
        if (head != null && head.label == label)
        {
            Luggage target = head;          // Запоминаем эту вещь, чтобы вернуть её в конце
            head = head.Next;               // Назначаем новой головой СЛЕДУЮЩУЮ вещь
            currentWeight -= target.weight; // Уменьшаем вес чемодана!
            target.Next = null;             // Отцепляем вещь от списка
            return target;                  // Возвращаем её наружу
        }
        Luggage current = head;

        // Пока за нами кто-то стоит
        while (current.Next != null)
        {
            // Проверяем: у следующей вещи то самое имя?
            if (current.Next.label == label)
            {
                Luggage target = current.Next; // Запоминаем вещь, которую удаляем

                // Перекидываем мостик В ОБХОД неё (к её соседу)
                current.Next = current.Next.Next;

                currentWeight -= target.weight; // Не забываем вычесть вес
                target.Next = null;             // Отцепляем её окончательно
                return target;                  // Возвращаем найденную вещь!
            }

            current = current.Next; // Идем дальше, если не нашли
        }
        return null;
    }
    public void GetAllLuggage()
    {
        // Встаем на самую первую вещь в чемодане
        Luggage current = head;

        // Если чемодан совсем пустой, можно сразу об этом сказать
        if (current == null)
        {
            Console.WriteLine("Der Koffer ist leer.");
            return;
        }

        // Идем по цепочке, пока вещи не кончатся
        while (current != null)
        {
            // Выводим название текущей вещи
            Console.WriteLine($"- {current.label} ({current.weight} kg)");

            // Переходим к следующей вещи
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
        // Создаем чемодан с лимитом 20 кг
        Suitcase mySuitcase = new Suitcase(20.0);

        Console.WriteLine($"--- Packen wir den Koffer (Max: {mySuitcase.Weight}/20 kg) ---");

        // Пробуем положить вещи
        mySuitcase.InsertLuggage(new Luggage(5.5, "Kleidung"));
        mySuitcase.InsertLuggage(new Luggage(3.2, "Schuhe"));
        mySuitcase.InsertLuggage(new Luggage(10.0, "Bücher"));

        // Эта вещь не должна поместиться (5.5 + 3.2 + 10 + 4.5 = 23.2 кг -> перебор!)
        bool fits = mySuitcase.InsertLuggage(new Luggage(4.5, "Kosmetik"));
        Console.WriteLine($"Passt Kosmetik rein? {fits} (Aktuelles Gewicht: {mySuitcase.Weight} kg)");

        Console.WriteLine("\nInhalt des Koffers:");
        mySuitcase.GetAllLuggage();

        // Тестируем удаление вещи
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

