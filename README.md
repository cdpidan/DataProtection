## Data Protection

### CSRedis

#### nuget

```
dotnet add package AspNetCore.DataProtection.CSRedis
```

#### 依赖注入

```
var redisConnection = "127.0.0.1:6379,password=123";
services.AddDataProtection()
    .PersistKeysToCSRedis(new CSRedis.CSRedisClient(redisConnection), "IdentityServerCSRedis-DataProtection-Keys");
```

#### 引用

[CSRedis](https://github.com/2881099/csredis)