using BankDomain;

namespace BankApplication
{
    public class GetAllBanksUseCase(IInternalBankRepository repository)
    {
        public List<Bank> GetAllBanks()
        {
            return repository.GetAllBanks();
        }
    }
}
