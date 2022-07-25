using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        public CreateEventCommandValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is Required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Date).NotEmpty().WithMessage("{PropertyName} is Required")
                .NotNull()
                .GreaterThan(DateTime.Now);

            RuleFor(e => e)
                .MustAsync(EventNameAndDateUnique)
                .WithMessage("An event with same name and date already exists");
            
            RuleFor(p => p.Price).NotEmpty().WithMessage("{PropertyName} is Required")
                .NotNull()
                .GreaterThan(0);
        }

        private async Task<bool> EventNameAndDateUnique(CreateEventCommand e, CancellationToken token)
        {
            return !(await _eventRepository.IsEventNameAndDateUnique(e.Name, e.Date));
        }
    }
}