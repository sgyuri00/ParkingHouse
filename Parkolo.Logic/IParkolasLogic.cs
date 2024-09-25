// <copyright file="IParkolasLogic.cs" company="PlaceholderCompany">
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
    /// IParkolasLogic to get data.
    /// </summary>
    public interface IParkolasLogic
    {
        /// <summary>
        /// Gives the parking spot by license plate number.
        /// </summary>
        /// /// <param name="rendszam">license plate number.</param>
        /// <returns>Parking data.</returns>
        Parkolas GetParkolasByRendszam(string rendszam);
        void AddParkolas(Parkolas value);
        void AddPerson(int szemelyId, string nev, string nem, DateTime? szuletesiIdo);
        void DeletePerson(int id);

        /// <summary>
        /// Changes the parking spot by license plate number.
        /// </summary>
        /// /// /// <param name="rendszam">license plate number.</param>
        /// /// /// <param name="newSpot">new spot where tha car parking.</param>
        void ChangeParkolasSpot(string rendszam, int newSpot);
        void DeleteParking(string rendszam);

        /// <summary>
        /// Get all parking data.
        /// </summary>
        /// <returns>All parking data in list.</returns>
        IList<Parkolas> GetAllParkolas();

        /// <summary>
        /// To get all Szemely.
        /// </summary>
        /// <returns>IList of people.</returns>
        IList<Szemely> GetAllSzemely();

        /// <summary>
        /// Changes the name of a person.
        /// </summary>
        /// <param name="id">person id.</param>
        /// <param name="newName">new name.</param>
        void ChangeName(int id, string newName);

        /// <summary>
        /// List of people who paid more than 2000.
        /// </summary>
        /// <returns> a PaidMoreThan2000 List.</returns>
        ICollection<PaidMoreThan2000> PeoplePaidMoreThan2000();

        ICollection<MalePeople> MalePeopleHaveParking();

        Szemely GetById(int id);
    }
}
