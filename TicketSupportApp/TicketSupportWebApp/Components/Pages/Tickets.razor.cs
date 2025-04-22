using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Design.Internal;
using MudBlazor;
using TicketSystem.BLL;
using TicketSystem.Models;
using TicketSystem.ViewModels;
using Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace TicketSupportWebApp.Components.Pages
{
	public partial class Tickets : ComponentBase
	{
		[Inject]
		TicketServices? TicketServices { get; set; }

		[Inject]
		NavigationManager? NavigationManager { get; set; }

		IEnumerable<TicketView>? tickets = null;
		List<TicketCategory> categories = [];
		List<TicketStatus> statuses = new List<TicketStatus>();
		List<string> errors = new List<string>();
		private MudDataGrid<TicketView> dataGrid;
		private MudForm ticketForm;
		private bool isTicketsLoading = true;
		private string? searchString = null;
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

			await base.OnInitializedAsync();
		}

		private async Task<GridData<TicketView>> LoadTickets(GridState<TicketView> state)
		{
			if (state == null)
			{
				throw new ArgumentNullException("state is null");
			}
			try
			{
				tickets = await TicketServices.GetAllTickets();
				if (tickets != null)
				{
					isTicketsLoading = false;
				}
				categories = TicketServices.GetTicketCategories();
				statuses = TicketServices.GetTicketStatuses();

				tickets = tickets.Where(ticket =>
				{
					if (string.IsNullOrWhiteSpace(searchString))
						return true;
					if (ticket.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
						return true;
					if (ticket.Subject.Contains(searchString, StringComparison.OrdinalIgnoreCase))
						return true;
					if ($"{ticket.TicketID} {ticket.Category.CategoryName} {ticket.Status.StatusName}".Contains(searchString))
						return true;
					return false;
				}).ToArray();

				int totalItems = tickets.Count();

				SortDefinition<TicketView> sortDefinition = state.SortDefinitions.FirstOrDefault();
				if (sortDefinition != null)
				{
					switch (sortDefinition.SortBy)
					{
						case nameof(TicketView.TicketID):
							tickets = tickets.OrderByDirection(
								sortDefinition.Descending ? SortDirection.Descending : SortDirection.Ascending,
								o => o.TicketID
							);
							break;
						case nameof(TicketView.Subject):
							tickets = tickets.OrderByDirection(
								sortDefinition.Descending ? SortDirection.Descending : SortDirection.Ascending,
								o => o.Subject
							);
							break;
						case nameof(TicketView.Description):
							tickets = tickets.OrderByDirection(
								sortDefinition.Descending ? SortDirection.Descending : SortDirection.Ascending,
								o => o.Description
							);
							break;
						case nameof(TicketView.Category.CategoryName):
							tickets = tickets.OrderByDirection(
								sortDefinition.Descending ? SortDirection.Descending : SortDirection.Ascending,
								o => o.Category.CategoryName
							);
							break;
						case nameof(TicketView.Status.StatusName):
							tickets = tickets.OrderByDirection(
								sortDefinition.Descending ? SortDirection.Descending : SortDirection.Ascending,
								o => o.Status.StatusName
							);
							break;
					}
				}
				var pagedData = tickets.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
				return new GridData<TicketView>
				{
					TotalItems = totalItems,
					Items = pagedData,
				};
			}
			catch (ArgumentNullException ex)
			{
				errors.Add(Utils.GetInnerException(ex).Message);
				return null;
			}
		}
		private Task OnSearch(string text)
		{
			searchString = text;
			return dataGrid.ReloadServerData();
		}
		private async void Save(TicketView ticket)
		{
			try
			{
				await TicketServices.SaveTicket(ticket);
			}
			catch (ArgumentException ex)
			{
				errors.Add(Utils.GetInnerException(ex).Message);
			}

		}

		private void PrintSomething()
		{
			Console.WriteLine("Printed Something");
		}

	}
}

