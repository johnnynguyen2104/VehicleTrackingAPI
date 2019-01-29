using VehicleTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Persistence.Interfaces
{
    public interface IAccountRepository
    {
        decimal GetCurrentBalanceByAccountId(BankAccount account);

        BankAccount GetAccountByAccountNumber(string accountNumber);
    }
}
