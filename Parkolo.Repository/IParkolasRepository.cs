// <copyright file="IParkolasRepository.cs" company="PlaceholderCompany">
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
    public interface IParkolasRepository : IRepository<Parkolas>
    {
        /// <summary>
        /// Changes parking spot for car.
        /// </summary>
        /// <param name="rendszam">Cars license plate number to find.</param>
        /// <param name="newSpot">New parking spot number.</param>
        void ChangeSpot(string rendszam, int newSpot);

        /// <summary>
        /// Gets a Parkolas object which has id=key.
        /// </summary>
        /// <param name="id">key.</param>
        /// <returns>Parkolas object.</returns>
        Parkolas GetOne(string id);
    }
}
