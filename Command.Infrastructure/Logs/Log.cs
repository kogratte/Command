﻿//  -----------------------------------------------------------------------
//  <copyright file="Log.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Logs
{
    public abstract class Log<T>
    {
        public T Value { get; private set; }

        protected Log(T value)
        {
            this.Value = value;
        }
    }
}