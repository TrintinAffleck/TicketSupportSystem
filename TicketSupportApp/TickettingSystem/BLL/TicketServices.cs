﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string SaveTicket(int ticketId)
        {
            if (ticketId == 0) return $"No ticket for ticket {ticketId}";
            try
            {
                Ticket ticketToUpdate = _ticketSupportContext.Tickets.Select(a=>a).Where(t=>t.TicketID == ticketId).First();
                _ticketSupportContext.Update(ticketToUpdate);
                _ticketSupportContext.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
