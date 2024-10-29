using BankDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class GetBanksByIdUseCase(IInternalBankRepository internalBankRepository)
    {
        public Bank GetBankById(Query query)
        {
            return internalBankRepository.GetBankById(query.BankId);
        }

        public class Query
        {
            public Guid BankId { get; set; }
        }
    }

}
