// <copyright file="SumCost.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Parkolo.Data;
    using Parkolo.Repository;

    /// <summary>
    /// A class for linq.
    /// </summary>
    public class SumCost
    {
        /// <summary>
        /// Gets or sets parking fee.
        /// </summary>
        public decimal? Sum { get; set; }

        /// <summary>
        /// Gets or sets license plate number.
        /// </summary>
        public string LicensePlateNumber { get; set; }

        /// <summary>
        /// Show data on console.
        /// </summary>
        /// <returns>string to show.</returns>
        public override string ToString()
        {
            return $"License plate number = {this.LicensePlateNumber}, Parking fee = {this.Sum}";
        }

        /// <summary>
        /// To compare objects.
        /// </summary>
        /// <param name="obj">SumCost object.</param>
        /// <returns> bool.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SumCost)
            {
                SumCost other = obj as SumCost;
                return this.LicensePlateNumber == other.LicensePlateNumber && this.Sum == other.Sum;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns>integer.</returns>
        public override int GetHashCode()
        {
            return this.LicensePlateNumber.GetHashCode() + (int)this.Sum;
        }
    }
}
