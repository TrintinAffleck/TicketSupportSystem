using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Design.Internal;
using MudBlazor;
using TicketSystem.BLL;
using TicketSystem.Models;
using TicketSystem.ViewModels;
using Utilities;
namespace TicketSupportWebApp.Components.Pages
{
	public partial class Tickets : ComponentBase
	{
		[Inject]
		TicketServices? TicketServices { get; set; }

		[Inject]
		NavigationManager? NavigationManager { get; set; }

		List<TicketView> tickets = [];
		List<TicketCategory> categories = [];
		List<TicketStatus> statuses = new List<TicketStatus>();
		List<string> errors = new List<string>();

		TicketView test;

		protected override async Task OnInitializedAsync()
		{
			if (TicketServices == null)
			{
				throw new ArgumentNullException("Ticket Services is not available.");
			}
			if (NavigationManager == null)
			{
				throw new ArgumentNullException("Navigation Manager is not available.");
			}
			try
			{
				tickets = TicketServices.GetAllTickets();
				categories = TicketServices.GetTicketCategories();
				statuses = TicketServices.GetTicketStatuses();
			}
			catch (ArgumentNullException ex)
			{
				errors.Add(Utils.GetInnerException(ex).Message);
			}
			

			await base.OnInitializedAsync();
		}

		private void Save(TicketView ticket)
		{
			try
			{
				TicketServices.SaveTicket(ticket);
			}
			catch (ArgumentException ex)
			{
				errors.Add(Utils.GetInnerException(ex).Message);
			}

		}

		

		

		
	}
}

