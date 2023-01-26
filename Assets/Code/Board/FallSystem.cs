using DefaultEcs;
using DefaultEcs.System;
using UnityEngine;

namespace DefaultMatchOne
{
    public sealed class FallSystem : AEntitySetSystem<float>
    {
        private readonly EntityMap<Position> _movablePiecesByPosition;

        public FallSystem(World world) : base(world.GetEntities().With<Piece>().WhenAdded<IsDestroyed>().AsSet(), true)
        {
            _movablePiecesByPosition = world.GetEntities()
                .With<Piece>()
                .With<IsMovable>()
                .Without<IsDestroyed>()
                .AsMap<Position>();
        }

        protected override void Update(float state, in Entity entity)
        {
            var board = World.Get<Board>().Size;

            for (var x = 0; x < board.x; x++)
            {
                for (var y = 1; y < board.y; y++)
                { 
                    var position = new Vector2Int(x, y);

                    if (_movablePiecesByPosition.TryGetEntity(new Position() { Value = position }, out var e))
                    {
                        MoveDown(e, position);
                    }
                }
            }
        }

        private void MoveDown(in Entity e, Vector2Int position)
        {
            var nextRowPos = BoardLogic.GetNextEmptyRow(e.World, position);
            if (nextRowPos != position.y)
            {
                Position pos = e.Get<Position>();
                pos.Value = new Vector2Int(position.x, nextRowPos);
                e.Set(pos);
            }
        }
    }
}
