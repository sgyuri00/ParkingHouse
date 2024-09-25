// <copyright file="AutoRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Parkolo.Data;

    /// <summary>
    /// MainRepository for Auto.
    /// </summary>
    public class AutoRepository : MainRepository<Auto>, IAutoRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoRepository"/> class.
        /// </summary>
        /// <param name="ctx">context.</param>
        public AutoRepository(ParkoloContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Change fuel type.
        /// </summary>
        /// <param name="plateNum">license plate number.</param>
        /// <param name="fuel">fuel type.</param>
        public void ChangeFuel(string plateNum, string fuel)
        {
            var auto = this.GetOne(plateNum);
            auto.Uzemanyag = fuel;
            this.ctx.SaveChanges();
        }

        /// <summary>
        /// To get one Auto object.
        /// </summary>
        /// <param name="id">license plate number.</param>
        /// <returns>An Auto object.</returns>
        public Auto GetOne(string id)
        {
            try
            {
                return this.GetAll().SingleOrDefault(x => x.Rendszam == id);
            }
            catch (Exception)
            {
                Thread.Sleep(2000);
                return this.GetAll().SingleOrDefault(x => x.Rendszam == id);
               
            }
            
        }

        /// <summary>
        /// To add a new entity.
        /// </summary>
        /// <param name="licensePlate">license plate number.</param>
        /// <param name="brand">brand name.</param>
        /// <param name="year">year of manufacture.</param>
        /// <param name="fuel">fuel type.</param>
        public void Add(string licensePlate, string brand, int year, string fuel)
        {
            Auto auto = new Auto();
            auto.Rendszam = licensePlate;
            auto.Marka = brand;
            auto.GyartasiEv = year;
            auto.Uzemanyag = fuel;

            this.ctx.Set<Auto>().Add(auto);
            this.ctx.SaveChanges();
        }

        /// <summary>
        /// To delete an entity.
        /// </summary>
        /// <param name="licensePlate">license plate number.</param>
        public void Delete(string licensePlate)
        {
            var q = this.GetOne(licensePlate);
            this.ctx.Set<Auto>().Remove(q);
            this.ctx.SaveChanges();
        }
    }
}