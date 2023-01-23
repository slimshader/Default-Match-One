using DefaultEcs;
using DefaultEcs.System;
using Messages;

public class ScoreSystem : AEntitySetSystem<float>
{
    public ScoreSystem(World world) : base(world.GetEntities().With<Piece>().With<IsDestroyed>().AsSet(), true)
    {
        
    }

    protected override void Update(float state, in Entity entity)
    {
        var newScore = World.Get<Score>().Value + 1;
        World.Set(new Score() { Value = newScore });
        World.Publish(new NewScore() { Value = newScore });
    }
}
