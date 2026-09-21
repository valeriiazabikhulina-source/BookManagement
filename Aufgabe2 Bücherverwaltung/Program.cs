using System;
class Book
{
    private string author;
    private string titel;
    private int pageNum;

    public Book (string author, string titel, int pageNum)
    {
        this.author = author;
        this.titel = titel;
        this.pageNum = pageNum;
    }

    public string Author
    {
        get { return author; }
    }

    public string Titel
    {
        get { return titel; }
    }

    public int PageNum
    {
        get { return pageNum; }
    }
}
class BookManagement
{
    private Book[] books;
    private int count;
    public BookManagement()
    {
        books = new Book[1];
        count = 0;
    }

    //Bücherverwaltung muss beliebig viele Bücher aufnehmen können
    private void resize()
    {
        Book[] newArray = new Book[books.Length + 1];

        for (int i = 0; i < books.Length; i++)
        {
           newArray[i] = books[i];
        }

        books = newArray;
    }

    //ein Buch hinzuzufügen
    public void addBook(Book newBook)
    {
        if (count == books.Length)
            resize();

        books[count] = newBook;
        count++;
    }

    //alle Bücher mit Titel, Autor und Seitenzahl auf der Konsole auszugeben
    public void allBooks()
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Titel: {books[i].Titel} Author: {books[i].Author} (Pages: {books[i].PageNum}");
        }
    }

    //alle Bücher eines Autors auf der Konsole auszugeben 
    public void authorBooks(string searchAuthor)
    {
        Console.WriteLine($"All books by: {searchAuthor}");
        bool atleastOne = false; 

        for (int i = 0; i < count; i++)
        {
            if (books[i].Author == searchAuthor)
            {
                Console.WriteLine($"Titel: {books[i].Titel} (Pages: {books[i].PageNum})");
                atleastOne = true;
            }
        }

        if (atleastOne == false)
        {
            Console.WriteLine("Sorry, our book management system doesn't have any books by this author yet");
        }
    }

    //alle Bücher eines Autors zu entfernen
    public void authorDelete(string chooseAuthor)
    {
        bool atleastOne = false;
        for (int i = 0; i < count; i++)
        {
            if (books[i].Author == chooseAuthor)
            {
                atleastOne = true;
            }
        }

        if (atleastOne == false) 
        {
            Console.WriteLine($"Sorry, our book management system doesn't have any books by this author yet.");
            return; 
        }

        Book[] tempArray = new Book[books.Length];
        int newCount = 0;

        for (int i = 0; i < count; i++)
        {
            if (books[i].Author != chooseAuthor)
            {
                tempArray[newCount] = books[i];
                newCount++;
            }
        }

        books = tempArray;
        count = newCount;

        Console.WriteLine("We deleted all books by this author.");
    }

    // Indexer verfügen, der bei Übergabe eines Autors
    //ein Array mit allen von diesem Autor verfassten Büchern zurückliefert
    public Book[] this[string searchAuthor]
    {
        get
        {
            int numBooks = 0;
            for (int i = 0; i < count; i++)
            {
                if (books[i].Author == searchAuthor)
                {
                    numBooks++;
                }
            }

            Book[] authorBooks = new Book[numBooks];
            int newArray = 0;

            for (int i = 0; i < count; i++)
            {
                if (books[i].Author == searchAuthor)
                {
                    authorBooks[newArray] = books[i];
                    newArray++; 
                }
            }
            return authorBooks;
        }
    }
}



