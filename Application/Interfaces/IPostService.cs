using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPostService
    {
        public Post AddPost(PostDto addPostDto);
        public Post UpdatePost(int postId);
        public Post RemovePost(int postId);
        public Post GetPost(int postId);
        public IEnumerable<Post> GetAllPosts();

    }
}
