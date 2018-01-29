using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BookEditor.Data.DataModels;

namespace BookEditor.Data.Models
{
	public sealed class BookModel : IValidatableObject
	{
		public long BookId { get; set; }
		[Required]
		[StringLength(30)]
		public string Title { get; set; }
		[Required]
		[Range(1, 10000)]
		public int NumPages { get; set; }
		public long? PubHouseId { get; set; }
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
				? $"data:image/jpg;base64,{Convert.ToBase64String(book.Illustration)}"
				: "";
			PubHouseId = book.PubHouseId;
			PubHouseName = pubHouse?.Name;
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
				return true;

			var pure = ISBN.Replace("-", "");
			try
			{
				if (PublishYear >= 2007)
				{

					if (pure.Length != Const.DigitsInISBN)
						return false;

					var sum = 0;
					for (var i = 1; i < 12; i = i + 2)
					{
						var first = int.Parse(pure[i - 1].ToString());
						var second = int.Parse(pure[i].ToString());
						sum = sum + first + 3 * second;
					}
					var last = int.Parse(pure[12].ToString());
					var reminder = sum % 10;

					return (10 - reminder == last);
				}
				else
				{
					if (pure.Length != Const.DigitsInISBNBefore2007)
						return false;
					var sum = 0;
					for (var i = 0; i <= 9; i++)
					{
						sum = sum + int.Parse(pure[i].ToString()) * (10 - i);
					}
					return (sum % 11 == 0);
				}
			}
			catch
			{
				return false;
			}
		}
	}
}
