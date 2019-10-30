using Domain;
using HighSchoolLesson.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAdoNet
{
	/*
	Курс: «Технология доступа к базам данных ADO.NET»
	Тема: Введение в Entity Framework
	Создайте базу данных для библиотеки. В ней будет храниться информация о зарегистрированных 
		посетителях библиотеки, какие книги на руках у посетителя; является ли посетитель должником. 
	Книга может иметь несколько авторов, а один автор может быть автором нескольких книг.
	Выполните к созданной структуре таблиц следующие задачи:
		1) Выведите список должников.
		2) Выведите список авторов книги №3 (по порядку из таблицы ‘Book’).
		3) Выведите список книг, которые доступны в данный момент.
		4) Вывести список книг, которые на руках у пользователя №2.
		5) Обнулите задолженности всех должников.
	*/
	class Program
	{
		static void Main(string[] args)
		{
			CreateDb();
			Debtors();
			AuthorOfBookNum3();
			AvailableBooks();
			BooksOfVisitorNum2();
			SetVisitorsDebtsToNull();
		}

		private static void SetVisitorsDebtsToNull()
		{
			using (var context = new LibraryContext())
			{
				var query = from visitor
										in context.Visitors
										where visitor.Debtor == true
										select visitor;
				foreach (var debtor in query.ToList())
				{
					debtor.Debtor = false;
					context.Visitors.Update(debtor);
					context.SaveChanges();
				}
			}
			Console.WriteLine("\t5.Все задолженности успешно удалены.");
			Debtors();
		}

		private static void BooksOfVisitorNum2()
		{
			Console.WriteLine("\t4.Книги постелителя номер 2 по списку:");
			using (var context = new LibraryContext())
			{
				var query = from visitor
										in context.Visitors
										select visitor;
				var result = query.ToList().Skip(1).Take(1);
				Console.Write($"Посетитель {result.First().FullName} - Книга(и) ");
				foreach (var book in result.First().Books)
				{
					Console.Write($"{book.Title}, ");
				}
				Console.Write("\n");
			}
		}

		private static void AvailableBooks()
		{
			Console.WriteLine("\t3.Доступные книги:");
			using (var context = new LibraryContext())
			{
				var query = from book
										in context.Books
										where book.Visitor == null
										select book;
				int num = 1;
				foreach (var oneBook in query.ToList())
				{
					Console.WriteLine($"{num++}) {oneBook.Title}");
				}
			}
		}

		private static void AuthorOfBookNum3()
		{
			Console.WriteLine("\t2.Авторы книги номер 3 по списку:");
			using (var context = new LibraryContext())
			{
				var query = from book
										in context.Books
										select book;
				var result = query.ToList().Skip(2).Take(1);
				Console.Write($"Книга {result.First().Title} - Автор(ы) ");
				foreach (var book in result.First().Authors)
				{
					Console.Write($"{book.Author.FullName}, ");
				}
				Console.Write("\n");
			}
		}

		private static void Debtors()
		{
			Console.WriteLine("\t1.Должники:");
			using (var context = new LibraryContext())
			{
				var query = from visitor
										 in context.Visitors
										where visitor.Debtor.Equals(true)
										select visitor;
				var result = query.ToList();
				foreach (var oneVisitor in result)
				{
					Console.Write($"Должник {oneVisitor.FullName} - Книга(и) ");
					foreach (var book in oneVisitor.Books)
					{
						Console.Write($"{book.Title}, ");
					}
					Console.Write("\n");
				}
			}
		}

		private static void CreateDb()
		{
			using (var context = new LibraryContext())
			{
				var book1 = new Book
				{
					Title = "Martin Eden"
				};
				var book2 = new Book
				{
					Title = "Sea Wolf"
				};
				var book3 = new Book
				{
					Title = "Some book"
				};
				var book4 = new Book
				{
					Title = "Another book"
				};
				var book5 = new Book
				{
					Title = "Вымышленная книга от вымышленного автора"
				};

				var author1 = new Author
				{
					FullName = "Jack London"
				};
				var author2 = new Author
				{
					FullName = "Creig Drake"
				};

				var booksAuthors1 = new BooksAuthors
				{
					Author = author1,
					Book = book1
				};

				var booksAuthors2 = new BooksAuthors
				{
					Author = author1,
					Book = book2
				};

				var booksAuthors3 = new BooksAuthors
				{
					Author = author2,
					Book = book3
				};

				var booksAuthors4 = new BooksAuthors
				{
					Author = author2,
					Book = book4
				};

				var booksAuthors5 = new BooksAuthors
				{
					Author = author2,
					Book = book5
				};

				var visitor1 = new Visitor
				{
					FullName = "Alex Frad",
					Debtor = false
				};

				var visitor2 = new Visitor
				{
					FullName = "Jack Leonardo",
					Debtor = true,
					Books = new List<Book> { book4 }
				};

				var visitor3 = new Visitor
				{
					FullName = "Marco Polo",
					Debtor = true,
					Books = new List<Book> { book1, book2 }
				};

				context.Add(book1);
				context.Add(book2);
				context.Add(book3);
				context.Add(book4);
				context.Add(book5);
				context.Add(author1);
				context.Add(author2);
				context.Add(booksAuthors1);
				context.Add(booksAuthors2);
				context.Add(booksAuthors3);
				context.Add(booksAuthors4);
				context.Add(booksAuthors5);
				context.Add(visitor1);
				context.Add(visitor2);
				context.Add(visitor3);

				context.SaveChanges();
			}
		}
	}
}
