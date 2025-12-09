using UnityEngine;
using System.Collections;

namespace Effect
{
    public class EffectContllore : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem ParticleSystem;
        void OnEnable()
        {
            StartCoroutine(ParticleWorking());
        }


        IEnumerator ParticleWorking()
        {
            yield return new WaitWhile(() => ParticleSystem.IsAlive(true));
            GameObject.Destroy(this.gameObject);
        }
    }
}

