using Microsoft.AspNetCore.Components;
using TicketSystem.BLL;
using TicketSystem.Models;
using TicketSystem.ViewModels;

namespace TicketSupportWebApp.Components.Pages
{
	public partial class Tickets : ComponentBase
	{
		[Inject]
		TicketServices ticketServices { get; set; }

		[Inject]
		NavigationManager navigationManager { get; set; }

		List<TicketView> tickets = [];
		List<TicketCategory> categories = [];
		List<TicketStatus> statuses = new List<TicketStatus>();

		string test = "";

		protected override async Task OnInitializedAsync()
		{
			tickets = ticketServices.GetAllTickets();
			categories = ticketServices.GetTicketCategories();
			statuses = ticketServices.GetTicketStatuses();
			await base.OnInitializedAsync();
		}

		private void Save(int ticketId)
		{
			test = $"TicketId = {ticketId}";
			//ticketServices.SaveTicket(ticketId);
		}

		private void EditTicket(Ticket ticket)
		{
			navigationManager.NavigateTo($"/ticket/{ticket.TicketID}");
		}

		
	}
}

