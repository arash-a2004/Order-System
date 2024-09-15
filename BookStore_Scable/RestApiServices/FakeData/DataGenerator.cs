using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using LibraryDemo.Models;
namespace RestApiServices.FakeData
{
    public class DataGenerator
    {
        private List<Author> Authors { get; set; }
        private List<Book> Books { get; set; }
        private List<User> Users { get; set; }

        public void SeedData()
        {
            //Set the randomizer seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(100);

            var author = new Faker<Author>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode
                .RuleFor(e => e.Name, f => f.Name.FullName())
                .RuleFor(e => e.Books, f => new List<Book>());
            //Generate Fake Data
            Authors = author.Generate(100);


            var books = new Faker<Book>()
                .RuleFor(e => e.Title, f => f.PickRandom(BookTitles.bookNames))
                .RuleFor(e => e.Authors, f => new List<Author>())
                .RuleFor(e => e.Carts, f => new List<Cart>());

            Books = books.Generate(100);

            // Assign authors to books
            foreach (var book in Books) 
            {
                var bookAuthor = Authors.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                book.Authors.AddRange(bookAuthor);

                foreach(var a in bookAuthor)
                {
                    a.Books.Add(book);
                }

            }

            var users = new Faker<User>()
                .RuleFor(e => e.Name, f => f.Name.FullName());
            Users = users.Generate(100);
            Console.WriteLine("Data Generated successfully");


            //SaveToDatabaseFakeData();
        }

        //private void SaveToDatabaseFakeData()
        //{
        //    using(var context = new BookStoreDbContext())
        //    {
        //        context.Database.EnsureCreated();
        //        context.Books.AddRange(Books);
        //        context.Authors.AddRange(Authors);
        //        context.Users.AddRange(Users);
        //        context.SaveChanges();
        //    }
        //}
    }


}
