using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookEdtor.DataModel.Models
{
	public sealed class BookModel : IValidatableObject
	{
		public long BookId { get; set; }
		[Required]
		[StringLength(30)]
		public string Title { get; set; }
		[Required]
		[Range(0, 10000)]
		public int NumPages { get; set; }
		public long PubHouseId { get; set; }
		[StringLength(30)]
		public string PubHouseName { get; set; }
		[Range(1800, int.MaxValue)]
		public int? PublishYear { get; set; }
		[RegularExpression("^[0123456789-]+$",
			ErrorMessage = "Указаны недопустимые символы в ISBN")]
		public string ISBN { get; set; }
		public byte[] Illustration { get; set; }

		public List<long> Authors { get; set; }
		public string AuthorsNames { get; set; }
		public string IllustrationUrl { get; internal set; }

		public BookModel()
		{
		}

		public BookModel(Book book, List<Author> thisBookAuthors, PubHouse pubHouse)
		{
			BookId = book.BookId;
			Title = book.Title;
			NumPages = book.NumPages;
			PublishYear = book.PublishYear;
			ISBN = book.ISBN;
			Illustration = book.Illustration;
			IllustrationUrl = book.Illustration != null
				? $"data:image/png;base64; {Convert.ToBase64String(book.Illustration)}"
				: "";
			PubHouseId = book.PubHouseId;
			PubHouseName = pubHouse.Name;
			Authors = thisBookAuthors.Select(q => q.AuthorId).ToList();
			AuthorsNames = string.Join("; ", thisBookAuthors.Select(aa => $"{aa.LastName} {aa.FirstName}"));
		}

		IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
		{
			if (!CheckISBN())
			{
				yield return new ValidationResult(
					"Задан не корректный ISBN",
					new[] { nameof(ISBN) });
			}
			if (Authors == null || !Authors.Any())
			{
				yield return new ValidationResult(
					"У книги должен быть как минимум один автор",
					new[] { nameof(Authors) });
			}
		}

		private bool CheckISBN()
		{
			if (string.IsNullOrWhiteSpace(ISBN))
				return false;

			var pure = ISBN.Replace("-", "");
			if (PublishYear >= 2007)
			{
				//if (pure.Length != Const.DigitsInISBN)  
				//	return false;
				//int sum = 0;
				//for (int i = 1; i <= 6; i = i + 2 )
				//	sum = sum + pure[i-1] + 3* pure[i - 1];
				//sum += pure[12];
				//var reminder = sum/10;

				//return (10 - reminder == 7);
				return true;
			}
			{
				if (pure.Length != Const.DigitsInISBNBefore2007)  
					return false;
				int sum = 0;
				for (int i = 1; i <= 10; i++)
					sum = sum + pure[i-1]*i;
				return (sum/11 == 0);
			}

		}
	}
}
