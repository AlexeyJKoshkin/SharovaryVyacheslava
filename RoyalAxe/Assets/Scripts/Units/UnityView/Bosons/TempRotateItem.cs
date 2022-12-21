using UnityEngine;

namespace RoyalAxe.Units {
    public class TempRotateItem : MonoBehaviour
    {
        [Range(0,30)]
        [SerializeField] private float _speed = 1;
        
        private void Update()
        {
            this.transform.Rotate(Vector3.forward, Mathf.PI * 2 / 10 * _speed);
        }
    }
}