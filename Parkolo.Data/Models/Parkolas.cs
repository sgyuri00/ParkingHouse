// <copyright file="Parkolas.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace Parkolo.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Parkolas class.
    /// </summary>
    public partial class Parkolas
    {
        /// <summary>
        /// Gets or sets for parkoloid.
        /// </summary>
        public decimal ParkoloId { get; set; }

        /// <summary>
        /// Gets or sets for license plate number.
        /// </summary>
        public string Rendszam { get; set; }

        /// <summary>
        /// Gets or sets for personId.
        /// </summary>
        public int SzemelyId { get; set; }

        /// <summary>
        /// Gets or sets for Parking spot.
        /// </summary>
        public int ParkolohelySzam { get; set; }

        /// <summary>
        /// Gets or sets for cost.
        /// </summary>
        public decimal? Koltseg { get; set; }

        /// <summary>
        /// Gets or sets for time spent.
        /// </summary>
        public decimal? EltoltottIdo { get; set; }

        /// <summary>
        /// Gets or sets to manage parkolohely between tables.
        /// </summary>
        [JsonIgnore]
        public virtual ParkoloSpots ParkolohelySzamNavigation { get; set; }

        /// <summary>
        /// Gets or sets to manage licesne plate number between tables.
        /// </summary>
        [JsonIgnore]
        public virtual Auto RendszamNavigation { get; set; }

        /// <summary>
        /// Gets or sets to manage Szemely between tables.
        /// </summary>
        [JsonIgnore]
        public virtual Szemely Szemely { get; set; }
    }
}
