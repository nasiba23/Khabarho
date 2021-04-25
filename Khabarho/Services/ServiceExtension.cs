using System;
using Khabarho.Models.PostModels;
using Khabarho.Repositories;
using Khabarho.Services.Posts;
using Microsoft.AspNetCore.Components.Forms;
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
            services.AddTransient(typeof(IPostService), typeof(PostService));
                
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}