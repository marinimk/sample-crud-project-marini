using CRUDApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CRUDApp.Data
{
    public class AppDBContext: DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
            
        }

        //what want to be CRUD

        public DbSet<Customer> Customer { get; set; }
    }
}
