using BankApplication;
using BankDomain;

namespace BankInfraestructure
{
    public class FakeBankAPI : IExternalBankRepository
    {
        private List<Bank> banks;

        public FakeBankAPI()
        {
            banks = new List<Bank>
            {
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "Banco Nacional",
                    Bic = "BNATXX01",
                    Country = "AR"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "BNE",
                    Bic = "BNTTXX02",
                    Country = "ES"
                }

            };
        }
        public List<Bank> GetAllBanks()
        {
            return banks;
        }
    }
}
