using BankDomain;

namespace BankApplication
{
    public class GetAllBanksUseCase(IInternalBankRepository internalRepository)
    {
        public List<Bank> GetAllBanks()
        {
            return internalRepository.GetAllBanksInDataBase();
        }
    }
}
