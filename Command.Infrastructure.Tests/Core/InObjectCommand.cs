//  -----------------------------------------------------------------------
//  <copyright file="InObjectCommand.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core
{
    using System;

    using Infrastructure.Core;
    using Infrastructure.Logger;

    public class InObjectCommand : CommandIn<Uri>
    {
        public InObjectCommand(IMessenger messenger)
            : base(messenger)
        {
        }

        protected override void OnExecute(Uri input)
        {
        }
    }
}