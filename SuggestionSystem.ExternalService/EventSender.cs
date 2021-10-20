using MediatR;
using SuggestionSystem.PublishedLanguage.Commands;
using SuggestionSystem.PublishedLanguage.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuggestionSystem.ExternalService
{
    public class AllEventsHandler : INotificationHandler<INotification>
    {
        public Task Handle(INotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(notification);
            return Task.CompletedTask;
        }
    }

    //public class AccountMadeEventHandler : INotificationHandler<AccountMade>
    //{
    //    public Task Handle(AccountMade notification, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class EnrollCustomerCommandHandler : IRequestHandler<EnrollCustomer>
    //{
    //    public Task<Unit> Handle(EnrollCustomer request, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}
