using Microsoft.AspNetCore.Components;
using TicketSystem.BLL;
using TicketSystem.Models;

namespace TicketSupportWebApp.Components.Pages
{
	public partial class Tickets : ComponentBase
	{
		[Inject]
		TicketServices ticketServices { get; set; }

		[Inject]
		NavigationManager navigationManager { get; set; }

		List<Ticket> tickets = [];
		List<TicketCategory> categories = [];

		protected override async Task OnInitializedAsync()
		{
			tickets = ticketServices.GetAllTickets();
			categories = ticketServices.GetTicketCategories();
			await base.OnInitializedAsync();
		}

		private void Save(Ticket ticket)
		{
			
		}

		private void EditTicket(Ticket ticket)
		{
			navigationManager.NavigateTo($"/ticket/{ticket.TicketID}");
		}
	}
}

