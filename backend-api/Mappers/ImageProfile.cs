using AutoMapper;
using backend_api.DTO;
using backend_api.Entities;

namespace backend_api.Mappers
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageResponseDTO>();
        }
    }
}

