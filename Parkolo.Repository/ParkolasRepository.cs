// <copyright file="ParkolasRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Parkolo.Data;

    /// <summary>
    /// MainRepository for connections.
    /// </summary>
    public class ParkolasRepository : MainRepository<Parkolas>, IParkolasRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParkolasRepository"/> class.
        /// </summary>
        /// <param name="ctx">context.</param>
        public ParkolasRepository(ParkoloContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Changes spot.
        /// </summary>
        /// <param name="rendszam">license plate numeber.</param>
        /// <param name="newSpot">new spot.</param>
        public void ChangeSpot(string rendszam, int newSpot)
        {
            var parkolas = this.GetOne(rendszam);
            parkolas.ParkolohelySzam = newSpot;
            this.ctx.SaveChanges();
        }

        /// <summary>
        /// To get one Parkolas object.
        /// </summary>
        /// <param name="id">license plate number.</param>
        /// <returns>Parkolas object.</returns>
        public Parkolas GetOne(string id)
        {
            return this.GetAll().SingleOrDefault(x => x.Rendszam.Equals(id));
        }
    }
}