using DefaultEcs;
using UnityEngine;

namespace DefaultMatchOne
{
    public static class BoardLogic
    {
        private static EntityMap<Position> piecesByPosition;

        public static int GetNextEmptyRow(World world, Vector2Int position)
        {
            if (piecesByPosition == null)
            {
                piecesByPosition = world.GetEntities()
                    .With<Piece>()
                    .Without<IsDestroyed>()
                    .AsMap<Position>();
            }

            position.y -= 1;
            while (position.y >= 0 && !piecesByPosition.ContainsKey(new Position() { Value = position }))
            {
                position.y -= 1;
            }
            return position.y + 1;
        }
    }
}
