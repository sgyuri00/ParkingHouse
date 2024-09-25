// <copyright file="GlobalSuppressions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: System.CLSCompliant(false)]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Already done>", Scope = "member", Target = "~M:Parkolo.Data.ParkoloContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Already done>", Scope = "member", Target = "~M:Parkolo.Data.ParkoloContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Set need>", Scope = "member", Target = "~P:Parkolo.Data.Szemely.Parkolas")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Set need>", Scope = "member", Target = "~P:Parkolo.Data.Auto.Parkolas")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Set need>", Scope = "member", Target = "~P:Parkolo.Data.ParkoloSpots.Parkolas")]
