using BankDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public interface IInternalBankRepository
    {
        public List<Bank> GetAllBanksInDataBase();
        Bank GetBankById(Guid id);
        public void SaveBanks(List<Bank> banks);
    }
}
