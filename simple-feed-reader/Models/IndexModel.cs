using simple_feed_reader.Services;
using simple_feed_reader.ViewModels;
using System.Net;
using System.Xml;

namespace simple_feed_reader.Models
{
    public class IndexModel
    {
        private readonly NewsService _newsService;

        public IndexModel()
        {

        }

        public IndexModel(NewsService newsService)
        {
            _newsService = newsService;
        }

        public string FeedUrl { get; set; }

        public string ErrorText { get; private set; }

        public List<NewsStoryViewModel> NewsItems { get; private set; }

        public async Task OnGet(string feedUrl)
        {

            if (!string.IsNullOrEmpty(feedUrl))
            {
                try
                {
                    NewsItems = await _newsService.GetNews(feedUrl);
                }
                catch (UriFormatException)
                {
                    ErrorText = "There was a problem parsing the URL.";
                    return;
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    ErrorText = "Unknown host name.";
                    return;
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    ErrorText = "Syndication feed not found.";
                    return;
                }
                catch (AggregateException ae)
                {
                    ae.Handle((x) =>
                    {
                        if (x is XmlException)
                        {
                            ErrorText = "There was a problem parsing the feed. Are you sure that URL is a syndication feed?";
                            return true;
                        }
                        return false;
                    });
                }
            }
        }
    }
}
