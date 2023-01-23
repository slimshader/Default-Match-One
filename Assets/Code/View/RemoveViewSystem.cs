using DefaultEcs;
using DefaultEcs.System;

public sealed partial class RemoveViewSystem : AEntitySetSystem<float>
{
    public RemoveViewSystem(World world)
        : base(world.GetEntities().With<IsDestroyed>().With<ViewComponent>().AsSet(), true)
    {
    }

    protected override void Update(float state, in Entity entity)
    {
        entity.Get<ViewComponent>().Value.OnDestroy();
    }
}
