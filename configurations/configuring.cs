using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Atm_sql.configurations
{
    public class userconfiguring:IEntityTypeConfiguration<users>
    {
        public void Configure(EntityTypeBuilder<users> builder)
        {
            builder.HasKey(u => u.id);

            builder.Property(u => u.id).ValueGeneratedOnAdd().HasAnnotation("sqlserver:identity", "1,1");
        }
    }
}
