using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.DAL;
using TicketSystem.Models;

namespace TicketSystem.BLL
{
    public class TicketServices
    {
        private readonly TicketSupportContext _ticketSupportContext;

        internal TicketServices(TicketSupportContext context)
        {
            _ticketSupportContext = context;
        }

        public List<Ticket> GetAllTickets()
        {
            return _ticketSupportContext.Tickets.Select(a => a).ToList();
        }
    }
}
