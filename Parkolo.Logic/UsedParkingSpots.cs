// <copyright file="UsedParkingSpots.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Logic
{
    /// <summary>
    /// Class to help in linq.
    /// </summary>
    public class UsedParkingSpots
    {
        /// <summary>
        /// Gets or sets for number.
        /// </summary>
        public decimal Number { get; set; }

        /// <summary>
        /// Gets or sets for size.
        /// </summary>
        public decimal? Size { get; set; }

        /// <summary>
        /// Gets or sets for is electric spot.
        /// </summary>
        public string IsElectric { get; set; }

        /// <summary>
        /// Gets or sets for person name.
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// To write on console.
        /// </summary>
        /// <returns>string.</returns>
        public override string ToString()
        {
            return $"parking spot number = {this.Number}, Size = {this.Size}, IsElectric = {this.IsElectric}, Person name = {this.PersonName}";
        }

        /// <summary>
        /// To compare objects.
        /// </summary>
        /// <param name="obj">UsedParkingSpots object.</param>
        /// <returns> bool.</returns>
        public override bool Equals(object obj)
        {
            if (obj is UsedParkingSpots)
            {
                UsedParkingSpots other = obj as UsedParkingSpots;
                return this.Number == other.Number && this.Size == other.Size && this.IsElectric == other.IsElectric;
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
            return this.IsElectric.GetHashCode() + (int)this.Size + (int)this.Number;
        }
    }
}
