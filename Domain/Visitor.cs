using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain
{
	public class Visitor : Entity
	{
		public string FullName { get; set; }
		public bool Debtor { get; set; }
		public virtual ICollection<Book> Books { get; set; }
	}
}
