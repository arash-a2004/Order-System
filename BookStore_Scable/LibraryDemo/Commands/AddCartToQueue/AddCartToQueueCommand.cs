using LibraryDemo.Models;
using LibraryDemo.Models.enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDemo.Commands.AddCartToQueue
{
    public record AddCartToQueueCommand(int UserId, BookDto Book) : IRequest<UserFoundState>;

}
