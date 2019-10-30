using System.Collections.Generic;

namespace Domain
{
	public class Author: Entity
	{
		public string FullName { get; set; }
		public virtual ICollection<BooksAuthors> Books { get; set; } = new List<BooksAuthors>();
	}
}
