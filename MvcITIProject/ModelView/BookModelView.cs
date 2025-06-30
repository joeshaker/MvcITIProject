using Microsoft.AspNetCore.Mvc.Rendering;
using MvcITIProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcITIProject.ModelView
{
    public class BookModelView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public int? CatId { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        public int? PublisherId { get; set; }

        [Required(ErrorMessage = "Shelf is required")]
        public string ShelfCode { get; set; } = string.Empty;
        public virtual Category ?Cat { get; set; } = null!;

        public virtual Publisher? Publisher { get; set; } = null!;

        public virtual Shelf? ShelfCodeNavigation { get; set; } = null!;

        public virtual ICollection<Author>? Authors { get; set; } = new List<Author>();
        public IEnumerable<SelectListItem> ?Categories { get; set; }
        public IEnumerable<SelectListItem>? Publishers { get; set; }
        public IEnumerable<SelectListItem>? Shelves { get; set; }
    }
}
