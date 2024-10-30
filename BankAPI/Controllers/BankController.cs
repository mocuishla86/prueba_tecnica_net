using Microsoft.AspNetCore.Mvc;
using BankApplication;
using BankDomain;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("/banks")]
    public class BankController(LoadBanksUseCase loadAllBanksUseCase, GetAllBanksUseCase getAllBanksUseCase, GetBanksByIdUseCase getBanksByIdUseCase, IInternalBankRepository internalBankRepository) : ControllerBase
    {
       
        [HttpPost("LoadBanksFromAPI", Name = "Load banks from external")]
        public ActionResult<int> LoadBanksFromAPI()
        {
            return Ok(loadAllBanksUseCase.LoadBanks());
    
        }

        [HttpGet(Name = "Get banks from internal")]
        public ActionResult<List<Bank>> GetBanksFromDataBase()
        {
            return Ok(getAllBanksUseCase.GetAllBanks());
        }

        [HttpGet("{bankId}", Name = "Get bank by id")]
        public ActionResult<Bank> GetBankById(Guid bankId)
        {
                Bank bank = getBanksByIdUseCase.GetBankById(new GetBanksByIdUseCase.Query { BankId = bankId });
                return Ok(bank);
        }
    }
}