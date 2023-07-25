using InstaBojan.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaBojan.Infrastructure.Data
{
    public class InstagramStoreContext : DbContext
    {



        public InstagramStoreContext(DbContextOptions<InstagramStoreContext> options) : base(options)
        { }


        public DbSet<User> Users { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Post> Posts { get; set; }







    }
}
