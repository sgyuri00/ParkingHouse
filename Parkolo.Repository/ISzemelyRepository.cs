// <copyright file="ISzemelyRepository.cs" company="PlaceholderCompany">
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
    /// Entity interface.
    /// </summary>
    public interface ISzemelyRepository : IRepository<Szemely>
    {
        /// <summary>
        /// Changes persons name.
        /// </summary>
        /// <param name="id">Person id.</param>
        /// <param name="newName">New Name.</param>
        void ChangeName(int id, string newName);

        /// <summary>
        /// Szemely object that has id as a key.
        /// </summary>
        /// <param name="id">key to find.</param>
        /// <returns>Szemely object.</returns>
        Szemely GetOne(int id);
    }
}
