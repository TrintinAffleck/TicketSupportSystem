using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Models;

namespace TicketSystem.ViewModels
{
	public class TicketUserView
	{
		[Key]
		public int TicketID { get; set; }

		[Required(ErrorMessage = "Subject is Required!")]
		[StringLength(200)]
		[Unicode(false)]
		public required string Subject { get; set; }

		[Required(ErrorMessage = "Description is Required!")]
		[Column(TypeName = "text")]
		public required string Description { get; set; }

		[Required(ErrorMessage = "Status is Required!")]
		public required TicketStatus Status { get; set; }

		[Required(ErrorMessage = "Category is Required!")]
		public required TicketCategory Category { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime CreatedAt { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime UpdatedAt { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? ResolvedAt { get; set; }

		public List<TicketComment>? CommentList { get; set; }
	}
}
