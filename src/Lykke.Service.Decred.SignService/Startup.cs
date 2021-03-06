﻿using System;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Service.Decred.SignService.Core.Services;
using Lykke.Service.Decred.SignService.Services;
using Lykke.SettingsReader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NDecred.Common;
using NDecred.Common.Wallet;

namespace Lykke.Service.Decred.SignService
{
    [UsedImplicitly]
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var service = services.BuildServiceProvider<AppSettings>(options =>
            {
                options.Logs = logs => logs.UseEmptyLogging();

                options.Swagger = swagger =>
                {
                    swagger.DescribeAllEnumsAsStrings();
                    swagger.DescribeStringEnumsInCamelCase();
                };

                options.SwaggerOptions = new LykkeSwaggerOptions
                {
                    ApiTitle = "Decred.SignService"
                };
            });

            return service;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration();
        }
    }
}
