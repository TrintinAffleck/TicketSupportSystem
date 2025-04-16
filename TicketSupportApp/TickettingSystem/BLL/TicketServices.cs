
using TicketSystem.DAL;
using TicketSystem.Models;
using TicketSystem.ViewModels;

namespace TicketSystem.BLL
{
    public class TicketServices
    {
        private readonly TicketSupportContext _ticketSupportContext;

        internal TicketServices(TicketSupportContext context)
        {
            _ticketSupportContext = context;
        }

        public List<TicketView> GetAllTickets()
        {
            return _ticketSupportContext.Tickets.Select(a => new TicketView
            {
                TicketID = a.TicketID,
                Subject = a.Subject,
                Description = a.Description,
                CustomerID = a.CustomerID,
                Customer = a.Customer,
                AssignedAgentID = a.AssignedAgentID,
                AssignedAgent = a.AssignedAgent,
                StatusID = a.StatusID,
                Status = a.Status,
                PriorityID = a.PriorityID,
                Priority = a.Priority,
                CategoryID = a.Category,
                Category = a.CategoryNavigation,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                ResolvedAt  = a.ResolvedAt,
                SLA_DueDate = a.SLA_DueDate,
                CommentList = a.TicketComments.Select(a=>a).ToList<TicketComment>(),
            })
            .ToList<TicketView>();
                
        }
        public List<TicketCategory> GetTicketCategories()
        {
            return _ticketSupportContext.TicketCategories.Select(a => a)
                .ToList();
        }

        public List<TicketStatus> GetTicketStatuses()
        {
            return _ticketSupportContext.TicketStatuses.Select(a => a)
                .ToList();
        }
        public async Task<Ticket> SaveTicket(TicketView ticket)
        {
            List<string> errorList = new List<string>();

            if (ticket == null)
            {
                errorList.Add("Ticket is null.");

                //TODO Refactor for a new Ticket; give this attributes; 
                //TicketView newTicket = new TicketView();
            }

            if (ticket.TicketID <= 0)
            {
                errorList.Add($"No ticket for ticket {ticket.TicketID}");
            }
            
            try
            {
			    Ticket ticketToUpdate = _ticketSupportContext.Tickets
				    .Select(a => a)
				    .Where(t => t.TicketID == ticket.TicketID)
				    .First();

			    ticketToUpdate.TicketID = ticket.TicketID;
                ticketToUpdate.Subject = ticket.Subject;
                ticketToUpdate.AssignedAgentID = ticket.AssignedAgentID;
                ticketToUpdate.Description = ticket.Description;
                ticketToUpdate.Category = ticket.CategoryID;
                ticketToUpdate.CategoryNavigation = ticket.Category;
                ticketToUpdate.StatusID = ticket.StatusID;
                ticketToUpdate.Status = ticket.Status;
                ticketToUpdate.TicketComments = ticket.CommentList;
                ticketToUpdate.UpdatedAt = DateTime.Now;

                _ticketSupportContext.Update(ticketToUpdate);
                await _ticketSupportContext.SaveChangesAsync();
                return ticketToUpdate;
            }
            catch (ArgumentNullException ex)
            {
                errorList.Add(ex.Message);
                return null;
            }
            catch (InvalidOperationException ex)
            {
				errorList.Add(ex.Message);
				return null;
            }

        }
    }
}
