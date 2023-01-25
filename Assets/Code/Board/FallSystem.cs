using DefaultEcs;
using DefaultEcs.Command;
using DefaultEcs.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultMatchOne
{

    public class FallSystem : AEntitySetSystem<float>
    {
        private readonly EntityMap<Position> _movablePiecesByPosition;

        public FallSystem(World world) : base(world.GetEntities().With<Piece>().WhenAdded<IsDestroyed>().AsSet(), true)
        {
            //_movablePiecesByPosition = world.GetEntities()
            //    .With<Piece>()
            //    .With<IsMovable>()
            //    .AsMap<Position>();
        }

        protected override void Update(float state, in Entity entity)
        {
            var board = World.Get<Board>().Size;

            for (var x = 0; x < board.x; x++)
            {
                for (var y = 1; y < board.y; y++)
                {
                    var movablePiecesByPosition = World.GetEntities()
                                                    .With<Piece>()
                                                    .With<IsMovable>()
                                                    .Without<IsDestroyed>()
                                                    .AsMap<Position>();

                    var position = new Vector2Int(x, y);

                    if (movablePiecesByPosition.TryGetEntity(new Position() { Value = position }, out var e))
                    {
                        movablePiecesByPosition.Dispose();
                        movablePiecesByPosition.Complete();

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

                try
                {
                    e.Set(pos);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"When moving {position} to {pos.Value}");
                }
            }
        }
    }
}
