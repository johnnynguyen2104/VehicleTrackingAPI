using VehicleTracking.Application.Exceptions;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Common;
using VehicleTracking.Domain.Entities;
using VehicleTracking.Persistence;
using VehicleTracking.Persistence.Interfaces;
using VehicleTracking.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VehicleTracking.Application.BankAccounts.Commands
{
    //public class DepositCommandHandler : IRequestHandler<DepositCommand>
    //{
    //    private readonly ICurrencyService _currencyService;
    //    private readonly VehicleTrackingDbContext _VehicleTrackingDbContext;
    //    private readonly IAccountRepository _accountRepository;
    //    private readonly IDateTime _machineDateTime;

    //    public DepositCommandHandler(ICurrencyService currencyService
    //        , VehicleTrackingDbContext VehicleTrackingDbContext
    //        , IDateTime machineDateTime)
    //    {
    //        _currencyService = currencyService;
    //        _VehicleTrackingDbContext = VehicleTrackingDbContext;
    //        _machineDateTime = machineDateTime;

    //        _accountRepository = new AccountRepository(_VehicleTrackingDbContext, _machineDateTime);
    //    }


    //    public async Task<Unit> Handle(DepositCommand request, CancellationToken cancellationToken)
    //    {
    //        var account = _accountRepository.GetAccountByAccountNumber(request.AccountNumber);

    //        if (account == null)
    //        {
    //            throw new DepositFailException("Account Number - {request.AccountNumber} could not be found.");
    //        }

    //        decimal rates = 1;

    //        if (request.Currency != account.Currency)
    //        {
    //            rates = await _currencyService.GetRates(account.Currency, request.Currency);

    //            if (rates <= 0)
    //            {
    //                throw new DepositFailException($"Invalid Currency {request.Currency}");
    //            }
    //        }

    //        var actualAmout = (request.Amount * rates);

    //        if (actualAmout <= 0)
    //        {
    //            throw new DepositFailException($"Invalid actual amount {actualAmout}");
    //        }

    //        _VehicleTrackingDbContext.Transactions.Add(new AccountTransaction()
    //        {
    //            AccountId = account.Id,
    //            Amount = actualAmout,
    //            Action = ActionCode.Deposit,
    //            TransactionDatetime = _machineDateTime.Now
    //        });

    //        account.LastActivityDate = _machineDateTime.Now;

    //        if (_VehicleTrackingDbContext.SaveChanges() <= 0)
    //        {
    //            throw new WithdrawFailException($"Withdraw fail with account number - {request.AccountNumber}.");
    //        }

    //        return Unit.Value;
    //    }
    //}
}
