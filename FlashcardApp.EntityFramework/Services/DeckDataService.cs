using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashcardApp.EntityFramework.Services.Common;

namespace FlashcardApp.EntityFramework.Services;

public class DeckDataService : IDataService<Deck>
{
    private readonly FlashcardAppDbContextFactory _contextFactory;
    private readonly NonQueryDataService<Deck> _nonQueryDataService;

    public DeckDataService(FlashcardAppDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
        _nonQueryDataService = new NonQueryDataService<Deck>(contextFactory);
    }

    public async Task<Deck> Create(Deck entity)
    {
        return await _nonQueryDataService.Create(entity);
    }

    public async Task<bool> Delete(int id)
    {
        return await _nonQueryDataService.Delete(id);
    }

    public async Task<Deck> Get(int id)
    {
        using (FlashcardAppDbContext context = _contextFactory.CreateDbContext())
        {
            Deck entity = await context.Decks
                .Include(d => d.Cards)
                .ThenInclude(c => c.CardTemplate)
                .FirstOrDefaultAsync(e => e.Id == id);

            return entity;
        }
    }

    public async Task<IEnumerable<Deck>> GetAll()
    {
        using (FlashcardAppDbContext context = _contextFactory.CreateDbContext())
        {
            IEnumerable<Deck> entities = await context.Decks
                .Include(d => d.Cards)
                .ThenInclude(c => c.CardTemplate)
                .ToListAsync();

            return entities;
        }
    }

    public async Task<Deck> Update(int id, Deck entity)
    {
        return await _nonQueryDataService.Update(id, entity);
    }
}
