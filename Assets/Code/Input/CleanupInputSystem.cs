using DefaultEcs;
using DefaultEcs.System;

namespace DefaultMatchOne
{
    [With(typeof(InputComponent))]
    public class CleanupInputSystem : AEntitySetSystem<float>
    {
        public CleanupInputSystem(World world) : base(world, true)
        {

        }

        protected override void Update(float state, in Entity entity)
        {
            entity.Dispose();
        }
    }
}