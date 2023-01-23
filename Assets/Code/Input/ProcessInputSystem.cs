using DefaultEcs;
using DefaultEcs.System;

public class ProcessInputSystem : AComponentSystem<float, InputComponent>
{
    private readonly EntityMap<Position> _byPosition;

    public ProcessInputSystem(World world) : base(world)
    {
        _byPosition = world.GetEntities().With<Piece>().AsMap<Position>();
    }

    protected override void Update(float dt, ref InputComponent component)
    {
        if (_byPosition.TryGetEntity(new Position() { Value = component.Value }, out var e))
        {
            if (e.Has<IsInteractable>())
                e.Set<IsDestroyed>();
        }
    }
}
