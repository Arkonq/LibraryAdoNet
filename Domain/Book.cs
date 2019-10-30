using System.Collections.Generic;

namespace Domain
{
	public class Book : Entity
	{
		public string Title { get; set; }		
		public virtual Visitor Visitor { get; set; }
		public virtual ICollection<BooksAuthors> Authors { get; set; } = new List<BooksAuthors>();
	}
}