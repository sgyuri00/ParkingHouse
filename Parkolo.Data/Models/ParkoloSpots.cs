// <copyright file="ParkoloSpots.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace Parkolo.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Class Parkolo.
    /// </summary>
    public partial class ParkoloSpots
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParkoloSpots"/> class.
        /// Parkolo contructor.
        /// </summary>
        public ParkoloSpots()
        {
            this.Parkolas = new HashSet<Parkolas>();
        }

        /// <summary>
        /// Gets or sets for Parking spot number.
        /// </summary>
        public int ParkolohelySzam { get; set; }

        /// <summary>
        /// Gets or sets for size.
        /// </summary>
        public int Meret { get; set; }

        /// <summary>
        /// Gets or sets  si electric.
        /// </summary>
        public string ElektromosE { get; set; }

        /// <summary>
        /// Gets or sets collection of Parkolas objects.
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Parkolas> Parkolas { get; set; }

        /// <summary>
        /// Tostring for Tests.
        /// </summary>
        /// <returns>data in string.</returns>
        public override string ToString()
        {
            return "ParkolohelySzam = " + this.ParkolohelySzam + "Meret = " + this.Meret + "ElektromosE = " + this.ElektromosE;
        }
    }
}
