// <copyright file="PaidMoreThan2000.cs" company="PlaceholderCompany">
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
    /// Help class for linq.
    /// </summary>
    public class PaidMoreThan2000
    {
        /// <summary>
        /// Gets or sets person name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets parking fee.
        /// </summary>
        public decimal? ParkingFee { get; set; }

        /// <summary>
        /// Gets or sets license plate number.
        /// </summary>
        public string LicensePlateNumber { get; set; }

        /// <summary>
        /// Show data on console.
        /// </summary>
        /// <returns>string of data.</returns>
        public override string ToString()
        {
            return $"Name = {this.Name}, Parking fee = {this.ParkingFee} HUF, License plate number = {this.LicensePlateNumber}";
        }

        /// <summary>
        /// Compare objects.
        /// </summary>
        /// <param name="obj">PaidMoreThan2000 object.</param>
        /// <returns>bool.</returns>
        public override bool Equals(object obj)
        {
            if (obj is PaidMoreThan2000)
            {
                PaidMoreThan2000 other = obj as PaidMoreThan2000;
                return this.Name == other.Name && this.ParkingFee == other.ParkingFee && this.LicensePlateNumber == other.LicensePlateNumber;
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
            return this.Name.GetHashCode() + this.LicensePlateNumber.GetHashCode();
        }
    }
}
