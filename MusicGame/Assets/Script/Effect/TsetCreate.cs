using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Effect
{
    public class TsetCreate:MonoBehaviour
    {
        [SerializeField] private GameObject effect;
        private void Start()
        {
            Instantiate(effect);
        }

    }
}
