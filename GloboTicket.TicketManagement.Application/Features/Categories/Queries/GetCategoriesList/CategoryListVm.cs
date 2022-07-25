using System;
using System.Collections.Generic;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventList;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList
{
    public class CategoryListVm
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
}