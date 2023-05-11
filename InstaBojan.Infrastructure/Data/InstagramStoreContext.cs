using InstaBojan.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Data
{
    public class InstagramStoreContext:IdentityDbContext<ApplicationUser>
    {


        public InstagramStoreContext(DbContextOptions<InstagramStoreContext> options) : base(options)
        { }



     

      

        
    }
}
