using DefaultEcs;
using DefaultEcs.System;

public class PositionViewSystem : AEntitySetSystem<float>
{
    public PositionViewSystem(World world) : base(world.GetEntities().With<ViewComponent>().With<Position>().AsSet())
    {
        
    }

    protected override void Update(float state, in Entity entity)
    {
        //base.Update(state, entity);
        entity.Get<ViewComponent>().Value.OnPosition(entity.Get<Position>().Value);
    }
}
