using UnityEngine;

namespace AdorableAssets.MVVM
{
    public class PlayerTransformView : ViewBase<StatsViewModel>
    {
        protected override void RegisterBindings()
        {
            BindingManager
                .ForComponent<Transform>("PlayerTransform", trans => trans.position)
                .ToProperties(EnumStatTypes.Position.ToString())
                .OneWay();
        }

        public override void OnPropertyUpdatedMethod(string propertyName, in object propertyNewValue)
        {
            base.OnPropertyUpdatedMethod(propertyName, propertyNewValue);

            if (propertyName == EnumStatTypes.Health.ToString() && ViewModel.GetPropertyValue<int>(EnumStatTypes.Health.ToString()) <= 0)
            {
                HideUI();
            }
        }
        public override void HideUI()
        {
            gameObject.SetActive(false);
        }
    }
}
