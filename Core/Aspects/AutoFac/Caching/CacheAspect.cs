using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.AutoFac.Caching
{
    // intercept yazicaz. Yani methodun onunde calisabilecek operasyonlar.
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); // methodun ismi
            var arguments = invocation.Arguments.ToList(); // methodun argumanlari
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; // methodun ismi ve argumanlariyla bir key olusturduk
            if (_cacheManager.IsAdd(key)) // eger cache'de varsa
            {
                invocation.ReturnValue = _cacheManager.Get(key); // methodu calistirmadan cache'den getir
                return;
            }
            invocation.Proceed(); // methodu calistir
            _cacheManager.Add(key, invocation.ReturnValue, _duration); // methodu calistirdiktan sonra cache'e ekle
        }
    }
}
