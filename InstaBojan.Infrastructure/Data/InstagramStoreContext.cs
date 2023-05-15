using InstaBojan.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Data
{
    public class InstagramStoreContext:DbContext
    {

       

        public InstagramStoreContext(DbContextOptions<InstagramStoreContext> options) : base(options)
        { }


        public DbSet<User> Users { get; set; }

        public DbSet<Profile> Profiles { get; set;}

       /* public new async Task<int> SaveChanges() { 
        
             return await base.SaveChangesAsync();
        
        }
       */

     

      

        
    }
}
