using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcITIProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcITIProject.ModelView
{
    public class BookModelView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title can't be more than 50 characters")]
        [Remote(action: "IsTitleUnique", controller: "Book", AdditionalFields = "Id", ErrorMessage = "This title already exists")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int? CatId { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        [Display(Name = "Publisher")]
        public int? PublisherId { get; set; }

        [Required(ErrorMessage = "Shelf is required")]
        [Display(Name = "Shelf")]
        public string ShelfCode { get; set; } = string.Empty;
        public string ? BookLink { get; set; }= string.Empty;
        public virtual Category ?Cat { get; set; } = null!;

        public virtual Publisher? Publisher { get; set; } = null!;

        public virtual Shelf? ShelfCodeNavigation { get; set; } = null!;

        public virtual ICollection<Author>? Authors { get; set; } = new List<Author>();
        public IEnumerable<SelectListItem> ?Categories { get; set; }
        public IEnumerable<SelectListItem>? Publishers { get; set; }
        public IEnumerable<SelectListItem>? Shelves { get; set; }
    }
}
