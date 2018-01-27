using System.Collections.Generic;


namespace BookEditor.Data.Models
{
	public  sealed class Book

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
		public string Title { get; set; }
		public int NumPages { get; set; }
		public long PubHouseId { get; set; }
		public int? PublishYear { get; set; }
		public string ISBN { get; set; }
		public object Illustration { get; set; }
	}
}
