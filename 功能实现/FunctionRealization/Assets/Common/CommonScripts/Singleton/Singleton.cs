
using UnityEngine;

namespace KFramework.Common
{
    public class Singleton<T> : SingletonBase where T : class, new()
    {
        private static T instance = null;
        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (_lock)
                    {
                        instance = new T(); //调用构造函数;
                        if (instance is SingletonInterface Instance)
                        {
                            Instance.SingletonInterfaceOnInitialize();
                        }
                    }
                }
                return instance;
            }
        }

        /// 构造函数;
        protected Singleton()
        {
            if (null == instance)
            {
                Debug.Log($"[Singleton]{typeof(T)} Instance instance created.");
            }
        }
    }
}
