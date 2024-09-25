// <copyright file="ICarLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Parkolo.Data;

    /// <summary>
    /// Car logic interface.
    /// </summary>
    public interface ICarLogic
    {
        /// <summary>
        /// To get all Auto objects.
        /// </summary>
        /// <returns>A list of cars.</returns>
        IList<Auto> GetAllAuto();
        ParkoloSpots GetBySpotId(int id);

        /// <summary>
        /// To delete an Auto object.
        /// </summary>
        /// <param name="plateNum">license plate number.</param>
        void DeleteCar(string plateNum);
        void ChangeSpotSize(int parkolohelySzam, int meret);

        /// <summary>
        /// Add an Auto object.
        /// </summary>
        /// <param name="entity">An auto object.</param>
        void AddCar(Auto entity);

        /// <summary>
        /// Get all Parkolo objects.
        /// </summary>
        /// <returns>IList of Parkolo entity.</returns>
        IList<ParkoloSpots> GetAllParkolo();

        /// <summary>
        /// Delete a Parkolo object.
        /// </summary>
        /// <param name="id">parking spot.</param>
        void DeleteParkolo(int id);

        /// <summary>
        /// Add a new Parkolo entity.
        /// </summary>
        /// <param name="entity">A Parkolo object.</param>
        void AddParkolo(ParkoloSpots entity);

        Auto GetByPlateNum(string id);

        ICollection<UsedParkingSpots> UsedSpotsSizeOver13();

        ICollection<SumCost> ParkingFee();

        ICollection<Over8Hours> Over8Hours();

        void ChangeFuel(string plateNum, string fuel);
    }
}
