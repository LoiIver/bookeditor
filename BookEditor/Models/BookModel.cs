using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace BookEditor.Models
{
    public class BookModel

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
		public int BookId { get; set; }
		[Required]
		[MaxLength(30)]
		public string Title { get; set; }

		[Required]
		[Range(1, 10000)]
		public int NumPages { get; set; }

	
		public int PublisherId { get; set; }

		[Range(1800, Int32.MaxValue)]
		public int? PublishYear { get; set; }

		[ISBN]
		public string ISBN { get; set; }

	    public object Illustration { get; set; }
    }
}
