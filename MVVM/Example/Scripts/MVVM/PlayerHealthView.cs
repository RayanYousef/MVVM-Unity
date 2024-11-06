using UnityEngine;

namespace AdorableAssets.MVVM
{
    public class PlayerHealthView : ViewBase<StatsViewModel>
    {
        protected override void RegisterBindings()
        {
            BindingManager
                .ForComponent<HealthBar>("PlayerHealthBar", healthBar => healthBar.HealthBarFillImage.fillAmount)
                .ToProperties(EnumStatTypes.HealthPercentage.ToString())
                .OneWay();

            BindingManager
                .ForComponent<HealthBar>("PlayerHealthBar", healthBar => healthBar.HealthBarFillImage.color)
                .ToProperties(EnumStatTypes.HealthPercentage.ToString())
                .UsingExpression(value =>
                {
                    Color colorToReturn = Color.Lerp(Color.red, Color.green, (float)value);
                    return colorToReturn;
                })
                .OneWay();

            BindingManager
                .ForComponent<HealthBar>("PlayerHealthBar", healthBar => healthBar.HealthValueText.color)
                .ToProperties(EnumStatTypes.HealthPercentage.ToString())
                .UsingExpression(value =>
                {
                    Color colorToReturn = Color.Lerp(Color.black, Color.white, (float)value);
                    return colorToReturn;
                })
                .OneWay();

            BindingManager
                .ForComponent<HealthBar>("PlayerHealthBar", healthBar => healthBar.HealthValueText.text)
                .ToProperties(EnumStatTypes.Health.ToString(), EnumStatTypes.MaxHealth.ToString())
                .UsingExpression(value =>
                {
                    string valueToReturn =
                    ViewModel.Health
                    + "/"
                    + ViewModel.MaxHealth;
                    return valueToReturn;
                })
                .OneWay();
        }

        public override void OnPropertyUpdatedMethod(string propertyName, in object propertyNewValue)
        {
            base.OnPropertyUpdatedMethod(propertyName, propertyNewValue);

            if(propertyName == EnumStatTypes.Health.ToString() && ViewModel.GetPropertyValue<int>(EnumStatTypes.Health.ToString()) <= 0)
            {
                HideUI();
            }
        }
    }
}
