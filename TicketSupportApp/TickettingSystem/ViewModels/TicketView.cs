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
	public class TicketView
	{
		[Key]
		public int TicketID { get; set; }
		
		[Required]
		[StringLength(200)]
		[Unicode(false)]
		public required string Subject { get; set; }

		[Required]
		[Column(TypeName = "text")]
		public required string Description { get; set; }

		[Required]
		[StringLength(450)]
		public required string CustomerID { get; set; }

		[Required]
		public required AspNetUser Customer { get; set; }

		[StringLength(450)]
		public string? AssignedAgentID { get; set; }

		public AspNetUser? AssignedAgent { get; set; }

		[Required]
		public int StatusID { get; set; }

		[Required]
		public required TicketStatus Status { get; set; }

		[Required]
		public int PriorityID { get; set; }

		[Required]
		public required TicketPriority Priority { get; set; }
		[Required]
		public int CategoryID { get; set; }
		[Required]
		public required TicketCategory Category { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime CreatedAt { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime UpdatedAt { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? ResolvedAt { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? SLA_DueDate { get; set; }

		public List<TicketComment>? CommentList { get; set; }
	}
}

