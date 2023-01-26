using DefaultEcs;
using DefaultEcs.System;

namespace DefaultMatchOne
{
    public class ProcessInputSystem : AComponentSystem<float, InputComponent>
    {
        private readonly EntityMap<Position> _byPosition;

        public ProcessInputSystem(World world) : base(world)
        {
            _byPosition = world.GetEntities().With<Piece>().Without<IsDestroyed>().AsMap<Position>();
        }

        protected override void Update(float dt, ref InputComponent component)
        {
            if (_byPosition.TryGetEntity(new Position() { Value = component.Value }, out var e))
            {
                if (e.Has<IsInteractable>())
                    e.Set<IsDestroyed>();
            }

            _byPosition.Complete();
        }
    }
}