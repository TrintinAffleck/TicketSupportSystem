using Microsoft.AspNetCore.Components;
using TicketSystem.BLL;
using TicketSystem.Models;

namespace TicketSupportWebApp.Components.Pages
{
	public partial class Tickets
	{
		[Inject]
		TicketServices ticketServices { get; set; }

		List<Ticket> tickets { get; set; }

		protected override async Task OnInitializedAsync()
		{
			tickets = ticketServices.GetAllTickets();
			await base.OnInitializedAsync();
		}
	}
}

