using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); // key 'e karsilik gelen veriyi T tipinde getirir
        object Get(string key); // key 'e karsilik gelen veriyi object tipinde getirir
        void Add(string key, object value, int duration); // key 'e karsilik gelen veriyi ekler, duration saniye cinsindendir
        bool IsAdd(string key); // key 'in cache de olup olmadigini kontrol eder
        void Remove(string key); // key 'e karsilik gelen veriyi cache den siler
        void RemoveByPattern(string pattern); // pattern 'e uyan tum key'leri cache den siler
    }
}
