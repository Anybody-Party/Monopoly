using System;
using System.Collections;
using UnityEngine;

namespace Coroutines
{
    public sealed class CoroutineObject : CoroutineObjectBase
    {
        public CoroutineObject(MonoBehaviour owner, Func<IEnumerator> routine)
        {
            Owner = owner;
            Routine = routine;
        }

        public Func<IEnumerator> Routine { get; }

        public override event Action Finished;

        private IEnumerator Process()
        {
            yield return Routine.Invoke();

            Coroutine = null;

            Finished?.Invoke();
        }

        public void Start()
        {
            Stop();

            Coroutine = Owner.StartCoroutine(Process());
        }

        public void Stop()
        {
            if (IsProcessing)
            {
                Owner.StopCoroutine(Coroutine);

                Coroutine = null;
            }
        }
    }
}