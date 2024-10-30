using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class BankNotFoundException(Guid bankId) : Exception
    {
        public override string Message => $"Bank {bankId} not found";
    }
}
