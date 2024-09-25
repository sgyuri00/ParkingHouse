// <copyright file="MalePeople.cs" company="PlaceholderCompany">
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
    /// Help class to show data on console.
    /// </summary>
    public class MalePeople
    {
        /// <summary>
        /// Gets or sets person name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets person gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets person date of birth.
        /// </summary>
        public string DateofBirth { get; set; }

        /// <summary>
        /// Show data on console.
        /// </summary>
        /// <returns>string to show on console.</returns>
        public override string ToString()
        {
            return $"Name = {this.Name}, Gender = {this.Gender}, Date of birth = {this.DateofBirth}";
        }

        /// <summary>
        /// Compare objects.
        /// </summary>
        /// <param name="obj">MalePeople object.</param>
        /// <returns>bool.</returns>
        public override bool Equals(object obj)
        {
            if (obj is MalePeople)
            {
                MalePeople other = obj as MalePeople;
                return this.Name == other.Name && this.Gender == other.Gender /*&& this.DateofBirth == other.DateofBirth*/;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Het hash code.
        /// </summary>
        /// <returns>integer.</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.Gender.GetHashCode();
        }
    }
}
