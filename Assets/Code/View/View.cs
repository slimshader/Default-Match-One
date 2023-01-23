using DefaultEcs;
using UnityEngine;

public class View : MonoBehaviour, IView
{
    protected Entity _linkedEntity;

    public virtual void Link(Entity entity)
    {
        _linkedEntity = entity;
    }

    public void OnPosition(Vector2Int value)
    {
        transform.localPosition = new Vector3(value.x, value.y);
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
