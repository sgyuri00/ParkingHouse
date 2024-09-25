// <copyright file="Factory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Program
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Parkolo.Data;
    using Parkolo.Logic;
    using Parkolo.Repository;

    /// <summary>
    /// A factory class.
    /// </summary>
    public class Factory
    {
        private static ParkoloContext ctx = new ParkoloContext();
        private ParkolasRepository dbRepo = new ParkolasRepository(ctx);
        private AutoRepository carRepo = new AutoRepository(ctx);
        private SzemelyRepository personRepo = new SzemelyRepository(ctx);
        private ParkoloRepository parkingRepo = new ParkoloRepository(ctx);

        /// <summary>
        /// Create parkolas logic.
        /// </summary>
        /// <returns>ParkolasLogic object.</returns>
        public ParkolasLogic CreateParkolasLogic()
        {
            ParkolasLogic parkolasLogic = new ParkolasLogic(this.dbRepo, this.personRepo);
            return parkolasLogic;
        }

        /// <summary>
        /// Create ca logic.
        /// </summary>
        /// <returns>CarLogic object.</returns>
        public CarLogic CreateCarLogic()
        {
            CarLogic carLogic = new CarLogic(this.carRepo, this.dbRepo, this.parkingRepo);
            return carLogic;
        }
    }
}
