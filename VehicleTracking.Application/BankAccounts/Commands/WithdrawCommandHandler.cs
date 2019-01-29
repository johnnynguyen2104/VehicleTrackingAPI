using VehicleTracking.Application.Exceptions;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Common;
using VehicleTracking.Domain.Entities;
using VehicleTracking.Persistence;
using VehicleTracking.Persistence.Exceptions;
using VehicleTracking.Persistence.Interfaces;
using VehicleTracking.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VehicleTracking.Application.BankAccounts.Commands
{
    public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand>
    {
        private readonly ICurrencyService _currencyService;
        private readonly VehicleTrackingDbContext _VehicleTrackingDbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly IDateTime _machineDateTime;

        public WithdrawCommandHandler(ICurrencyService currencyService
            , VehicleTrackingDbContext VehicleTrackingDbContext
            , IDateTime machineDateTime)
        {
            _currencyService = currencyService;
            _VehicleTrackingDbContext = VehicleTrackingDbContext;
            _machineDateTime = machineDateTime;

            _accountRepository = new AccountRepository(_VehicleTrackingDbContext, _machineDateTime);
        }

        public async Task<Unit> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var account = _accountRepository.GetAccountByAccountNumber(request.AccountNumber);

            if (account == null)
            {
                throw new WithdrawFailException($"No Account was found by account numbe - {request.AccountNumber}");
            }

            decimal rates = 1;

            if (request.Currency != account.Currency)
            {
                rates = await _currencyService.GetRates(account.Currency, request.Currency);

                if (rates <= 0)
                {
                    throw new WithdrawFailException($"Invalid Currency {request.Currency}.");
                }
            }

            var currentBalance = _accountRepository.GetCurrentBalanceByAccountId(account);
            var actualAmout = (request.Amount * rates);

            if (actualAmout <= 0)
            {
                throw new WithdrawFailException($"Invalid actual amount after exchanged ({actualAmout}).");
            }

            if (actualAmout > currentBalance)
            {
                throw new WithdrawFailException("The current balance not enough to withdraw.");
            }

            _VehicleTrackingDbContext.Transactions.Add(new AccountTransaction() {
                AccountId = account.Id,
                Amount = actualAmout,
                Action = ActionCode.Withdraw,
                TransactionDatetime = _machineDateTime.Now
            });

            account.LastActivityDate = _machineDateTime.Now;

            if (_VehicleTrackingDbContext.SaveChanges() <= 0)
            {
                throw new WithdrawFailException($"Withdraw fail with account number - {request.AccountNumber}.");
            }

            return Unit.Value;
        }
    }
}
