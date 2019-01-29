using VehicleTracking.Application.BankAccounts.Exceptions;
using VehicleTracking.Application.BankAccounts.Models;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Common;
using VehicleTracking.Persistence;
using VehicleTracking.Persistence.Interfaces;
using VehicleTracking.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VehicleTracking.Application.BankAccounts.Queries
{
    public class InquiryByAccountNumberQueryHandler : IRequestHandler<InquiryByAccountNumberQuery, InquiryViewModel>
    {

        private readonly VehicleTrackingDbContext _VehicleTrackingDbContext;
        private readonly IAccountRepository _accountRepository;

        public InquiryByAccountNumberQueryHandler(VehicleTrackingDbContext VehicleTrackingDbContext
            , IDateTime machineDateTime)
        {
            _VehicleTrackingDbContext = VehicleTrackingDbContext;

            _accountRepository = new AccountRepository(_VehicleTrackingDbContext, machineDateTime);
        }

        public Task<InquiryViewModel> Handle(InquiryByAccountNumberQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => {


                var account = _accountRepository.GetAccountByAccountNumber(request.AccountNumber);

                if (account == null)
                {
                    throw new InquiryFailException($"Account Number - {request.AccountNumber} could not be found.");
                }

                return new InquiryViewModel()
                {
                    AccountNumber = account.AccountNumber,
                    Currency = account.Currency,
                    Balance = _accountRepository.GetCurrentBalanceByAccountId(account)
                };

            });

        }
    }
}
