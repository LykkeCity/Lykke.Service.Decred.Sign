using System;
using System.IO;
using System.Threading.Tasks;
using Lykke.Sdk;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

namespace Lykke.Service.Decred.SignService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
#if DEBUG
            await LykkeStarter.Start<Startup>(true);
#else
            await LykkeStarter.Start<Startup>(false);
#endif
        }
    }
}
