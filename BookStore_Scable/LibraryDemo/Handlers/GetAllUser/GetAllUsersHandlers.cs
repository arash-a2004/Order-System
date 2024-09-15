using LibraryDemo.DBContext;
using LibraryDemo.Queries.GetAllUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDemo.Handlers.GetAllUser
{
    public class GetAllUsersHandlers : IRequestHandler<GetAllUserQuery, IEnumerable<object>>
    {
        public Task<IEnumerable<object>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            DataAccess.DemoDataAccess da = new DataAccess.DemoDataAccess();

            var res = da.GetAllUsers(request.Body.SkipCount, request.Body.MaxResult, request.Body.NameFilter);

            return Task.FromResult(res);
        }
    }
}
