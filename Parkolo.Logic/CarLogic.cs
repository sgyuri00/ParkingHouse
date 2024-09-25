// <copyright file="CarLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Parkolo.Data;
    using Parkolo.Repository;

    /// <summary>
    /// car logic class.
    /// </summary>
    public class CarLogic : ICarLogic
    {
        public IAutoRepository autoRepo;
        public IParkolasRepository dbRepo;
        public IParkoloRepository parkoloRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarLogic"/> class.
        /// </summary>
        /// <param name="autoRepo">car repository.</param>
        /// <param name="dbRepo">connectios repository.</param>
        /// <param name="parkoloRepo">Parking spot repository.</param>
        public CarLogic(IAutoRepository autoRepo, IParkolasRepository dbRepo, IParkoloRepository parkoloRepo)
        {
            this.autoRepo = autoRepo;
            this.dbRepo = dbRepo;
            this.parkoloRepo = parkoloRepo;
        }

        /// <summary>
        /// To gett all Auto.
        /// </summary>
        /// <returns>Ilist.</returns>
        public IList<Auto> GetAllAuto()
        {
            try
            {
                return this.autoRepo.GetAll().ToList();
            }
            catch (System.InvalidOperationException)
            {
                Thread.Sleep(2000);
                return this.autoRepo.GetAll().ToList();
            }
            
        }

        /// <summary>
        /// To delete a car.
        /// </summary>
        /// <param name="plateNum">license plate number.</param>
        public void DeleteCar(string plateNum)
        {
            var car = this.autoRepo.GetOne(plateNum);
            this.autoRepo.Delete(car);
        }

        /// <summary>
        /// To add a car.
        /// </summary>
        /// <param name="entity">Car entity.</param>
        public void AddCar(Auto entity)
        {
            this.autoRepo.InsertOne(entity);
        }

       /// <summary>
       /// To change fuel type.
       /// </summary>
       /// <param name="plateNum">license plate number.</param>
       /// <param name="fuel">fuel type.</param>
        public void ChangeFuel(string plateNum, string fuel)
        {
            var car = this.autoRepo.GetOne(plateNum);
            this.autoRepo.ChangeFuel(plateNum, fuel);
        }

        /// <summary>
        /// To get one car by license plate number.
        /// </summary>
        /// <param name="id">license palte number.</param>
        /// <returns>An Auto object.</returns>
        public Auto GetByPlateNum(string id)
        {
            return this.autoRepo.GetOne(id);
        }

        /// <summary>
        /// To get all Parkolo data.
        /// </summary>
        /// <returns>Ilist of Parkolo.</returns>
        public IList<ParkoloSpots> GetAllParkolo()
        {
            return this.parkoloRepo.GetAll().ToList();
        }

        /// <summary>
        /// To delete a Parkolo entity.
        /// </summary>
        /// <param name="id">spot number.</param>
        public void DeleteParkolo(int id)
        {
            var spot = this.parkoloRepo.GetOne(id);
            this.parkoloRepo.Delete(spot);
        }

        /// <summary>
        /// To change the size of spot.
        /// </summary>
        /// <param name="id">spot id.</param>
        /// <param name="size">new size of spot.</param>
        public void ChangeSpotSize(int id, int size)
        {
            this.parkoloRepo.ChangeSize(id, size);
        }

        /// <summary>
        /// To add a Parkolo entity.
        /// </summary>
        /// <param name="entity">Parkolo entity.</param>
        public void AddParkolo(ParkoloSpots entity)
        {
            this.parkoloRepo.InsertOne(entity);
        }

        /// <summary>
        /// To get a Parkolo by id.
        /// </summary>
        /// <param name="id">parking spot number.</param>
        /// <returns>A parkolo object.</returns>
        public ParkoloSpots GetBySpotId(int id)
        {
            return this.parkoloRepo.GetOne(id);
        }

        /// <summary>
        /// To get all male people who have a parking.
        /// </summary>
        /// <returns>A list of male people.</returns>
        public ICollection<UsedParkingSpots> UsedSpotsSizeOver13()
        {
            var q3 = from spots in this.parkoloRepo.GetAll()
                     join parking in this.dbRepo.GetAll() on spots.ParkolohelySzam equals parking.ParkolohelySzam
                     where spots.Meret > 13
                     let item = new { Id = spots.ParkolohelySzam, Size = spots.Meret, isElectric = spots.ElektromosE, personName = parking.Szemely.Nev }
                     select new UsedParkingSpots()
                     {
                         Number = item.Id,
                         PersonName = item.personName,
                         Size = item.Size,
                         IsElectric = item.isElectric,
                     };
            return q3.ToArray();
        }

        /// <summary>
        /// Get list of parking fees by vehicles.
        /// </summary>
        /// <returns>list of parking fees and vehicles.</returns>
        public ICollection<SumCost> ParkingFee()
        {
            var q4 = from car in this.autoRepo.GetAll()
                     join parking in this.dbRepo.GetAll() on car.Rendszam equals parking.Rendszam
                     let item = new { Number = car.Rendszam, Parkingfee = parking.Koltseg * parking.EltoltottIdo }
                     group item by item.Number into grp
                     select new SumCost()
                     {
                         LicensePlateNumber = grp.Key,
                         Sum = grp.Sum(item => item.Parkingfee) ?? 0,
                     };
            try
            {
                return q4.ToArray();
            }
            catch (Exception)
            {
                return q4.ToArray();
            }
        }

        /// <summary>
        /// Task logic for used spots over 13.
        /// </summary>
        /// <returns>task.</returns>
        public Task<ICollection<UsedParkingSpots>> UsedSpotsSizeOver13Async()
        {
            return Task.Run(() => this.UsedSpotsSizeOver13());
        }

        /// <summary>
        /// Task logic for parking fee.
        /// </summary>
        /// <returns>task.</returns>
        public Task<ICollection<SumCost>> ParkingFeeAsync()
        {
            return Task.Run(() => this.ParkingFee());
        }

         public ICollection<Over8Hours> Over8Hours()
        {
            var q5 = from cars in this.autoRepo.GetAll()
                     join parking in this.dbRepo.GetAll() on cars.Rendszam equals parking.Rendszam
                     where parking.EltoltottIdo >= 8
                     let item = new { LPN = cars.Rendszam, personId = parking.SzemelyId, fee = parking.EltoltottIdo*parking.Koltseg, time = parking.EltoltottIdo, name = parking.Szemely.Nev }
                     select new Over8Hours()
                     {
                         LicensePlateNum = item.LPN,
                         PersonId = item.personId,
                         Fee = (int)item.fee,
                         Time = (int)item.time,
                         Name = item.name,
                     };
            return q5.ToArray();
        }
    }
}
