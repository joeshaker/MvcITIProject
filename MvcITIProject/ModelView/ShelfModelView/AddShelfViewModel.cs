using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcITIProject.ModelView.ShelfModelView
{
    public class AddShelfViewModel
    {
        public string Code { get; set; } = null!;

        public int? FloorNum { get; set; }

        public List<SelectListItem> ?FloorOptions { get; set; }

    }
}
