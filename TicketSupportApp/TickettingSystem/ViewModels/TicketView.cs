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
		public int TicketID { get; set; }

		public required string Subject { get; set; }

		public required string Description { get; set; }

		public required string CustomerID { get; set; }

		public required AspNetUser Customer { get; set; }

		public string? AssignedAgentID { get; set; }

		public AspNetUser? AssignedAgent { get; set; }

		public int StatusID { get; set; }

		public required TicketStatus Status { get; set; }

		public int PriorityID { get; set; }

		public required TicketPriority Priority { get; set; }

		public int CategoryID { get; set; }

		public required TicketCategory Category { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public DateTime? ResolvedAt { get; set; }

		public DateTime? SLA_DueDate { get; set; }

		public List<TicketComment>? CommentList { get; set; }
	}
}

