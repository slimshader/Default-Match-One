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

            var _movablePiecesByPosition = World.GetEntities()
                .With<Piece>()
    .With<IsMovable>()
    .Without<IsDestroyed>()
    .AsMap<Position>();

            var recorder = new EntityCommandRecorder();

            for (var x = 0; x < board.x; x++)
            {
                for (var y = 1; y < board.y; y++)
                {
                    var position = new Vector2Int(x, y);

                    if (_movablePiecesByPosition.TryGetEntity(new Position() { Value = position }, out var e))
                    {
                        var record = recorder.Record(e);
                        MoveDown(e, position, in record);
                    }
                }
            }

            _movablePiecesByPosition.Dispose();
            _movablePiecesByPosition.Complete();
            recorder.Execute();
        }

        private void MoveDown(in Entity e, Vector2Int position, in EntityRecord record)
        {
            var nextRowPos = BoardLogic.GetNextEmptyRow(e.World, position);
            if (nextRowPos != position.y)
            {
                Position pos = e.Get<Position>();
                pos.Value = new Vector2Int(position.x, nextRowPos);

                try
                {
                    e.Set(pos);
                    //record.Set(pos);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"When moving {position} to {pos.Value}");
                }
            }
        }
    }
}
