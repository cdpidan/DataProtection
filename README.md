## Data Protection

### CSRedis

#### nuget

```bash
dotnet add package AspNetCore.DataProtection.CSRedis
```

#### inject

```c#
public void ConfigureServices(IServiceCollection services)
{
    var redisConnection = "localhost:6379, password=redis123, defaultDatabase=8";

    services.AddDataProtection()
        .PersistKeysToCSRedis(new CSRedisClient(redisConnection), "IdentityServerCSRedis-DataProtection-Keys");
}
```

#### 引用

[CSRedis](https://github.com/2881099/csredis)

### NewLife.Redis

#### nuget

```bash
dotnet add package AspNetCore.DataProtection.NewLifeRedis
```

#### inject
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddDataProtection()
        .PersistKeysToNewLifeRedis(
            new ProtectionFullRedis("127.0.0.1:6379", "redis123", 9),
            "IdentityServerNewLifeRedis-DataProtection-Keys");
}
```

#### 引用

[NewLife.Redis](https://github.com/NewLifeX/NewLife.Redis)