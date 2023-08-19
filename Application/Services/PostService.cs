using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository postRepository, IMapper mapper, ILogger<PostService> logger)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public Post AddPost(PostDto addPostDto)
        {
            if (addPostDto == null)
                throw new Exception("Brak treści postu");
            var post = _mapper.Map<Post>(addPostDto);
            _postRepository.Add(post);
            return post;
        }

        public Post GetPost(int postId)
        {
            var post = _postRepository.GetById(postId);
            if (post == null)
                throw new Exception("nie ma postu z takim ID");
            return post;
        }

        public Post RemovePost(int postId)
        {
            var post = _postRepository.GetById(postId);
            if(post !=  null)
                _postRepository.Delete(postId);
            return post;
        }

        public Post UpdatePost(int postId)
        {
            var post = _postRepository.GetById(postId);
            if(post != null)
                _postRepository.Update(post);
            post = _postRepository.GetById(postId);
            return post;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            IEnumerable<Post> list = _postRepository.GetAllPosts();
            return list;
        }
    }
}
