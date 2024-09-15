using LibraryDemo.Models;

namespace LibraryDemo.DataAccess
{
    public interface IDemoDataAccess
    {
        void AddCartToQueue(string CartjsonSerialize);
        IEnumerable<object> GetAllUsers(int skipCount = 0, int maxResult = 10, string? name = "");
        Task<User?> GetUserById(int id);
    }
}