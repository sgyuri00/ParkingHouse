// <copyright file="GlobalSuppressions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: System.CLSCompliant(false)]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Not important>", Scope = "member", Target = "~F:Parkolo.Repository.MainRepository`1.ctx")]
[assembly: SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "<Not important>", Scope = "member", Target = "~F:Parkolo.Repository.MainRepository`1.ctx")]
[assembly: SuppressMessage("Globalization", "CA1307:Specify StringComparison for clarity", Justification = "<Not needed>", Scope = "member", Target = "~M:Parkolo.Repository.ParkolasRepository.GetOne(System.String)~Parkolo.Data.Parkolas")]
[assembly: SuppressMessage("Globalization", "CA1309:Use ordinal string comparison", Justification = "<Not needed>", Scope = "member", Target = "~M:Parkolo.Repository.ParkolasRepository.GetOne(System.String)~Parkolo.Data.Parkolas")]
[assembly: SuppressMessage("Design", "CA1012:Abstract types should not have public constructors", Justification = "<Public ctor need>", Scope = "type", Target = "~T:Parkolo.Repository.MainRepository`1")]
