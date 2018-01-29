using System.ComponentModel.DataAnnotations;

namespace BookEdtor.DataModel.Models
{
	public sealed class AuthorModel
	{
		public long AuthorId { get; set; }
		[Required]
		[StringLength(30)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(30)]
		public string LastName { get; set; }
	}
}
