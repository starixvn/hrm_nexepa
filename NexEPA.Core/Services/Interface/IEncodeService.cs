// -----------------------------------------------------------------------
// <copyright file="IEncodeService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.08.05">Created</change>
// </history>

namespace NexEPA.Core.Services.Interface
{
    /// <summary>
    /// The interface of encode service.
    /// </summary>
    public interface IEncodeService
    {
        string Encode(string plainText);

        string Decode(string cipherText);
    }
}
