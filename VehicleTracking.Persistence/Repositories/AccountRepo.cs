using VehicleTracking.Common;
using VehicleTracking.Domain.Entities;
using VehicleTracking.Persistence.Exceptions;
using VehicleTracking.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehicleTracking.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly VehicleTrackingDbContext _VehicleTrackingDbContext;
        private readonly IDateTime _machineDateTime;

        public AccountRepository(VehicleTrackingDbContext VehicleTrackingDbContext
            , IDateTime machineDateTime)
        {
            _VehicleTrackingDbContext = VehicleTrackingDbContext;
            _machineDateTime = machineDateTime;
        }

        public BankAccount GetAccountByAccountNumber(string accountNumber)
        {
            if (!accountNumber.IsValid())
            {
                throw new ArgumentNullException("accountNumber was null or Empty.");
            }

            var result = _VehicleTrackingDbContext.BankAccounts.FirstOrDefault(a => a.IsActive && a.AccountNumber == accountNumber);

            if (result == null)
            {
                throw new DomainEntityNotFoundException($"No Account was found by account numbe - {accountNumber}");
            }

            return result;
        }

        public decimal GetCurrentBalanceByAccountId(BankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException("Account arguement was null.");
            }

            var latestStatement = _VehicleTrackingDbContext.Statements.FirstOrDefault(a => a.AccountId == account.Id);

            if (latestStatement == null)
            {
                throw new DomainEntityNotFoundException($"Latest statement was not found, accountNumber = {account.AccountNumber}.");
                                                                
            }

            var transactionsFromLatestStatement = _VehicleTrackingDbContext.Transactions
                                                                .Where(a => a.TransactionDatetime >= latestStatement.StatementDateTime && a.AccountId == account.Id)
                                                                .Select(a => new { Action = a.Action, Amount = a.Amount });

            var withdrawAmountFromLatestStatement = transactionsFromLatestStatement
                                                                .Where(a => a.Action == ActionCode.Withdraw)
                                                                .Sum(a => a.Amount);

            var depositAmountFromLatestStatement = transactionsFromLatestStatement
                                                    .Where(a => a.Action == ActionCode.Deposit)
                                                    .Sum(a => a.Amount);

            var currentBalance = (latestStatement.ClosingBalance + depositAmountFromLatestStatement) 
                                - withdrawAmountFromLatestStatement;


            return currentBalance;
        }
    }
}
