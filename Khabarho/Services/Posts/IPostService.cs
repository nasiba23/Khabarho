﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Khabarho.Models.PostModels;
using Khabarho.ViewModels.Post;

namespace Khabarho.Services.Posts
{
    public interface IPostService
    {
        public Task<ShowPostViewModel> CreateAsync(PostViewModel model);

        public Task<ShowPostViewModel> GetAsync(string id);

        public Task<List<ShowPostViewModel>> GetAllAsync();

        public Task<ShowPostViewModel> UpdateAsync(PostViewModel model);

        public Task<bool> DeleteAsync(string id);
    }
}