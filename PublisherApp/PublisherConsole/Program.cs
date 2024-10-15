// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!"
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

//GetAuthors();
//AddAuthor();
//AddAuthor("Polina","APANASIUK");
//AddAuthor("Kyrylo", "APANASYUK");
//GetAuthors();
Console.WriteLine("----------------------------------");
//AddAuthorWithBook();
GetAuthorWithBooks();


void GetAuthors() { 
    using var context = new PubContext();
    var authors=context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine($"{author.Id} - {author.FirstName} {author.LastName}");
    }
}

void AddAuthor(string FirstName,string LastName) {
    var author = new Author();
    author = new Author { FirstName = FirstName, LastName = LastName };
    using var context = new PubContext();
    context.Authors.Add(author);
    // context.SaveChanges();
    //author = new Author { FirstName = "Josie", LastName = "Newf" };
    //context.Authors.Add(author);
    context.SaveChanges();
}

void AddAuthorWithBook ()
{
    var author = new Author { FirstName = "Julie", LastName = "Lerman" };
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework",
        PublishDate = new DateTime(2009, 1, 1)
    });
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework 2nd Ed",
        PublishDate = new DateTime(2010, 8, 1)
    });
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();

    author = new Author { FirstName = "Anatolii", LastName = "APANASIUK" };
    author.Books.Add(new Book
    {
        Title = "Star Wars. Raising of Epic Force Users. Part I.",
        PublishDate = new DateTime(2025, 1, 1)
    });

    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthorWithBooks()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();
    foreach (var author in authors)
    {
        Console.WriteLine($"{author.Id} - {author.FirstName} {author.LastName}");
        foreach (var book in author.Books)
        {
            Console.WriteLine($" * {book.Title}");
        }
    }
}