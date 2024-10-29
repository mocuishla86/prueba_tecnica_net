using BankDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class LoadBanksUseCase(IExternalBankRepository repository)
    {
        public List<Bank> GetAllBanks()
        {
            return repository.GetAllBanks();
        } 
    }
}
