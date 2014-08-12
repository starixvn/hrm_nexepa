// -----------------------------------------------------------------------
// <copyright file="GlobalSuppression.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.8.8">Created.</change>
// </history>

using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "namespace",
    Target = "NexEPA.Core.Services.Interface", MessageId = "Interface", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "namespace",
    Target = "NexEPA.Core.Repositories.Interface", MessageId = "Interface", Justification = "Reviewed. Suppression is OK here.")]
