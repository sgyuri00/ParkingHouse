// <copyright file="Auto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace Parkolo.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Auto class.
    /// </summary>
    public partial class Auto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Auto"/> class.
        /// </summary>
        public Auto()
        {
            this.Parkolas = new HashSet<Parkolas>();
        }

        /// <summary>
        /// Gets or sets get and set for license plate number.
        /// </summary>
        public string Rendszam { get; set; }

        /// <summary>
        /// Gets or sets get and set for brand.
        /// </summary>
        public string Marka { get; set; }

        /// <summary>
        /// Gets or sets get and set for year of manufacture.
        /// </summary>
        public int GyartasiEv { get; set; }

        /// <summary>
        /// Gets or sets get and set for fuel type.
        /// </summary>
        public string Uzemanyag { get; set; }

        /// <summary>
        /// Gets or sets collection of Parkolas objects.
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Parkolas> Parkolas { get; set; }
    }
}
