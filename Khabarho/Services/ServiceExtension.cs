using System;
using Khabarho.Repositories;
using Khabarho.Services.CategoryService;
using Khabarho.Services.PostService;
using Microsoft.Extensions.DependencyInjection;

namespace Khabarho.Services
{
    public static class ServiceExtension
    {
        public static void InitService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IReactionRepository<>), typeof(ReactionRepository<>));
            services.AddTransient(typeof(IUpdatableReactionRepository<>), typeof(UpdatableReactionRepository<>));
            services.AddTransient(typeof(IPostService), typeof(PostService.PostService));
            services.AddTransient(typeof(ICategoryService), typeof(CategoryService.CategoryService));

                
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}