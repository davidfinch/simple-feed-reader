using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using simple_feed_reader.Models;
using System.Diagnostics;

namespace simple_feed_reader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private IndexModel _indexModel;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _indexModel = new IndexModel(new Services.NewsService(mapper));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_indexModel);
        }

        [HttpPost]
        public IActionResult Index(IndexModel model)
        {
            _indexModel.FeedUrl = model.FeedUrl;
            _ = _indexModel.OnGet(model.FeedUrl);
            return View(_indexModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}