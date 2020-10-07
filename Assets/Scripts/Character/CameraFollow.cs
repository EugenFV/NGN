using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private float _minCameraPosition = -43.0f;
        [SerializeField] private float _maxCameraPosition = 0.0f;
        public float smoothSpeed = 5f;
        private float _deltaOffset = 11.0f;

        void Update()
        {
            Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(target.position.z - _deltaOffset, _minCameraPosition, _maxCameraPosition));
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
    // Only for test 00
}
