using Microsoft.EntityFrameworkCore;
using MaPetiteApi.Model;

namespace MaPetiteApi.Data
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

       // public DbSet<Todo> Todos => Set<Todo>();

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
