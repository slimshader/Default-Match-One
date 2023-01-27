using DefaultEcs;
using DefaultEcs.System;
using System;

namespace DefaultMatchOne
{
    public sealed class PositionViewSystem : AEntitySetSystem<float>
    {
        public PositionViewSystem(World world) : base(world.GetEntities()
            .With<ViewComponent>()
            .WhenChanged<Position>()
            .WhenAdded<Position>().AsSet())
        {

        }

        protected override void Update(float state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.Get<ViewComponent>().Value.OnPosition(entity.Get<Position>().Value);
            }
        }
    }
}