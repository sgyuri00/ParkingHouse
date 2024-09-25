// <copyright file="IAutoRepository.cs" company="PlaceholderCompany">
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
    public interface IAutoRepository : IRepository<Auto>
    {
        /// <summary>
        /// Changes fueal type for car.
        /// </summary>
        /// <param name="plateNum">license plate number.</param>
        /// <param name="fuel">fuel type.</param>
        void ChangeFuel(string plateNum, string fuel);

        /// <summary>
        /// Gets an object which matches the id=key.
        /// </summary>
        /// <param name="id">key.</param>
        /// <returns>Auto object.</returns>
        Auto GetOne(string id);

        void Add(string licensePlate, string brand, int year, string fuel);
    }
}
