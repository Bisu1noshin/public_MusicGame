using Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionContllore: MonoBehaviour
    {
        Notes.NotesObject notes;

        private void Update()
        {
            if (notes == null) { return; }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<NotesObject>(out var c_)) {

                notes = c_;
            }
        }
    }
}
