using DefaultEcs;
using UnityEngine;

public interface IView
{
    void Link(Entity entity);

    // not as in original
    void OnDestroyed();
    void OnPosition(Vector2Int value);
}
