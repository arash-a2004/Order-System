using LibraryDemo.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDemo.Queries.GetAllBooks
{
    public record GetAllBooksQuery(ApiBook? ApiBook) : IRequest<List<BookDto>>;
}
