using System;
using Autofac;
using Lykke.Service.Decred.SignService.Core.Services;
using Lykke.Service.Decred.SignService.Services;
using Lykke.SettingsReader;
using NDecred.Common;
using NDecred.Common.Wallet;

namespace Lykke.Service.Decred.SignService.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _settings;

        public ServiceModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterNetwork(builder);

            builder.RegisterType<SigningWallet>().As<ISigningWallet>();
            builder.RegisterType<SecurityService>().As<ISecurityService>();
            builder.RegisterType<SigningService>().As<ISigningService>();
            builder.RegisterType<KeyService>().As<IKeyService>();
        }

        /// <summary>
        /// Reads the network type from settings.
        /// </summary>
        /// <param name="builder"></param>
        private void RegisterNetwork(ContainerBuilder builder)
        {
            var value = _settings.CurrentValue.NetworkType.ToLower();

            switch (value)
            {
                case "main":
                    value = "mainnet";
                    break;
                case "test":
                    value = "testnet";
                    break;
            }

            builder.RegisterInstance(Network.ByName(value)).As<Network>();
        }
    }
}
