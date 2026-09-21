using System;
using System;

class Clipboard
{
    private string[] storage; 
    private int count;        

  
    public Clipboard(int size)
    {
        storage = new string[size];
        count = 0; // Сначала буфер пуст
    }


    public void Copy(string text)
    {
        if (count < storage.Length)
        {
            
            storage[count] = text;
            count++;
        }
        else
        {
            
            for (int i = 1; i < storage.Length; i++)
            {
                storage[i - 1] = storage[i];
            }
            
            storage[storage.Length - 1] = text;
        }
    }

   
    public string Paste()
    {
        if (count == 0)
        {
            return null; // Буфер пуст
        }

        
        string lastElement = storage[count - 1];

      
        count--;

        return lastElement;
    }

    public override string ToString()
    {
        if (count == 0)
        {
            return "Die Zwischenablage ist leer.";
        }

        string result = "";
       
        for (int i = count - 1; i >= 0; i--)
        {
            result += storage[i] + "\n";
        }
        return result;
    }
}
