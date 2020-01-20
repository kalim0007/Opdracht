using Opdracht.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht.Sql
{
    public class DataContext :DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }
        public DbSet<Order> Orders { get; set; }
    }
}
