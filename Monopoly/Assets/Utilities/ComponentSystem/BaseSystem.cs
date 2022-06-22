using System;
using UnityEngine;

namespace RH.Utilities.ComponentSystem
{
    public abstract class BaseSystem : IDisposable
    {
        protected BaseSystem() => Init();

        protected abstract void Init();

        public abstract void Dispose();
    }

    public class EntryPoint : MonoBehaviour
    {
            
    }
}