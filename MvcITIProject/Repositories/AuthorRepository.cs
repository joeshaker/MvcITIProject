using MvcITIProject.Models;

namespace MvcITIProject.Repositories
{
    public class AuthorRepository : GenericRepositries<Author>
    {
        public AuthorRepository(LibraryContext context) : base(context)
        {
        }
    }
}
