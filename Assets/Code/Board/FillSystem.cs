using DefaultEcs;
using DefaultEcs.System;
using UnityEngine;

namespace DefaultMatchOne
{
    [With(typeof(Piece))]
    [With(typeof(IsDestroyed))]
    public sealed class FillSystem : AEntitySetSystem<float>
    {
        public FillSystem(World world) : base(world)
        {
            
        }

        protected override void Update(float state, in Entity entity)
        {
            var board = World.Get<Board>().Size;
            for (var x = 0; x < board.x; x++)
            {
                var position = new Vector2Int(x, board.y);
                var nextRowPos = BoardLogic.GetNextEmptyRow(World, position);
                while (nextRowPos != board.y)
                {
                    World.CreateRandomPiece(x, nextRowPos);
                    nextRowPos = BoardLogic.GetNextEmptyRow(World, position);
                }
            }
        }
    }
}
