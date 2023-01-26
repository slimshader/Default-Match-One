using System;
using UnityEngine;

namespace DefaultMatchOne
{
    public class CameraView : MonoBehaviour
    {
        Camera _cam;

        void Awake()
        {
            _cam = GetComponent<Camera>();
        }

        public void OnAnyBoard(Vector2Int size)
        {
            _cam.orthographicSize = Math.Max(size.x, size.y) * 0.7f;
            transform.localPosition = new Vector3(
                size.x * 0.5f - 0.5f,
                size.y * 0.6f,
                -10
            );
        }
    }
}
