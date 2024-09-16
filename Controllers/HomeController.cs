namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _gameService;

        public HomeController(IGamesService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var game = _gameService.GetAll();
            return View(game);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
