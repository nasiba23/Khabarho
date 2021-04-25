using Khabarho.Models.PostModels;
using Khabarho.ViewModels.Post;

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
        }
    }
}