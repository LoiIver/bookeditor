

using System.Security.Policy;

namespace BookEditor.Data.Models
{
	public sealed class BookAuthors
	{
		public long BookAuthorId { get; set; }
		public long AuthorId { get; set; }
		public long BookId { get; set; }
	}
}
