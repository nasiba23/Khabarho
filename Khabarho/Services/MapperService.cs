using Khabarho.Models.PostModels;
using Khabarho.ViewModels.CategoryViewModels;
using Khabarho.ViewModels.CommentViewModels;
using Khabarho.ViewModels.LikeViewModels;
using Khabarho.ViewModels.PostViewModels;
using Khabarho.ViewModels.TypeViewModels;

namespace Khabarho.Services
{
    public class MapperService : AutoMapper.Profile
    {
        public MapperService()
        {
            this.CreateMap<Post, PostViewModel>()
                .ForMember(m => m.Id,
                    option => option.MapFrom(m => m.Id))
                .ForMember(m => m.Types,
                    option => option.Ignore())
                .ForMember(m => m.TypeId,
                    option => option.MapFrom(m => m.TypeId))
                .ForMember(m => m.CreatedDate,
                    option => option.MapFrom(m => m.CreatedDate))
                .ForMember(m => m.Text,
                    option => option.MapFrom(m => m.Text))
                .ForMember(m => m.ImagePath,
                    option => option.MapFrom(m => m.Image))
                .ForMember(m => m.Title,
                    option => option.MapFrom(m => m.Title))
                .ForMember(m => m.ImageFile,
                    option => option.Ignore())
                .ForMember(m => m.Categories,
                    option => option.MapFrom(m => m.Categories))
                .ForMember(m => m.AuthorId,
                    option => option.MapFrom(m => m.AuthorId))
                .ReverseMap();
            
            this.CreateMap<Post, ShowPostViewModel>()
                .ForMember(m => m.Id,
                    option => option.MapFrom(m => m.Id))
                .ForMember(m => m.CreatedDate,
                    option => option.MapFrom(m => m.CreatedDate))
                .ForMember(m => m.Text,
                    option => option.MapFrom(m => m.Text))
                .ForMember(m => m.ImagePath,
                    option => option.MapFrom(m => m.Image))
                .ForMember(m => m.Title,
                    option => option.MapFrom(m => m.Title))
                .ForMember(m => m.Categories,
                    option => option.Ignore())
                .ForMember(m => m.AuthorName,
                    option => option.MapFrom(m => m.Author))
                .ForMember(m => m.Comments,
                            option => option.Ignore())
                .ForMember(m => m.NumberOfComments,
                    option => option.Ignore())
                .ForMember(m => m.NumberOfLikes,
                    option => option.Ignore())
                .ReverseMap();

            this.CreateMap<Category, CategoryViewModel>()
                .ForMember(m => m.Id,
                    option => option.MapFrom(m => m.Id))
                .ForMember(m => m.Title,
                    option => option.MapFrom(m => m.Title))
                .ForMember(m => m.Posts,
                    option => option.MapFrom(m => m.Posts))
                .ReverseMap();
            
            this.CreateMap<Type, TypeViewModel>()
                .ForMember(m => m.Id,
                    option => option.MapFrom(m => m.Id))
                .ForMember(m => m.Title,
                    option => option.MapFrom(m => m.Title))
                .ForMember(m => m.Posts,
                    option => option.MapFrom(m => m.Posts))
                .ReverseMap();

            this.CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.Id,
                    option => option.MapFrom(m => m.Id))
                .ForMember(m => m.UserId,
                    option => option.MapFrom(m => m.UserId))
                .ForMember(m => m.UserName,
                    option => option.MapFrom(m => m.User.UserName))
                .ForMember(m => m.PostId,
                    option => option.MapFrom(m => m.PostId))
                .ReverseMap();
            
            this.CreateMap<Like, LikeViewModel>()
                .ForMember(m => m.Id,
                    option => option.MapFrom(m => m.Id))
                .ForMember(m => m.UserId,
                    option => option.MapFrom(m => m.UserId))
                .ForMember(m => m.UserName,
                    option => option.MapFrom(m => m.User.UserName))
                .ForMember(m => m.PostId,
                    option => option.MapFrom(m => m.PostId))
                .ReverseMap();
        }
    }
}