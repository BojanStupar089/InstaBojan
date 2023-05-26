using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBokiInsta.TestPostsRepository
{
    public class TestsPostRepository
    {

        private readonly DbContextOptions<InstagramStoreContext> _options;
        private readonly InstagramStoreContext _context;
        private readonly UsersRepository _userRepository;

        public TestsPostRepository()
        {

            _options = new DbContextOptionsBuilder<InstagramStoreContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _context = new InstagramStoreContext(_options);
            _userRepository = new UsersRepository(_context);

        }


        [Fact]
        public void GetPosts_ReturnsListOfPosts() { 
        
        
             
        }


    }
}
