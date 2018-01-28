namespace BookEditor.Data.Models
{
	public sealed class Author

	{
		/*
		 
- список авторов (книга должна содержать хотя бы одного автора)
	- имя автора (обязательный параметр, не более 20 символов)
	- фамилия автора (обязательный параметр, не более 20 символов)
 
 */
		public long AuthorId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
  
	}
}
