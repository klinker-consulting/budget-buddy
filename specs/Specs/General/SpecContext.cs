using System;
using System.Collections.Generic;
using System.Linq;

namespace Specs.General
{
    public class SpecContext : IDisposable
    {
        private readonly Dictionary<string, object> _data;

        public SpecContext()
        {
            _data = new Dictionary<string, object>();
        }

        public T Get<T>()
        {
            return _data.ContainsKey(GetKey<T>())
                ? (T) _data[GetKey<T>()]
                : default(T);
        }

        public void Set<T>(T data)
        {
            _data[GetKey<T>()] = data;
        }

        public void Dispose()
        {
            foreach (var disposable in _data.Values.OfType<IDisposable>())
                disposable.Dispose();
        }

        private string GetKey<T>()
        {
            return typeof(T).FullName;
        }
    }
}
