using UnityEngine;

namespace KFramework.Common
{
    public class MonoSingletonInterface : MonoBehaviour
    {
        public virtual void Launch() { }
        public virtual void MonoSingletonInterfaceOnInitialize() { }
        public virtual void MonoSingletonInterfaceOnUninitialize() { }
    }

    public class MonoSingletonBase : MonoSingletonInterface
    {
        public event OnSingletonInitializeEventHandler OnInitializeHandler;
        public event OnSingletonUninitializeEventHandler OnUninitializeHandler;

        private bool isInit = false;
        private bool isUninit = false;

        public sealed override void Launch() { }

        public sealed override void MonoSingletonInterfaceOnInitialize()
        {
            if (!isInit)
            {
                isInit = true;
                OnInitialize();
                OnInitializeHandler?.Invoke();
            }
        }

        public sealed override void MonoSingletonInterfaceOnUninitialize()
        {
            if (!isUninit)
            {
                isUninit = true;
                OnUninitializeHandler?.Invoke();
                OnUninitialize();
            }
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnUninitialize() { }
    }
}
