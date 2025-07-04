using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcITIProject.ModelView
{
    public class BookAuthorModelView
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public List<int> SelectedAuthorIds { get; set; } = new();

        public IEnumerable<SelectListItem>? Books { get; set; }
        public IEnumerable<SelectListItem>? Authors { get; set; }

    }
}
