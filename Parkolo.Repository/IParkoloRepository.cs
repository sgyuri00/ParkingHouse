// <copyright file="IParkoloRepository.cs" company="PlaceholderCompany">
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
    public interface IParkoloRepository : IRepository<ParkoloSpots>
    {
        /// <summary>
        /// Changes the spot size.
        /// </summary>
        /// <param name="spot">The spot id.</param>
        /// <param name="newSize">New size of the spot.</param>
        void ChangeSize(int spot, int newSize);

        /// <summary>
        /// Search for a Parkol.
        /// </summary>
        /// <param name="id">key.</param>
        /// <returns>Parkolo object which matches the condition.</returns>
        ParkoloSpots GetOne(int id);
    }
}
