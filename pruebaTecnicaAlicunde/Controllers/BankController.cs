using Microsoft.AspNetCore.Mvc;
using BankApplication;
using BankDomain;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("/banks")]
    public class BankController(LoadBanksUseCase loadAllBanksUseCase, GetAllBanksUseCase getAllBanksUseCase, IInternalBankRepository internalBankRepository) : ControllerBase
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
    }
}
