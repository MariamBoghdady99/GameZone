namespace GameZone.ViewModels
{
	public class CreateGameFormViewModel : GameFormViewModel
	{
		[AllowedExtensionAttributes(FileSettings.AllowedExtensions),
			MaxFileSizeAttributes(FileSettings.MaxFileSizeInBytes)]
		public IFormFile Cover { get; set; } = default!;
	}
}
