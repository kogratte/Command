//  -----------------------------------------------------------------------
//  <copyright file="Messenger.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Logger
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Logs;

    using Newtonsoft.Json.Linq;

    public delegate void LogHandler(
        string category,
        string hostName,
        string message);

    public class Messenger : IMessenger
    {
        private readonly LogHandler logHandler;

        public bool HasError
        {
            get
            {
                return this.Logs.HasError();
            }
        }

        public Messages Logs { get; }

        public Messenger(LogHandler logHandler)
        {
            this.logHandler = logHandler;
            this.Logs = new Messages();
        }

        public void AddLog<TValue>(Log<TValue> log)
        {
            if (this.AddLog(log as ErrorLog, this.Logs.Error.Add))
            {
                return;
            }

            if (this.AddLog(log as ValidationLog, this.AddValidationLog))
            {
                return;
            }

            if (this.AddLog(log as WarnLog, this.Logs.Warn.Add))
            {
                return;
            }

            this.AddLog(log as SuccessLog, this.Logs.Success.Add);
        }

        public class Messages
        {
            public List<string> Error { get; }

            public List<string> Success { get; }

            public List<JObject> Validation { get; }

            public List<string> Warn { get; set; }

            public bool HasError()
            {
                return this.Error.Any() || this.Validation.Any();
            }

            internal Messages()
            {
                this.Error = new List<string>();
                this.Validation = new List<JObject>();
                this.Success = new List<string>();
                this.Warn = new List<string>();
            }
        }

        private bool AddLog<TValue>(Log<TValue> log, Action<TValue> add)
        {
            if (log == null)
            {
                return false;
            }

            add(log.Value);
            this.logHandler(
                log.GetType().Name.Replace("Log", string.Empty),
                Environment.MachineName,
                log.Value.ToString());

            return true;
        }

        private void AddValidationLog(ValidationResult validationResult)
        {
            string propertyName = validationResult.MemberNames.First();

            var propertyError = this.Logs.Validation.SingleOrDefault(item => item[propertyName] != null);

            if (propertyError == null)
            {
                JObject jsonObject = new JObject(new JProperty(propertyName, new List<string>()));
                this.Logs.Validation.Add(jsonObject);
            }

            propertyError = this.Logs.Validation.SingleOrDefault(item => item[propertyName] != null);

            JArray errors = propertyError[propertyName] as JArray;

            errors.Add(validationResult.ErrorMessage);
        }
    }
}