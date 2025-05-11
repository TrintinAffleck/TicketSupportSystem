using Microsoft.AspNetCore.Components;
using MudBlazor;
using UserServices.BLL;
using TicketSystem.BLL;
using TicketSystem.Models;
using TicketSystem.ViewModels;
using Utilities;
using Mono.TextTemplating;
namespace TicketSupportWebApp.Components.Pages
{
	public partial class Tickets : ComponentBase
	{
		[Inject]
		TicketServices? TicketServices { get; set; }

		[Inject]
		NavigationManager? NavigationManager { get; set; }

		[Inject]
		UserService? UserServices { get; set; }

		IEnumerable<TicketView>? tickets = null;
		List<TicketCategory> categories = [];
		List<TicketStatus> statuses = [];
		readonly List<string> errors = [];
		List<AspNetUser> users = [];
		private MudDataGrid<TicketView>? dataGrid;
		private MudForm? ticketForm;
		private bool isTicketsLoading = true;
		private string? searchString = null;
		protected override async Task OnInitializedAsync()
		{
			ArgumentNullException.ThrowIfNull(UserServices);
			ArgumentNullException.ThrowIfNull(TicketServices);
			ArgumentNullException.ThrowIfNull(NavigationManager);
			users = await UserServices.GetAllUsers();
			await base.OnInitializedAsync();

		}

		private async Task<GridData<TicketView>>? LoadTickets(GridState<TicketView> state)
		{
			ArgumentNullException.ThrowIfNull(state);
			try
			{
				tickets = await TicketServices.GetAllTickets();
				if (tickets != null)
				{
					isTicketsLoading = false;
				}
				categories = await TicketServices.GetTicketCategories();
				statuses = await TicketServices.GetTicketStatuses();
				
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

