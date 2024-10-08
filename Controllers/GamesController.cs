﻿namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGamesService _gamesService;

		public GamesController(ICategoriesService categoriesService, 
            IDevicesService devicesService, 
            IGamesService gamesService)
		{
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gamesService = gamesService;
		}

        public IActionResult Index()
        {
            var game = _gamesService.GetAll();
            return View(game);
        }

        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);
            if (game is null)
                return NotFound();
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectListItems(),
                Devices = _devicesService.GetSelectListItems()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid) {
                model.Categories = _categoriesService.GetSelectListItems();
                model.Devices = _devicesService.GetSelectListItems();
				return View(model);
            }

            await _gamesService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var game = _gamesService.GetById(id);
            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                SelectedDevices = game.Devices.Select(d=>d.DeviceId).ToList(),
                CategoryId = game.CategoryId,
                Categories = _categoriesService.GetSelectListItems(),
                Devices = _devicesService.GetSelectListItems().ToList(),
                CurrentCover = game.Cover,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
			if (!ModelState.IsValid)
			{
				model.Categories = _categoriesService.GetSelectListItems();
				model.Devices = _devicesService.GetSelectListItems();
				return View(model);
			}

			var game = await _gamesService.Edit(model);
            if(game is null)
                return BadRequest();

			return RedirectToAction(nameof(Index));
		}

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gamesService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
