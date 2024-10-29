using BankApplication;
using BankDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankInfraestructure
{
    public class InMemoryBanksDataBase : IInternalBankRepository
    {
        private readonly List<Bank> _banks = new();

        public void SaveBanks(List<Bank> banks)
        {
            _banks.AddRange(banks);
        }
        public List<Bank> GetAllBanksInDataBase()
        {
            return _banks;
        }

    }
}
