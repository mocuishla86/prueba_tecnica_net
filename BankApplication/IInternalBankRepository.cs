﻿using BankDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public interface IInternalBankRepository
    {
        public List<Bank> GetAllBanks();
    }
}