using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDemo.Queries.GetAllUser
{
    public record GetAllUserQuery(Models.FromBody.GetAllUserApiBody Body) : IRequest<IEnumerable<object>>;

}
