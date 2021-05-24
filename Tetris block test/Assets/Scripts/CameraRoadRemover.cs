using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisRunnerSpace
{
    namespace CameraSpace
    {
        [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
        public class CameraRoadRemover : MonoBehaviour
        {
            [SerializeField]
            float _xDistance = 20;

            private void Start()
            {
                transform.position = new Vector3(-_xDistance, 0, 0);
            }
            private void OnTriggerEnter(Collider other)
            {
                IDieable road = other.GetComponent<IDieable>();
                if (road != null)
                    road.Die();
            }
        }
    }
}
