using AutoMapper;
using Microsoft.SyndicationFeed;
using simple_feed_reader.ViewModels;

namespace simple_feed_reader.Services
{
    public class NewsStoryProfile : Profile
    {
        public NewsStoryProfile()
        {
            // Create the AutoMapper mapping profile between the 2 objects.
            // ISyndicationItem.Id maps to NewsStoryViewModel.Uri.
            CreateMap<ISyndicationItem, NewsStoryViewModel>()
                .ForMember(dest => dest.Uri, opts => opts.MapFrom(src => src.Id));
        }
    }
}
