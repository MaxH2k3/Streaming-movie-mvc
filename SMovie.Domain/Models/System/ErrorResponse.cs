using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SMovie.Domain.Models
{
	public class ErrorResponse
	{
		public int StatusCode { get; set; }
		public string Message { get; set; } = null!;
		public string Location { get; set; } = null!;
		public string Detail { get; set; } = null!;

		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}
