using DefaultEcs;
using DefaultEcs.System;
using UnityEngine;

namespace DefaultMatchOne
{
    public sealed partial class AddViewSystem : AEntitySetSystem<float>
    {
        readonly Transform _parent;

        public AddViewSystem(World world)
            : base(world.GetEntities().With<Asset>().Without<IView>().AsSet(), true)
        {
            _parent = new GameObject("Views").transform;
        }

        protected override void Update(float state, in Entity entity)
        {
            var prefab = Resources.Load<GameObject>(entity.Get<Asset>().Value);
            var view = Object.Instantiate(prefab, _parent).GetComponent<IView>();
            view.Link(entity);
            entity.Set(view);
        }
    }
}