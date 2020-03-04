// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.NewLifeRedis;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Caching;

namespace Microsoft.AspNetCore.DataProtection
{
    /// <summary>
    /// Contains Redis-specific extension methods for modifying a <see cref="IDataProtectionBuilder"/>.
    /// </summary>
    public static class NewLifeRedisDataProtectionBuilderExtensions
    {
        private const string DataProtectionKeysName = "DataProtection-Keys";

        /// <summary>
        /// Configures the data protection system to persist keys to specified key in Redis database
        /// </summary>
        /// <param name="builder">The builder instance to modify.</param>
        /// <param name="redisFactory">The delegate used to create <see cref="CSRedisClient"/> instances.</param>
        /// <param name="key">The used to store key list.</param>
        /// <returns>A reference to the <see cref="IDataProtectionBuilder" /> after this operation has completed.</returns>
        public static IDataProtectionBuilder PersistKeysToNewLifeRedis(this IDataProtectionBuilder builder,
            Func<ProtectionFullRedis> redisFactory, string key)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (redisFactory == null)
            {
                throw new ArgumentNullException(nameof(redisFactory));
            }

            return PersistKeysToNewLifeRedisInternal(builder, redisFactory, key);
        }

        /// <summary>
        /// Configures the data protection system to persist keys to the default key ('DataProtection-Keys') in Redis database
        /// </summary>
        /// <param name="builder">The builder instance to modify.</param>
        /// <param name="redisClient">The <see cref="CSRedisClient"/> for database access.</param>
        /// <returns>A reference to the <see cref="IDataProtectionBuilder" /> after this operation has completed.</returns>
        public static IDataProtectionBuilder PersistKeysToNewLifeRedis(this IDataProtectionBuilder builder,
            ProtectionFullRedis redisClient)
        {
            return PersistKeysToNewLifeRedis(builder, redisClient, DataProtectionKeysName);
        }

        /// <summary>
        /// Configures the data protection system to persist keys to the specified key in Redis database
        /// </summary>
        /// <param name="builder">The builder instance to modify.</param>
        /// <param name="redisClient">The <see cref="CSRedisClient"/> for database access.</param>
        /// <param name="key">The used to store key list.</param>
        /// <returns>A reference to the <see cref="IDataProtectionBuilder" /> after this operation has completed.</returns>
        public static IDataProtectionBuilder PersistKeysToNewLifeRedis(this IDataProtectionBuilder builder,
            ProtectionFullRedis redisClient, string key)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (redisClient == null)
            {
                throw new ArgumentNullException(nameof(redisClient));
            }

            return PersistKeysToNewLifeRedisInternal(builder, () => redisClient, key);
        }

        private static IDataProtectionBuilder PersistKeysToNewLifeRedisInternal(IDataProtectionBuilder builder,
            Func<ProtectionFullRedis> redisClient, string key)
        {
            builder.Services.Configure<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new RedisXmlRepository(redisClient, key);
            });
            return builder;
        }
    }
}