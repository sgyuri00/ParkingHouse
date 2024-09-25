// <copyright file="Szemely.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace Parkolo.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Szemely class.
    /// </summary>
    public partial class Szemely
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Szemely"/> class.
        /// </summary>
        public Szemely()
        {
            this.Parkolas = new HashSet<Parkolas>();
        }

        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        /// <summary>
        /// Gets or sets for person id.
        /// </summary>
        public int SzemelyId { get; set; }

        /// <summary>
        /// Gets or sets for name.
        /// </summary>
        public string Nev { get; set; }

        /// <summary>
        /// Gets or sets for gender.
        /// </summary>
        public string Nem { get; set; }

        /// <summary>
        /// Gets or sets for date of birth.
        /// </summary>
        public DateTime? SzuletesiIdo { get; set; }

        /// <summary>
        /// Gets or sets collection of Parkolas.
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Parkolas> Parkolas { get; set; }
    }
}
