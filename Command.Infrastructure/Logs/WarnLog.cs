//  -----------------------------------------------------------------------
//  <copyright file="WarnLog.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Logs
{
    public class WarnLog : Log<string>
    {
        public WarnLog(string warnMessage)
            : base(warnMessage)
        {
        }
    }
}