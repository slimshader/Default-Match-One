using DefaultEcs;
using UnityEngine;

namespace DefaultMatchOne
{
    public static class BoardLogic
    {
        public static int GetNextEmptyRow(World world, Vector2Int position)
        {
            var piecesByPosition = world.GetEntities()
                .With<Piece>()
                .Without<IsDestroyed>()
                .AsMap<Position>();

            position.y -= 1;
            while (position.y >= 0 && !piecesByPosition.ContainsKey(new Position() { Value = position }))
            {
                position.y -= 1;
            }

            piecesByPosition.Dispose();
            piecesByPosition.Complete();

            return position.y + 1;
        }
    }
}
