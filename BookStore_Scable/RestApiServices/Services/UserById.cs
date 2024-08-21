using Microsoft.EntityFrameworkCore;
using RestApiServices.Models;

namespace RestApiServices.Services
{
    public class UserById
    {

        public async Task<User?> GetUserById(int id)
        {
            BookStoreDbContext bookStoreDbContext = new BookStoreDbContext();

            var user = await bookStoreDbContext.Users
                .Include(e => e.Cart)
                .FirstOrDefaultAsync(e =>e.Id == id);

            return user;
        }

    }
}
