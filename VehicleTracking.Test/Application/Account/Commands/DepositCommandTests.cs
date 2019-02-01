using VehicleTracking.Application.BankAccounts.Commands;
using VehicleTracking.Application.BankAccounts.Validations;
using VehicleTracking.Application.Exceptions;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Common;
using VehicleTracking.Domain.Entities;
using VehicleTracking.Infrastructure;
using VehicleTracking.Persistence;
using VehicleTracking.Persistence.Interfaces;
using VehicleTracking.Persistence.Repositories;
using VehicleTracking.Test.Infrastructure;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VehicleTracking.Test.Application.Account.Commands
{
    [TestFixture]
    public class DepositCommandTests
    {

        //DepositCommandValidation validation = new DepositCommandValidation();
        //VehicleTrackingDbContext context;
        //DepositCommandHandler handler;
        //IDateTime dateTime = new FakeMachineDateTime();
        //ICurrencyService currencyService = new CurrencyService();
        //IBaseRepository _accountRepository;

        //[SetUp]
        //public void Setup()
        //{
        //    context = BankingContext.Create();
        //    handler = new DepositCommandHandler(currencyService, context, dateTime);
        //    _accountRepository = new AccountRepository(context, dateTime);
        //}

        //[Test]
        //public void Deposit_GivenUnSupportCurrency_ShouldThrowBusinessErrorException()
        //{
        //    var command = new DepositCommand() { AccountNumber = "1", Amount = 3, Currency = "VND" };

        //    var result = Assert.ThrowsAsync<DepositFailException>(() => handler.Handle(command, CancellationToken.None));

        //    result.ShouldNotBeNull();
        //    result.Message.Contains("Invalid Currency VND.");
        //}


        //[Test]
        //public void Deposit_GivenNegativeExchangedAmount_ShouldThrowBusinessErrorException()
        //{
        //    var command = new DepositCommand() { AccountNumber = "1", Amount = -3, Currency = "EUR" };

        //    var result = Assert.ThrowsAsync<DepositFailException>(() => handler.Handle(command, CancellationToken.None));

        //    result.ShouldNotBeNull();
        //    result.Message.Contains($"Invalid actual amount {command.Amount * 40}");
        //}

        //[Test]
        //public async Task Deposit_GivenCorrectRequest_WithSameCurrency_ShouldDepositSuccessfulWithCorrectAmount()
        //{
        //    DepositCommand command = new DepositCommand() { AccountNumber = "1", Amount = 30, Currency = "THB" };
        //    await handler.Handle(command, CancellationToken.None);

        //    var depositAccount = context.BankAccounts.First(a => a.AccountNumber == command.AccountNumber);

        //    var currentBalance = _accountRepository.GetCurrentBalanceByAccountId(depositAccount);
        //    var withdrawTransaction = context.Transactions.FirstOrDefault(a => a.AccountId == depositAccount.Id
        //    && a.TransactionDatetime == dateTime.Now
        //    && a.Action == ActionCode.Deposit);


        //    currentBalance.ShouldBe(130);
        //    withdrawTransaction.ShouldNotBeNull();
        //    withdrawTransaction.Amount.ShouldBe(30);
        //    depositAccount.LastActivityDate.ShouldBe(dateTime.Now);
        //}

        //[TestCaseSource("CorrectDepositDifferentCurrency")]
        //public async Task Deposit_GivenCorrectRequest_WithDifferenntCurrency_ShouldDepositSuccessfulWithCorrectAmount(DepositCommand command, decimal expectedAmount, decimal transactionAmount)
        //{
        //    await handler.Handle(command, CancellationToken.None);

        //    var withdrawedAccount = context.BankAccounts.First(a => a.AccountNumber == command.AccountNumber);

        //    var currentBalance = _accountRepository.GetCurrentBalanceByAccountId(withdrawedAccount);
        //    var withdrawTransaction = context.Transactions.FirstOrDefault(a => a.AccountId == withdrawedAccount.Id
        //    && a.TransactionDatetime == dateTime.Now
        //    && a.Action == ActionCode.Deposit);

        //    currentBalance.ShouldBe(expectedAmount);
        //    withdrawTransaction.ShouldNotBeNull();
        //    withdrawTransaction.Amount.ShouldBe(transactionAmount);
        //    withdrawedAccount.LastActivityDate.ShouldBe(dateTime.Now);
        //}


        //private static object[] CorrectDepositDifferentCurrency = new object[] {
        //    new object[] { new DepositCommand() { AccountNumber = "1", Amount = 1, Currency= "USD"  }, 133m, 33m  }
        //    ,new object[] { new DepositCommand() { AccountNumber = "1", Amount = 1, Currency= "EUR"  }, 140m, 40m  }
        //};
    }
}
