// <copyright file="ParkolasLogic.cs" company="PlaceholderCompany">
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
    using Parkolo.Repository;

    /// <summary>
    /// Parkolas logic class.
    /// </summary>
    public class ParkolasLogic : IParkolasLogic
    {
        private IParkolasRepository dbRepo;
        private ISzemelyRepository personRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkolasLogic"/> class.
        /// </summary>
        /// <param name="repo">IParkolasRepository.</param>
        /// <param name="perrepo">ISzemelyRepository.</param>
        public ParkolasLogic(IParkolasRepository repo, ISzemelyRepository perrepo)
        {
            this.dbRepo = repo;
            this.personRepo = perrepo;
        }

        /// <summary>
        /// Changes parking spot fo vehicles.
        /// </summary>
        /// <param name="rendszam">license palte number.</param>
        /// <param name="newSpot">new parking spot.</param>
        public void ChangeParkolasSpot(string rendszam, int newSpot)
        {
            this.dbRepo.ChangeSpot(rendszam, newSpot);
        }

        /// <summary>
        /// Get IList of Parkolas entites.
        /// </summary>
        /// <returns>IList of Parkolas entites.</returns>
        public IList<Parkolas> GetAllParkolas()
        {
            return this.dbRepo.GetAll().ToList();
        }

        /// <summary>
        /// Get parking data ny license plate number.
        /// </summary>
        /// <param name="rendszam">license plate number.</param>
        /// <returns>Parkolas entity.</returns>
        public Parkolas GetParkolasByRendszam(string rendszam)
        {
            return this.dbRepo.GetOne(rendszam);
        }

        /// <summary>
        /// Add new Parkolas entity.
        /// </summary>
        /// <param name="entity">Parkolas entity.</param>
        public void AddParkolas(Parkolas entity)
        {
            this.dbRepo.InsertOne(entity);
        }

        /// <summary>
        /// Delete a Parking data.
        /// </summary>
        /// <param name="id">parking data id.</param>
        public void DeleteParking(string id)
        {
            var parking = this.dbRepo.GetOne(id);
            this.dbRepo.Delete(parking);
        }

        /// <summary>
        /// Delete person entity.
        /// </summary>
        /// <param name="id">person id.</param>
        public void DeletePerson(int id)
        {
            var person = this.personRepo.GetOne(id);
            this.personRepo.Delete(person);
        }

        /// <summary>
        /// Add a person entity.
        /// </summary>
        /// <param name="id">person id.</param>
        /// <param name="name">person name.</param>
        /// <param name="gender">person gender.</param>
        /// <param name="dateodBirth">person date of birth.</param>
        public void AddPerson(int id, string name, string gender, DateTime? dateodBirth)
        {
            var person = new Szemely()
            {
                SzemelyId = id,
                Nev = name,
                Nem = gender,
                SzuletesiIdo = dateodBirth,
            };
            this.personRepo.InsertOne(person);
        }

        /// <summary>
        /// Get person by id.
        /// </summary>
        /// <param name="id">pereson id.</param>
        /// <returns>A szemely entity.</returns>
        public Szemely GetById(int id)
        {
            return this.personRepo.GetOne(id);
        }

        /// <summary>
        /// Changes name of person.
        /// </summary>
        /// <param name="id">person id.</param>
        /// <param name="newName">new name of person.</param>
        public void ChangeName(int id, string newName)
        {
            this.personRepo.ChangeName(id, newName);
        }

        /// <summary>
        /// Get all Szemely entities.
        /// </summary>
        /// <returns>IList of Szemely.</returns>
        public IList<Szemely> GetAllSzemely()
        {
            return this.personRepo.GetAll().ToList();
        }

        /// <summary>
        /// Get people who paid more than 2000.
        /// </summary>
        /// <returns>List of people who paid more than 2000.</returns>
        public ICollection<PaidMoreThan2000> PeoplePaidMoreThan2000()
        {
            var q1 = from person in this.personRepo.GetAll()
                     join parking in this.dbRepo.GetAll() on person.SzemelyId equals parking.SzemelyId
                     where parking.Koltseg * parking.EltoltottIdo > 2000
                     let item = new { PName = person.Nev, PParkingfee = parking.Koltseg * parking.EltoltottIdo, PlateNum = parking.Rendszam }
                     select new PaidMoreThan2000()
                     {
                         Name = item.PName,
                         ParkingFee = item.PParkingfee,
                         LicensePlateNumber = item.PlateNum,
                     };
            return q1.ToArray();
        }

        /// <summary>
        /// Get male people who have parking.
        /// </summary>
        /// <returns>List of people who have parking data.</returns>
        public ICollection<MalePeople> MalePeopleHaveParking()
        {
            var q2 = from person in this.personRepo.GetAll()
                     join parking in this.dbRepo.GetAll() on person.SzemelyId equals parking.SzemelyId
                     where person.Nem == "Ferfi"
                     let item = new { PName = person.Nev, Gender = person.Nem, Birth = ((DateTime)person.SzuletesiIdo).ToString("yyyy-MM-dd") }
                     select new MalePeople()
                     {
                         Name = item.PName,
                         Gender = item.Gender,
                         DateofBirth = item.Birth,
                     };
            return q2.ToArray();
        }

        /// <summary>
        /// Task logic for People paid more than 2000.
        /// </summary>
        /// <returns>task.</returns>
        public Task<ICollection<PaidMoreThan2000>> PeoplePaidMoreThan2000Async()
        {
            return Task.Run(() => this.PeoplePaidMoreThan2000());
        }

        /// <summary>
        /// Task logic for male people.
        /// </summary>
        /// <returns>Task.</returns>
        public Task<ICollection<MalePeople>> MalePeopleHaveParkingAsync()
        {
            return Task.Run(() => this.MalePeopleHaveParking());
        }
    }
}
