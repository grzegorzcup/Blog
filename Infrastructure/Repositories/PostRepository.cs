using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _blogContext;

        public PostRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public IEnumerable<Post> GetAllPosts()
        {
            return _blogContext.Posts;
        }

        public Post Add(Post post)
        {
            _blogContext.Posts.Add(post);
            _blogContext.SaveChanges();
            return post;
        }

        public void Delete(int id)
        {
            var post = _blogContext.Posts.FirstOrDefault(p => p.Id == id);
            _blogContext.Posts.Remove(post);
            _blogContext.SaveChanges();
        }

        public Post GetById(int id)
        {
            var post = _blogContext.Posts.FirstOrDefault(p => p.Id == id);
            return post;
        }

        public void Update(Post post)
        {
            _blogContext.Posts.Update(post);
            _blogContext.SaveChanges();
        }
    }
}
