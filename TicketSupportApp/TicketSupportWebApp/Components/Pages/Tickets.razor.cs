using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Design.Internal;
using MudBlazor;
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

		TicketView test;

		protected override async Task OnInitializedAsync()
		{
			tickets = ticketServices.GetAllTickets();
			categories = ticketServices.GetTicketCategories();
			statuses = ticketServices.GetTicketStatuses();
			await base.OnInitializedAsync();
		}

		private void Save(TicketView ticket)
		{
			
			ticketServices.SaveTicket(ticket);
		}

		private void OnCategoryChange(TicketView ticketView)
		{
			test = ticketView;
			Console.WriteLine(ticketView.Category);
		}

		
	}
}

