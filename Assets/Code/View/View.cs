using DefaultEcs;
using UnityEngine;

namespace DefaultMatchOne
{
    public class View : MonoBehaviour, IView
    {
        protected Entity _linkedEntity;

        public virtual void Link(Entity entity)
        {
            _linkedEntity = entity;

            var pos = _linkedEntity.Get<Position>().Value;
            transform.localPosition = new Vector3(pos.x, pos.y);
        }

        public virtual void OnPosition(Vector2Int value)
        {
            transform.localPosition = new Vector3(value.x, value.y);
        }

        public virtual void OnDestroyed()
        {
            Destroy(gameObject);
        }
    }
}