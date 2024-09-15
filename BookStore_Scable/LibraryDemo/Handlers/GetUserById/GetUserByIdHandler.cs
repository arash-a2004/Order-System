using LibraryDemo.Models;
using LibraryDemo.Queries.GetUserById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDemo.Handlers.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            DataAccess.DemoDataAccess access = new();

            var res =await access.GetUserById(request.Id);

            return res;
        }
    }
}
