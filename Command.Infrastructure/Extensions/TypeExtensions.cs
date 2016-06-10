//  -----------------------------------------------------------------------
//  <copyright file="TypeExtensions.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Extensions
{
    using System;

    internal static class TypeExtensions
    {
        public static bool IsPrimitive(this Type that)
        {
            return that.IsPrimitive || that == typeof(string) || that == typeof(decimal) || that == typeof(DateTime);
        }
    }
}