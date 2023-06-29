using System.ComponentModel.DataAnnotations.Schema;

namespace backend_api.Entities
{
	[Table("image")]
	public class Image
	{
		public Guid Id { get; set; }

		public string? Name { get; set; }
	}
}

