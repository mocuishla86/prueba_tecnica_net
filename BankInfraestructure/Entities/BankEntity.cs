using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankInfraestructure.Entities
{
    public class BankEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Bic { get; set; }
        public string Country { get; set; }
    }
}
