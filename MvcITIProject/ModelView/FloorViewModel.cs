using System.ComponentModel.DataAnnotations;
using MvcITIProject.Models;

namespace MvcITIProject.ModelView
{
    public class FloorViewModel
    {
        [Display(Name = "Floor Number")]
        public int Number { get; set; }

        [Display(Name = "Number of Blocks")]
        public int NumBlocks { get; set; }

        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        public DateOnly? HiringDate { get; set; }

        [Display(Name = "Number of Shelves")]
        public int ShelvesCount { get; set; }
    }

    public class FloorCreateViewModel
    {
        [Required(ErrorMessage = "Floor number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Floor number must be a positive number")]
        [Display(Name = "Floor Number")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Number of blocks is required")]
        [Range(1, 1000, ErrorMessage = "Number of blocks must be between 1 and 1000")]
        [Display(Name = "Number of Blocks")]
        public int NumBlocks { get; set; }

        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        public DateOnly? HiringDate { get; set; }
    }

    public class FloorEditViewModel
    {
        [Display(Name = "Floor Number")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Number of blocks is required")]
        [Range(1, 1000, ErrorMessage = "Number of blocks must be between 1 and 1000")]
        [Display(Name = "Number of Blocks")]
        public int NumBlocks { get; set; }

        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        public DateOnly? HiringDate { get; set; }
    }

    public class FloorDetailsViewModel
    {
        [Display(Name = "Floor Number")]
        public int Number { get; set; }

        [Display(Name = "Number of Blocks")]
        public int NumBlocks { get; set; }

        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        public DateOnly? HiringDate { get; set; }

        [Display(Name = "Shelves")]
        public List<Shelf> Shelves { get; set; } = new List<Shelf>();
    }
}