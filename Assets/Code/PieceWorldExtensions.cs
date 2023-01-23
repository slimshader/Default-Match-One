using DefaultEcs;

public static class PieceWorldExtensions
{
    public static Entity CreateRandomPiece(this World world, int x, int y)
    {
        var entity = world.CreateEntity();
        entity.Set(new Piece() { Type = Rand.game.Int(10) });
        entity.Set(new Position() { Value = new(x, y) });
        entity.Set<IsMovable>();
        entity.Set<IsInteractable>();
        entity.Set(new Asset() { Value = "Piece" });
        return entity;
    }

    public static Entity CreateBlocker(this World world, int x, int y)
    {
        var entity = world.CreateEntity();
        entity.Set(new Piece() { Type = -1 });
        entity.Set(new Position() { Value = new(x, y) });
        entity.Set(new Asset() { Value = "Blocker" });
        return entity;
    }
}
