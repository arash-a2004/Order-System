using Microsoft.EntityFrameworkCore;

namespace RestApiServices.Services
{
    public class GetAllUser
    {
        public IEnumerable<object> GetAllUsers(int skipCount = 0, int maxResult = 10, string? name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                BookStoreDbContext dbcontext = new BookStoreDbContext();

                var users = dbcontext.Users
                    .Include(u => u.Cart)
                        .ThenInclude(e => e.Books)
                    .Select(u => new
                    {
                        Names = u.Name,
                        Titles = u.Cart.Books.Select(a => a.Title).ToList()
                    }).Skip(skipCount).Take(maxResult).ToList();

                return users;
            }
            return null;
        }
    }
}
