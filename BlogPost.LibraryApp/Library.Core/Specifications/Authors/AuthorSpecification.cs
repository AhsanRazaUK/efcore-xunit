using Library.Core.Entities;

namespace Library.Core.Specifications.Authors
{
    public class AuthorSpecification : BaseSpecification<Author>
    {
        public AuthorSpecification() : base() { }
        public AuthorSpecification(string filter) :
            base(a => a.FirstName.Equals(filter) || a.LastName.Equals(filter))
        {

        }
    }
}
