using DefaultEcs;
using UnityEngine;

public interface IView
{
    void Link(Entity entity);

    // not as in original
    void OnDestroy();
    void OnPosition(Vector2Int value);
}
