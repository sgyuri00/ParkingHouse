// <copyright file="MainRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Parkolo.Data;

    /// <summary>
    /// Main repository.
    /// </summary>
    /// <typeparam name="T">T is entity.</typeparam>
    public abstract class MainRepository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Context to work with.
        /// </summary>
         protected DbContext ctx;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainRepository{T}"/> class.
        /// </summary>
        /// <param name="ctx">context.</param>
         public MainRepository(DbContext ctx)
        {
            this.ctx = ctx;
        }

        /// <summary>
        /// To get all data from the tables.
        /// </summary>
        /// <returns>Every data.</returns>
         public IQueryable<T> GetAll()
        {
            return this.ctx.Set<T>();
        }

        /// <summary>
        /// Add new data.
        /// </summary>
        /// <param name="entity">Like tables.</param>
         public void InsertOne(T entity)
        {
            this.ctx.Set<T>().Add(entity);
            this.ctx.SaveChanges();
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity">An entity to delete.</param>
         public void Delete(T entity)
        {
            this.ctx.Set<T>().Remove(entity);
            this.ctx.SaveChanges();
        }
    }
}