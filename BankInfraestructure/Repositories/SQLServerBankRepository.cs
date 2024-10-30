using BankApplication;
using BankDomain;
using BankInfraestructure.Context;
using BankInfraestructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankInfraestructure.Repositories
{
    public class SQLServerBankRepository(AppDbContext dbContext) : IInternalBankRepository
    {
        public List<Bank> GetAllBanksInDataBase()
        {
            return dbContext.Banks.Select(bankEntity => ToDomain(bankEntity)).ToList();
        }

        public Bank GetBankById(Guid bankId)
        {
            BankEntity bankEntity = dbContext.Banks.SingleOrDefault(bankEntity => bankEntity.Id == bankId) 
                ?? throw new BankNotFoundException(bankId);

            return ToDomain(bankEntity);
        }

        public void SaveBanks(List<Bank> banks)
        {
            List<BankEntity> entities = ToEntity(banks);

            dbContext.Banks.AddRange(entities);

            dbContext.SaveChanges();
        }

        private static Bank ToDomain(BankEntity bankEntity)
        {
            return new() { Id = bankEntity.Id, Name = bankEntity.Name, Bic = bankEntity.Bic, Country = bankEntity.Country };
        }

        private static List<BankEntity> ToEntity(List<Bank> banks)
        {
           return banks.Select(bank => new BankEntity
           { 
               Id = bank.Id, 
               Name = bank.Name, 
               Bic = bank.Bic, 
               Country = bank.Country
           })
           .ToList();
        }

        public void Clear()
        {
            //https://stackoverflow.com/a/10450893
            dbContext.Database.ExecuteSqlRaw("delete from Banks");
        }
    }
}
