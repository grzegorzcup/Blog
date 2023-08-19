using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        Post GetById(int id);
        Post Add(Post post);
        void Update(Post post);
        void Delete(int id);
    }
}
