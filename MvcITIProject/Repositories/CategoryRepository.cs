using MvcITIProject.Models;

namespace MvcITIProject.Repositories
{
    public class CategoryRepository:GenericRepositries<Category>
    {
        public CategoryRepository(LibraryContext context):base(context)
        {
            
        }
    }
}
