using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace BookEditor.Data.Models
{
	public  sealed class BookModel :IValidatableObject

	{
		/*
		 - заголовок (обязательный параметр, не более 30 символов)
- список авторов (книга должна содержать хотя бы одного автора)
	- имя автора (обязательный параметр, не более 20 символов)
	- фамилия автора (обязательный параметр, не более 20 символов)
- количество страниц (обязательный параметр, больше 0 и не более 10000)
- название издательства (опциональный параметр, не более 30 символов)
- год публикации (не раньше 1800)
- ISBN с валидацией (http://en.wikipedia.org/wiki/International_Standard_Book_Number)
- изображение (опциональный параметр)
 */
		public long BookId { get; set; }
		[Required]
		public string Title { get; set; }
		public int NumPages { get; set; }
		public long PubHouseId { get; set; }
		public string PubHouseName { get; set; }
		[Range(1800, Int32.MaxValue)]
		public int? PublishYear { get; set; }
		public string ISBN { get; set; }
		public object Illustration { get; set; }

		public List<long> Authors { get; set; }
		public string AuthorsNames { get; set; }

		IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
		{
			if (!CheckISBN())
			{
				yield return new ValidationResult(
					"Задан не корректный ISBN",
					new [] { nameof(ISBN) });
			}
			if (Authors == null  || !Authors.Any())
			{
				yield return new ValidationResult(
					"У книги должен быть как минимум один автор",
					new[] { nameof(Authors) });
			}
		}

		private bool CheckISBN()
		{
			return true;
		}
	}
}
