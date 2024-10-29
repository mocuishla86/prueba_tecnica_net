using BankInfraestructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankInfraestructure.Context
{
    public class BankConfiguration : IEntityTypeConfiguration<BankEntity>
    {
        public void Configure(EntityTypeBuilder<BankEntity> builder)
        {
            builder.HasKey(u => u.Id);
        }
    }
}
