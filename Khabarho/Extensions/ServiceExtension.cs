using System;
using Khabarho.Repositories;
using Khabarho.Services.CategoryService;
using Khabarho.Services.CommentService;
using Khabarho.Services.LikeService;
using Khabarho.Services.PostService;
using Khabarho.Services.TypeService;
using Microsoft.Extensions.DependencyInjection;

namespace Khabarho.Extensions
{
    public static class ServiceExtension
    {
        public static void InitService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IReactionRepository<>), typeof(ReactionRepository<>));
            services.AddTransient(typeof(IUpdatableReactionRepository<>), typeof(UpdatableReactionRepository<>));
            services.AddTransient(typeof(IPostService), typeof(Services.PostService.PostService));
            services.AddTransient(typeof(ICategoryService), typeof(Services.CategoryService.CategoryService));
            services.AddTransient(typeof(ITypeService), typeof(Services.TypeService.TypeService));
            services.AddTransient(typeof(ILikeService), typeof(Services.LikeService.LikeService));
            services.AddTransient(typeof(ICommentService), typeof(CommentService));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}