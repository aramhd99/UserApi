using Microsoft.EntityFrameworkCore;
using User_Information.models;

namespace User_Information
{
    public class UserDbContext : DbContext
    {
        public UserDbContext (DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}