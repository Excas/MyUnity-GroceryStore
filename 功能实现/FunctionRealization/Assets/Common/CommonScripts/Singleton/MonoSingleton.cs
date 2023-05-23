using UnityEngine;

namespace KFramework.Common
{
    public class MonoSingleton<T> : MonoSingletonBase where T : MonoBehaviour
    {
        private static readonly string _monoSingletonRoot = "@GameRoot";

        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (null == _instance)
                {
                    var go = GameObject.Find(_monoSingletonRoot);
                    if (go)
                    {
                        _instance = go.AddComponent<T>();
                        var singleton = _instance as MonoSingletonInterface;
                        if (singleton != null)
                        {
                            /// OnInitialize晚于AwakeEx执行;
                            singleton.MonoSingletonInterfaceOnInitialize();
                        }
                    }
                }
                return _instance;
            }
        }

        public static bool ApplicationIsPlaying
        {
            get
            {
                return _instance != null;
            }
        }

        /// 构造函数;
        protected MonoSingleton()
        {
            if (null == _instance)
            {
                Debug.Log($"[MonoSingleton]{typeof(T)} singleton instance created.");
            }
        }

        void Awake()
        {
            AwakeEx();
        }

        void Start()
        {
            StartEx();
        }

        void OnEnable()
        {
            OnEnableEx();
        }

        void FixedUpdate()
        {
            FixedUpdateEx(Time.deltaTime);
        }

        void Update()
        {
            UpdateEx(Time.deltaTime);
        }

        void LateUpdate()
        {
            LateUpdateEx(Time.deltaTime);
        }

        void OnDisable()
        {
            OnDisableEx();
        }

        /// MonoSingleton只有在ApplicationQuit时才会Destroy;
        void OnApplicationQuit()
        {
            var singleton = _instance as MonoSingletonInterface;
            if (singleton != null)
            {
                singleton.MonoSingletonInterfaceOnUninitialize();
            }
            OnDestroyEx();
            _instance = null;
        }

        protected virtual void AwakeEx() { }
        protected virtual void StartEx() { }
        protected virtual void OnEnableEx() { }
        protected virtual void FixedUpdateEx(float interval) { }
        protected virtual void UpdateEx(float interval) { }
        protected virtual void LateUpdateEx(float interval) { }
        protected virtual void OnDisableEx() { }
        protected virtual void OnDestroyEx() { }
    }
}
