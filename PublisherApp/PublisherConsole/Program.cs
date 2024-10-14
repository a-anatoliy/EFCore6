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
GetAuthors();

void GetAuthors() { 
    using var context = new PubContext();
    var authors=context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}

void AddAuthor() {
    var author = new Author { FirstName = "Anatolii", LastName = "APANASIUK" };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
    author = new Author { FirstName = "Josie", LastName = "Newf" };
    context.Authors.Add(author);
    context.SaveChanges();
}