// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!"
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;


PubContext _context = new PubContext();
//using (PubContext context = new PubContext())
//{
//    context.Database.EnsureCreated();
//}


//GetAuthors();
//AddAuthor();
//AddAuthor("Polina","APANASIUK");
//AddAuthor("Kyrylo", "APANASYUK");
//AddAuthor("Rhoda","Lerman");
//AddAuthor("Don", "Jones");
//AddAuthor("Jim", "Christopher");
//AddAuthor("Stephen", "Haunts");
//GetAuthors();
//Console.WriteLine("----------------------------------");
//AddAuthorWithBook();
//GetAuthorWithBooks();
//QueryFilters();
//AddSomeMoreAuthors();
//SkipAndTakeAuthors();
// SortAuthors();
QueryAggregate();

void QueryAggregate()
{
    //var author = _context.Authors.OrderByDescending(a => a.FirstName).FirstOrDefault(a => a.LastName == "Lerman");
    //var author = _context.Authors.Where(a=>a.LastName =="Lerman").ToList();
    var author = _context.Authors.FirstOrDefault(a => a.LastName == "Lerman");
    //PrintAuthors(author);
}

void SortAuthors()
{
    var authorsByLastName = _context.Authors
        .OrderBy(a => a.LastName)
        .ThenBy(a => a.FirstName).ToList();
    //authorsByLastName.ForEach(a => Console.WriteLine(a.LastName + "," + a.FirstName));
    PrintAuthors(authorsByLastName);

    var authorsDescending = _context.Authors
    .OrderByDescending(a => a.LastName)
    .ThenByDescending(a => a.FirstName).ToList();
    Console.WriteLine("**Descending Last and First**");
    // authorsDescending.ForEach(a => Console.WriteLine(a.LastName + "," + a.FirstName));
    PrintAuthors(authorsDescending);
    var lermans = _context.Authors.Where(a => a.LastName == "Lerman").OrderByDescending(a => a.FirstName).ToList();
}


void AddSomeMoreAuthors()
{
    _context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
    _context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
    _context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
    _context.SaveChanges();
}

void SkipAndTakeAuthors()
{
    var groupSize = 3;
    for (int i = 0; i < 5; i++)
    {
        var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
        Console.WriteLine($"   Group {i}:");
        PrintAuthors(authors);
    }
}

void PrintAuthors (List<Author> authors) {
    foreach (var author in authors)
    {
        Console.WriteLine($"{author.Id} - {author.FirstName} {author.LastName}");
    }
}


void QueryFilters () {
    
//    string name = "Anatolii";
    var filter = "L%";
//    var authors = _context.Authors.Where(s=>s.FirstName==name).ToList();
//   PrintAuthors(authors);

//    name = "Josie";
//    authors = _context.Authors.Where(s => s.FirstName == name).ToList();
//    PrintAuthors(authors);

    //name = "Joe";
    //authors = _context.Authors.Where(s => s.FirstName == name).ToList();

    var authors = _context.Authors.Where(a=>EF.Functions.Like(a.LastName,filter)).ToList();
    PrintAuthors(authors);

}

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