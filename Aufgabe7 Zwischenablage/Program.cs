using System;
using System;

class Clipboard
{
    private string[] storage; // Наш массив-буфер
    private int count;        // Сколько элементов сейчас реально занято

    // 1) Конструктор
    public Clipboard(int size)
    {
        storage = new string[size];
        count = 0; // Сначала буфер пуст
    }

    // 2) Метод Copy (Добавление в буфер)
    public void Copy(string text)
    {
        if (count < storage.Length)
        {
            // Место есть — просто кладем в конец
            storage[count] = text;
            count++;
        }
        else
        {
            // Места нет! Сдвигаем всё влево, стирая самый старый элемент на индексе 0
            for (int i = 1; i < storage.Length; i++)
            {
                storage[i - 1] = storage[i];
            }
            // Теперь последняя ячейка свободна — пишем туда
            storage[storage.Length - 1] = text;
        }
    }

    // 3) Метод Paste (Забираем последнее добавленное)
    public string Paste()
    {
        if (count == 0)
        {
            return null; // Буфер пуст
        }

        // Последний элемент лежит на индексе count - 1
        string lastElement = storage[count - 1];

        // Уменьшаем счетчик (удаляем элемент из логики буфера)
        count--;

        return lastElement;
    }

    // 4) Переопределение метода ToString()
    public override string ToString()
    {
        if (count == 0)
        {
            return "Die Zwischenablage ist leer.";
        }

        string result = "";
        // Идем с конца (от самых свежих) к началу
        for (int i = count - 1; i >= 0; i--)
        {
            result += storage[i] + "\n";
        }
        return result;
    }
}
