// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Parkolo.Data;

    /// <summary>
    /// Main repository.
    /// </summary>
    /// <typeparam name="T"> T means class.</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Get all object from the database.
        /// </summary>
        /// <returns>Queryable list.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// To insert an entity.
        /// </summary>
        /// <param name="entity">class.</param>
        void InsertOne(T entity);

        /// <summary>
        /// To delete an entity.
        /// </summary>
        /// <param name="entity">Class.</param>
        void Delete(T entity);
    }
}
