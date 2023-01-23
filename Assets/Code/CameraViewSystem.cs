using DefaultEcs;
using DefaultEcs.System;
using System;
using UnityEngine;

public class CameraViewSystem : AEntitySetSystem<float>
{
    private readonly Camera _camera;

    public CameraViewSystem(World world, Camera camera) : base(world.GetEntities().WhenChanged<Board>().WhenAdded<Board>().AsSet())
    {
        _camera = camera;
    }

    protected override void Update(float state, in Entity entity)
    {
        var size = entity.Get<Board>().Value;
        _camera.orthographicSize = Math.Max(size.x, size.y) * 0.7f;
        _camera.transform.localPosition = new Vector3(
            size.x * 0.5f - 0.5f,
            size.y * 0.6f,
            -10
        );
    }
}
