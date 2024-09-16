namespace GameZone.Attributes
{
	public class AllowedExtensionAttributes : ValidationAttribute
	{
		private readonly string _allowedExtenxions;

		public AllowedExtensionAttributes(string allowedExtenxions)
		{
			_allowedExtenxions = allowedExtenxions;
		}

		protected override ValidationResult? IsValid(object? value,
			ValidationContext validationContext)
		{
			var file = value as IFormFile;

			if(file is not null){
				var extension = Path.GetExtension(file.FileName);

				var isAllowed = _allowedExtenxions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);

				if (!isAllowed) {
					return new ValidationResult($"Only {_allowedExtenxions} are allowed!");
				}
			}

			return ValidationResult.Success;
		}
	}
}
