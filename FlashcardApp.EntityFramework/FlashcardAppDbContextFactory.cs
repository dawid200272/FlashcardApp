using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.EntityFramework;

public class FlashcardAppDbContextFactory
{
    private readonly string _connectionString;

    public FlashcardAppDbContextFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public FlashcardAppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<FlashcardAppDbContext>();
        options.UseSqlServer(_connectionString);

        return new FlashcardAppDbContext(options.Options);
    }
}
