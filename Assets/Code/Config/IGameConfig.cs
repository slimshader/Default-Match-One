using UnityEngine;

public interface IGameConfig
{
    Vector2Int BoardSize { get; }
    float BlockerProbability { get; }
}
