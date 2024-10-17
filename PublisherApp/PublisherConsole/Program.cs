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


// GetAuthors();
// AddAuthor();
// AddAuthor("Polina","APANASIUK");
// AddAuthor("Kyrylo", "APANASYUK");
// AddAuthor("Rhoda","Lerman");
// AddAuthor("Don", "Jones");
// AddAuthor("Jim", "Christopher");
// AddAuthor("Stephen", "Haunts");
// GetAuthors();
// Console.WriteLine("----------------------------------");
// AddAuthorWithBook();
GetAuthorWithBooks();
// QueryFilters();
// AddSomeMoreAuthors();
// SkipAndTakeAuthors();
// SortAuthors();
// QueryAggregate();
// InsertAuthor();
//RetrieveAndUpdateAuthor();
//RetrieveAndUpdateMultipleAuthors();
//VariousOperations();
//CoordinatedRetrieveAndUpdateAuthor();
// InsertMultipleAuthors();
//BulkAddUpdate();
//GetAuthors();

void BulkAddUpdate()
{
    var newAuthors = new Author[]{
     new Author { FirstName = "Tsitsi", LastName = "Dangarembga" },
     new Author { FirstName = "Lisa", LastName = "See" },
     new Author { FirstName = "Zhang", LastName = "Ling" },
     new Author { FirstName = "Marilynne", LastName="Robinson"}
    };
    _context.Authors.AddRange(newAuthors);
    var book = _context.Books.Find(2);
    book.Title = "Programming Entity Framework 2nd Edition";
    _context.SaveChanges();
}

void InsertMultipleAuthorsPassedIn(List<Author> listOfAuthors)
{
    _context.Authors.AddRange(listOfAuthors);
    _context.SaveChanges();
}

void InsertMultipleAuthors()
{
    var newAuthors = new Author[]{
       new Author { FirstName = "Ruth", LastName = "Ozeki" },
       new Author { FirstName = "Sofia", LastName = "Segovia" },
       new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
       new Author { FirstName = "Hugh", LastName = "Howey" },
       new Author { FirstName = "Isabelle", LastName = "Allende" }
    };
    _context.AddRange(newAuthors);
    _context.SaveChanges();
}


void DeleteAnAuthor()
{
    var extraJL = _context.Authors.Find(1);
    if (extraJL != null)
    {
        _context.Authors.Remove(extraJL);
        _context.SaveChanges();
    }
}

void CoordinatedRetrieveAndUpdateAuthor()
{
    var author = FindThatAuthor(3);
    if (author?.FirstName == "Julie")
    {
        author.FirstName = "Julia";
        SaveThatAuthor(author);
    }
}

Author FindThatAuthor(int authorId)
{
    using var shortLivedContext = new PubContext();
    return shortLivedContext.Authors.Find(authorId);
}

void SaveThatAuthor(Author author)
{
    using var anotherShortLivedContext = new PubContext();
    anotherShortLivedContext.Authors.Update(author);
    anotherShortLivedContext.SaveChanges();
}

void VariousOperations()
{
    var author = _context.Authors.Find(2); //this is currently Josie Newf
    author.LastName = "Newfoundland";
    var newauthor = new Author { LastName = "Appleman", FirstName = "Dan" };
    _context.Authors.Add(newauthor);
    _context.SaveChanges();
}

void RetrieveAndUpdateMultipleAuthors()
{
    var LermanAuthors = _context.Authors.Where(a => a.LastName == "Lehrman").ToList();
    foreach (var la in LermanAuthors)
    {
        Console.WriteLine($"{la.FirstName} {la.LastName}");
        la.LastName = "Lerman";
    }

    //var a1 = LermanAuthors[0];
    //var a2 = LermanAuthors[1];
    //a1 = null;
    Console.WriteLine("Before" + _context.ChangeTracker.DebugView.ShortView);
    _context.ChangeTracker.DetectChanges();
    Console.WriteLine("After:" + _context.ChangeTracker.DebugView.ShortView);

    _context.SaveChanges();

    // LermanAuthors.RemoveAt(0);
    //_context.ChangeTracker.DetectChanges();
    // _context.SaveChanges();
    //Console.WriteLine("After:" + _context.ChangeTracker.DebugView.ShortView);
}

void RetrieveAndUpdateAuthor()
{
    var author = _context.Authors.FirstOrDefault(a => a.FirstName == "Julie" && a.LastName == "Lerman");
    if (author != null)
    {
        author.FirstName = "Julia";
        _context.SaveChanges();
    }
}

void InsertAuthor () { 
    var author = new Author { FirstName = "Frank", LastName = "Herbert" };
    _context.Authors.Add(author);
    _context.SaveChanges();
}

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