using AutoMapper;
using backend_api.Context;
using backend_api.DTO;
using backend_api.Entities;
using backend_api.Mappers;
using static StackExchange.Redis.Role;

namespace backend_api.Services
{
	public class ImageService
	{
		readonly ApplicationDBContext _applicationDBContext;
		readonly Mapper _mapper;

		public ImageService(ApplicationDBContext applicationDBContext, Mapper mapper) {
			_applicationDBContext = applicationDBContext;
			_mapper = mapper;
		}

		public List<ImageResponseDTO> GetAllImages() {
			var images = _applicationDBContext.Images.ToList();
			var imageDTOS = MapperConfig.InitializeAutoMapper().Map<List<ImageResponseDTO>>(images);
			return imageDTOS;
        }

        internal void createImage(ImageRequestDTO imageRequestDTO)
        {
			var image = MapperConfig.InitializeAutoMapper().Map<Image>(imageRequestDTO);
			_applicationDBContext.Images.Add(image);
        }

        internal void updateImage(ImageRequestDTO imageRequestDTO)
        {
			
        }
    }
}

