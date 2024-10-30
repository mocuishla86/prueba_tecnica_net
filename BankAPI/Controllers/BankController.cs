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
        public async Task<IActionResult> LoadBanksFromAPI()
        {
            var count = await loadAllBanksUseCase.LoadBanks();
            return Ok(new { Message = $"{count} banks loaded successfully."});
    
        }

        [HttpGet(Name = "Get banks from internal")]
        public ActionResult<List<Bank>> GetBanksFromDataBase()
        {
            return Ok(getAllBanksUseCase.GetAllBanks());
        }

        [HttpGet("{bankId}", Name = "Get bank by id")]
        public ActionResult<Bank> GetBankById(Guid bankId)
        {
            try
            {
                Bank bank = getBanksByIdUseCase.GetBankById(new GetBanksByIdUseCase.Query { BankId = bankId });
                return Ok(bank);
            }
            catch(BankNotFoundException bankNotFoundException) 
            {
                return NotFound(bankNotFoundException.Message);
            }
        }
    }
}
