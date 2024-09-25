// <copyright file="ParkoloRepository.cs" company="PlaceholderCompany">
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
    /// MainRepository for Parkolo.
    /// </summary>
    public class ParkoloRepository : MainRepository<ParkoloSpots>, IParkoloRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParkoloRepository"/> class.
        /// </summary>
        /// <param name="ctx">context.</param>
        public ParkoloRepository(ParkoloContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Changes spot size.
        /// </summary>
        /// <param name="spot">spot number.</param>
        /// <param name="newSize">new size of spot.</param>
        public void ChangeSize(int spot, int newSize)
        {
            var size = this.GetAll().SingleOrDefault(x => x.ParkolohelySzam == spot);
            size.Meret = newSize;
            this.ctx.SaveChanges();
        }

        /// <summary>
        /// To get one entity.
        /// </summary>
        /// <param name="id">id to search by.</param>
        /// <returns>A parkolo entity.</returns>
        public ParkoloSpots GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.ParkolohelySzam == id);
        }
    }
}