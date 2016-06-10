//  -----------------------------------------------------------------------
//  <copyright file="ErrorLog.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:12</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Logs
{
    public class ErrorLog : Log<string>
    {
        public ErrorLog(string errorLog)
            : base(errorLog)
        {
        }

        public ErrorLog(string errorPattern, params object[] values)
            : this(string.Format(errorPattern, values))
        {
        }
    }
}