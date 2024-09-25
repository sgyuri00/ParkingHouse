// <copyright file="SzemelyRepository.cs" company="PlaceholderCompany">
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
    /// MainRepository for People.
    /// </summary>
    public class SzemelyRepository : MainRepository<Szemely>, ISzemelyRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SzemelyRepository"/> class.
        /// </summary>
        /// <param name="ctx">context.</param>
        public SzemelyRepository(ParkoloContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Changes name for person by id.
        /// </summary>
        /// <param name="id">person's id.</param>
        /// <param name="newName">person's new name.</param>
        public void ChangeName(int id, string newName)
        {
            var person = this.GetAll().SingleOrDefault(x => x.SzemelyId == id);
            person.Nev = newName;
            this.ctx.SaveChanges();
        }

        /// <summary>
        /// To get one entity.
        /// </summary>
        /// <param name="id">id to search by.</param>
        /// <returns>A Szemely object.</returns>
        public Szemely GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.SzemelyId == id);
        }
    }
}