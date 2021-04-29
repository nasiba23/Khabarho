using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Khabarho.Db;
using Khabarho.Models.PostModels;
using Khabarho.Repositories;
using Khabarho.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Khabarho.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private IReactionRepository<Comment> _reactionRepo;
        private IUpdatableReactionRepository<Comment> _updatableRepo;
        private IMapper _mapper;
        private ILogger<CommentService> _logger;
        private DataContext _context;

        public CommentService(IReactionRepository<Comment> reactionRepo, IUpdatableReactionRepository<Comment> updatableRepo,
            IMapper mapper, ILogger<CommentService> logger, DataContext context)
        {
            _reactionRepo = reactionRepo;
            _updatableRepo = updatableRepo;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }
        
        public async Task<CommentViewModel> CreateAsync(CommentViewModel model)
        {
            var commentViewModel = new CommentViewModel();
            
            try
            {
                model.CreatedDate = DateTime.Now;
                
                var comment = _mapper.Map<Comment>(model);
                comment.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
                var result = await _reactionRepo.InsertAsync(comment);
                
                if (!result)
                {
                    return new CommentViewModel();
                } 
                
                commentViewModel = _mapper.Map<CommentViewModel>(comment);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return commentViewModel;
        }

        public async Task<CommentViewModel> GetAsync(string id)
        {
            var commentViewModel = new CommentViewModel();
            
            try
            { 
                var comment = await _reactionRepo.GetAsync(id);
                commentViewModel = _mapper.Map<CommentViewModel>(comment);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return commentViewModel;
        }

        public async Task<List<CommentViewModel>> GetAllAsync()
        {
            var commentViewModel = new List<CommentViewModel>();
            
            try
            {
                var categories = await _reactionRepo.GetAllAsync();
                commentViewModel = categories.Select(c=> _mapper.Map<CommentViewModel>(c)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return commentViewModel;
        }

        public async Task<CommentViewModel> UpdateAsync(CommentViewModel model)
        {
            var commentViewModel = new CommentViewModel();
            
            try
            {
                var comment = _mapper.Map<Comment>(model);
                
                comment.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);

                var result = await _updatableRepo.UpdateAsync(comment);
                
                if (!result)
                {
                    return new CommentViewModel();
                }
                
                commentViewModel = _mapper.Map<CommentViewModel>(comment);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return commentViewModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = false;
            try
            {
                var comment = await _reactionRepo.GetAsync(id);
                result = await _reactionRepo.DeleteAsync(comment);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return result;
        }
    }
}