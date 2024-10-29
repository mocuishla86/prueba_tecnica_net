using BankDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class LoadBanksUseCase(IExternalBankRepository externalBankRepository, IInternalBankRepository internalBankRepository)
    {
        
       private readonly IExternalBankRepository externalBankRepository = externalBankRepository;
       private readonly IInternalBankRepository internalBankRepository = internalBankRepository;
      
        public int LoadBanks()
        {
            var externalBanks = externalBankRepository.GetAllBanks();

            internalBankRepository.SaveBanks(externalBanks);

            return externalBanks.Count;

        }


    }
}
