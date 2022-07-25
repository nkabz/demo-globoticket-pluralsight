using System.Collections.Generic;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventList
{
    // EventListVm => Vm = View Model
    public class GetEventsListQuery : IRequest<List<EventListVm>>
    {
        
    }
}