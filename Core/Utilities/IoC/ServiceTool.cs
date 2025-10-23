using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{

    //IoC : Inversion of Control Yani Dependency Injection'da degisimin kontrolu
    public static class ServiceTool // uygulamanin service providerina erismek icin
    {
        public static IServiceProvider ServiceProvider { get; set; }// merkezi servis yonetimi nesnesi
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        } // bu yapiyla service tool ile .net core servislerine erisebiliriz
    }
}
