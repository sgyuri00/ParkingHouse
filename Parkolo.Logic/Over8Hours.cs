// <copyright file="UsedParkingSpots.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Logic
{
    /// <summary>
    /// Class to help in linq.
    /// </summary>
    public class Over8Hours
    {
        public string LicensePlateNum { get; set; }

        public int PersonId { get; set; }

        public int Fee { get; set; }

        public int Time { get; set; }

        public string Name { get; set; }

        //public override string ToString()
        //{
        //    return $"parking spot number = {this.Number}, Size = {this.Size}, IsElectric = {this.IsElectric}, Person name = {this.PersonName}";
        //}

        public override bool Equals(object obj)
        {
            if (obj is Over8Hours)
            {
                Over8Hours other = obj as Over8Hours;
                return this.LicensePlateNum == other.LicensePlateNum && this.PersonId == other.PersonId && this.Fee == other.Fee && this.Time == other.Time && this.Name == other.Name;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.LicensePlateNum.GetHashCode() + (int)this.Fee + (int)this.PersonId + (int)this.Time + this.Name.GetHashCode();
        }
    }
}
