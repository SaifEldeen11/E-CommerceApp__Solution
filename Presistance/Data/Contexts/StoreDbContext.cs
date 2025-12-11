using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Data.Contexts
{
    public class StoreDbContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
