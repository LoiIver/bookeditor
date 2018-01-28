namespace BookEditor.Data.Models
{
	public  sealed class Book
	{ 
		public long BookId { get; set; }
		public string Title { get; set; }
		public int NumPages { get; set; }
		public long PubHouseId { get; set; }
		public int? PublishYear { get; set; }
		public string ISBN { get; set; }
		public byte[] Illustration { get; set; }
	}
}
