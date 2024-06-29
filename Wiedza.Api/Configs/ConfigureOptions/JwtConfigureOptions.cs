using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Wiedza.Api.Configs.ConfigureOptions;

public class JwtConfigureOptions(JwtConfiguration configuration) : IConfigureNamedOptions<JwtBearerOptions>
{
    public void Configure(JwtBearerOptions options) => Configure(Options.DefaultName, options);

    public void Configure(string? name, JwtBearerOptions options)
    {
        if(name != JwtBearerDefaults.AuthenticationScheme)return;
        options.TokenValidationParameters = configuration.TokenValidationParameters;
    }
}