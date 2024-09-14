using LibraryDemo.DBContext;
using LibraryDemo.Models;
using LibraryDemo.Queries.GetAllBooks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDemo.Handlers.GetAllBooks
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookDto>>
    {
        public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                BooksStoreDbContext dbContext = new();
                var query = dbContext.Books.AsQueryable();

                if (!string.IsNullOrEmpty(request.ApiBook.Title))
                    query = query.Where(e => e.Title == request.ApiBook.Title);

                int Skip = 0;
                int Max = 10;

                if (request.ApiBook.SkipCount != null)
                    Skip = request.ApiBook.SkipCount.Value;

                if (request.ApiBook.MaxResult != null)
                    Max = request.ApiBook.MaxResult.Value;

                var result = query.Include(u => u.Authors).Skip(Skip).Take(Max).ToList();

                var res = new List<BookDto>();
                var list2 = new List<string>();

                foreach (var item in result)
                {
                    list2.Clear();
                    list2.AddRange(item.Authors.Select(e => e.Name));
                    res.Add(
                            new BookDto()
                            {
                                Title = item.Title,
                                Authors = list2
                            }

                        );
                }

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
