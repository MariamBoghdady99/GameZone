namespace GameZone.Sevices
{
	public interface ICategoriesService
	{
		IEnumerable<SelectListItem> GetSelectListItems();
	}
}
