using DefaultEcs;
using DefaultEcs.System;

namespace DefaultMatchOne
{
    [With(typeof(IsDestroyed))]
    public class DisposeDestroyedSystem : AEntitySetSystem<float>
    {
        public DisposeDestroyedSystem(World world) : base(world, true)
        {

        }

        protected override void Update(float state, in Entity entity)
        {
            entity.Dispose();
        }
    }
}
