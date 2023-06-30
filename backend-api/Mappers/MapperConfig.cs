using System;
using AutoMapper;
using backend_api.DTO;
using backend_api.Entities;

namespace backend_api.Mappers
{
	public class MapperConfig
	{
		public static Mapper InitializeAutoMapper() {
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<List<Image>, List<ImageResponseDTO>>();
				cfg.CreateMap<ImageRequestDTO, Image>()
				.ForMember(dest => dest.Name, act => act.MapFrom(src => src.NewName));

            });

			return new Mapper(config);
		}
	}
}

