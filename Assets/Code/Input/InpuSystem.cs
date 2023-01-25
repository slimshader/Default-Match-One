using DefaultEcs;
using DefaultEcs.System;
using System;
using UnityEngine;

namespace DefaultMatchOne
{
    public class InpuSystem : ISystem<float>
    {
        private readonly World _world;
        private readonly Camera _camera;

        public InpuSystem(World world, Camera camera)
        {
            _world = world;
            _camera = camera;
            IsEnabled = true;
        }

        public bool IsEnabled { get; set; }

        public void Dispose() { }

        public void Update(float state)
        {
            if (IsEnabled)
            {
                var input = _world.Has<BurstMode>()
                    ? Input.GetMouseButton(0)
                    : Input.GetMouseButtonDown(0);

                if (input)
                {
                    var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                    var e = _world.CreateEntity();
                    e.Set(new InputComponent()
                    {
                        Value = new Vector2Int(
                        (int)Math.Round(mouseWorldPos.x),
                        (int)Math.Round(mouseWorldPos.y)
                        )
                    });
                }
            }
        }
    }
}