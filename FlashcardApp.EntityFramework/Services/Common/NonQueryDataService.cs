﻿using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashcardApp.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlashcardApp.EntityFramework.Services.Common;

/// <summary>
/// Implements IDataService interface methods except getter methods
/// </summary>
public class NonQueryDataService<T>
    where T : DomainObject
{
    private readonly FlashcardAppDbContextFactory _contextFactory;

    public NonQueryDataService(FlashcardAppDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<T> Create(T entity)
    {
        using (FlashcardAppDbContext context = _contextFactory.CreateDbContext())
        {
            EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return createdResult.Entity;
        }
    }

    public async Task<T> Update(int id, T entity)
    {
        using (FlashcardAppDbContext context = _contextFactory.CreateDbContext())
        {
            entity.Id = id;

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }
    }

    public async Task<bool> Delete(int id)
    {
        using (FlashcardAppDbContext context = _contextFactory.CreateDbContext())
        {
            T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            if (entity is null)
            {
                return false;
            }

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
