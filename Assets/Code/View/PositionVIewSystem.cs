using DefaultEcs;
using DefaultEcs.System;

namespace DefaultMatchOne
{
    public class PositionViewSystem : AEntitySetSystem<float>
    {
        public PositionViewSystem(World world) : base(world.GetEntities()
            .With<ViewComponent>()
            .WhenChanged<Position>()
            .WhenAdded<Position>().AsSet())
        {

        }

        protected override void Update(float state, in Entity entity)
        {
            entity.Get<ViewComponent>().Value.OnPosition(entity.Get<Position>().Value);
        }
    }
}