using System.ComponentModel.DataAnnotations;

namespace Students.Models
{
	public class Student
	{
		public int Id { get; set; }
		[Required]
		public string? FirstName { get; set; }
		[Required]
		public string? LastName { get; set; }

		public int OfYear { get; set; }
		[Required]
		public string? Major { get;  set; }

	}
}

