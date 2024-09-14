using LibraryDemo.Commands.AddCartToQueue;
using LibraryDemo.Models.enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDemo.Handlers.AddCartToQueue
{
    public class AddCartToQueueHandler : IRequestHandler<AddCartToQueueCommand, UserFoundState>
    {
        public Task<UserFoundState> Handle(AddCartToQueueCommand request, CancellationToken cancellationToken)
        {
            DataAccess.DemoDataAccess dataAccess = new DataAccess.DemoDataAccess();

            var res = dataAccess.AddBoodToCartByUserId(request.UserId,request.Book);

            return res;
        }
    }
}
