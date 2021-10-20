using MediatR;
using SuggestionSystem.Application.Services;
using SuggestionSystem.Data;
using SuggestionSystem.Models;
using SuggestionSystem.PublishedLanguage.Commands;
using SuggestionSystem.PublishedLanguage.Events;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuggestionSystem.Application.WriteOperations
{
    public class CreateAccount : IRequestHandler<MakeNewAccount>
    {
        private readonly IMediator _mediator;
        private readonly AccountOptions _accountOptions;
        //private readonly RatingDbContext _dbContext;
        private readonly NewIban _ibanService;

        public CreateAccount(IMediator mediator, AccountOptions accountOptions, NewIban ibanService)
        {
            _mediator = mediator;
            _accountOptions = accountOptions;
           // _dbContext = dbContext;
            _ibanService = ibanService;
        }

        public async Task<Unit> Handle(MakeNewAccount request, CancellationToken cancellationToken)
        {
            // TODO: implement logic
            return Unit.Value;
        }        
    }
}
