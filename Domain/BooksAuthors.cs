using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class BooksAuthors : Entity
	{
		public virtual Book Book { get; set; }

		public virtual Author Author { get; set; }
	}
}
