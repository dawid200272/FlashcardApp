using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.EntityFramework;

public class FlashcardAppDbContextFactory : IDesignTimeDbContextFactory<FlashcardAppDbContext>
{
    public FlashcardAppDbContext CreateDbContext(string[] args = null)
    {
        var options = new DbContextOptionsBuilder<FlashcardAppDbContext>();
        options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FlashcardAppDB;Trusted_Connection=True;");

        return new FlashcardAppDbContext(options.Options);
    }
}
