// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CSRedis;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Microsoft.AspNetCore.DataProtection.CSRedis
{
    /// <summary>
    /// An XML repository backed by a Redis list entry.
    /// </summary>
    public class RedisXmlRepository : IXmlRepository
    {
        private readonly Func<CSRedisClient> _databaseFactory;
        private readonly string _key;

        /// <summary>
        /// Creates a <see cref="RedisXmlRepository"/> with keys stored at the given directory.
        /// </summary>
        /// <param name="databaseFactory">The delegate used to create <see cref="CSRedisClient"/> instances.</param>
        /// <param name="key">The used to store key list.</param>
        public RedisXmlRepository(Func<CSRedisClient> databaseFactory, string key)
        {
            _databaseFactory = databaseFactory;
            _key = key;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<XElement> GetAllElements()
        {
            return GetAllElementsCore().ToList().AsReadOnly();
        }

        private IEnumerable<XElement> GetAllElementsCore()
        {
            // Note: Inability to read any value is considered a fatal error (since the file may contain
            // revocation information), and we'll fail the entire operation rather than return a partial
            // set of elements. If a value contains well-formed XML but its contents are meaningless, we
            // won't fail that operation here. The caller is responsible for failing as appropriate given
            // that scenario.
            var database = _databaseFactory();
            foreach (var value in database.LRange(_key,0L,-1L))
            {
                yield return XElement.Parse(value);
            }
        }

        /// <inheritdoc />
        public void StoreElement(XElement element, string friendlyName)
        {
            var database = _databaseFactory();
            database.LPush(_key, element.ToString(SaveOptions.DisableFormatting));
        }
    }
}