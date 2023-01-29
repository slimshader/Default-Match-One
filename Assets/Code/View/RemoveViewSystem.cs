using DefaultEcs.System;

namespace DefaultMatchOne
{
    [With(typeof(IsDestroyed))]
    public sealed partial class RemoveViewSystem : AEntitySetSystem<float>
    {
        [Update]
        private void Update(float _, in IView view)
        {
            view.OnDestroyed();
        }
    }
}
