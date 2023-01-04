using AutoMapper;
using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPUScraper.Maps
{
    public class GPUMappingProfile : Profile
    {
        public GPUMappingProfile()
        {
            CreateMap<GPU, GPUDto>();
        }
    }
}
