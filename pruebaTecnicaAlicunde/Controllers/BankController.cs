using Microsoft.AspNetCore.Mvc;
using BankApplication;
using BankDomain;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("/banks")]
    public class BankController(LoadBanksUseCase loadAllBanksUseCase) : ControllerBase
    {
        
        [HttpGet(Name = "Get banks from external")]
        public ActionResult<Bank> GetBanks()
        {
            return Ok(loadAllBanksUseCase.GetAllBanks());
        }
    }
}
