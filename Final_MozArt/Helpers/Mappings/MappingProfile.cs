using AutoMapper;
using Final_MozArt.Models;
using Final_MozArt.ViewModels.Category;
using Final_MozArt.ViewModels.Slider;

namespace Final_MozArt.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Slider, SliderVM>();
            CreateMap<SliderCreateVM, Slider>();
            CreateMap<SliderEditVM, Slider>().ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryCreateVM, Category>();
            CreateMap<CategoryVM, CategoryEditVM>();
        }
    }
}
