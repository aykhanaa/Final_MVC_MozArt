using AutoMapper;
using Final_MozArt.Models;
using Final_MozArt.ViewModels.About;
using Final_MozArt.ViewModels.Advantage;
using Final_MozArt.ViewModels.Blog;
using Final_MozArt.ViewModels.BlogCategory;
using Final_MozArt.ViewModels.Brand;
using Final_MozArt.ViewModels.Category;
using Final_MozArt.ViewModels.Color;
using Final_MozArt.ViewModels.ContactIntro;
using Final_MozArt.ViewModels.Instagram;
using Final_MozArt.ViewModels.Product;
using Final_MozArt.ViewModels.Setting;
using Final_MozArt.ViewModels.Slider;
using Final_MozArt.ViewModels.Support;
using Final_MozArt.ViewModels.Tag;
using Final_MozArt.ViewModels.Video;

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

            CreateMap<Brand, BrandVM>();
            CreateMap<BrandCreateVM, Brand>();
            CreateMap<BrandVM, BrandEditVM>();

            CreateMap<Instagram, InstagramVM>();
            CreateMap<InstagramCreateVM, Instagram>();
            CreateMap<InstagramVM, InstagramEditVM>();

            CreateMap<Support, SupportVM>();
            CreateMap<SupportCreateVM, Support>();
            CreateMap<SupportVM, SupportEditVM>();

            CreateMap<About, AboutVM>();
            CreateMap<AboutCreateVM, About>();
            CreateMap<AboutVM, AboutEditVM>();

            CreateMap<Setting, SettingVM>();
            CreateMap<SettingCreateVM, Setting>();
            CreateMap<SettingEditVM, Setting>();

            CreateMap<Video, VideoVM>();
            CreateMap<VideoCreateVM, Video>();
            CreateMap<VideoVM, VideoEditVM>();
                
            CreateMap<Advantage, AdvantageVM>();
            CreateMap<AdvantageCreateVM, Advantage>();
            CreateMap<AdvantageVM, AdvantageEditVM>();

            CreateMap<ContactIntro, ContactIntroVM>();
            CreateMap<ContactIntroCreateVM, ContactIntro>();
            CreateMap<ContactIntroVM, ContactIntroEditVM>();

            CreateMap<BlogCategory, BlogCategoryVM>();
            CreateMap<BlogCategoryCreateVM, BlogCategory>();
            CreateMap<BlogCategoryVM, BlogCategoryEditVM>();

            CreateMap<Blog, BlogVM>()
              .ForMember(dest => dest.BlogCategoryName, opt => opt.MapFrom(src => src.BlogCategory.Name));

            CreateMap<BlogCreateVM, Blog>();
            CreateMap<BlogVM, BlogEditVM>();

            CreateMap<Tag, TagVM>();
            CreateMap<TagCreateVM, Tag>();
            CreateMap<TagVM, TagEditVM>();

            CreateMap<Color, ColorVM>();
            CreateMap<ColorCreateVM, Color>();
            CreateMap<ColorVM, ColorEditVM>();

            // Product -> ProductVM (Index view)
            CreateMap<Product, ProductVM>()
    .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src =>
        src.Images.FirstOrDefault(i => i.IsMain) != null ? src.Images.FirstOrDefault(i => i.IsMain).Image : null))
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
    .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));



            // Product -> ProductDetailVM
            CreateMap<Product, ProductDetailVM>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    src.Images.Select(img => new ProductImageVM
                    {
                        Id = img.Id,
                        Url = img.Image, // ProductImage.Image istifadə olunur
                        IsMain = img.IsMain
                    }).ToList()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TagNames, opt => opt.MapFrom(src =>
                    src.ProductTags.Select(pt => pt.Tag.Name).ToList()))
                .ForMember(dest => dest.ColorNames, opt => opt.MapFrom(src =>
                    src.ProductColors.Select(pc => pc.Color.Name).ToList()));

            // Product -> ProductEditVM
            CreateMap<Product, ProductEditVM>()
                .ForMember(dest => dest.ExistingImages, opt => opt.MapFrom(src =>
                    src.Images.Select(img => new ProductImageVM
                    {
                        Id = img.Id,
                        Url = img.Image, // Image property
                        IsMain = img.IsMain
                    }).ToList()))
                .ForMember(dest => dest.SelectedColorIds, opt => opt.MapFrom(src =>
                    src.ProductColors.Select(pc => pc.ColorId).ToList()))
                .ForMember(dest => dest.SelectedTagIds, opt => opt.MapFrom(src =>
                    src.ProductTags.Select(pt => pt.TagId).ToList()));

            // ProductCreateVM -> Product
            CreateMap<ProductCreateVM, Product>();

            // ProductEditVM -> Product
            CreateMap<ProductEditVM, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.ProductColors, opt => opt.Ignore())
                .ForMember(dest => dest.ProductTags, opt => opt.Ignore());







        }
    }
}
