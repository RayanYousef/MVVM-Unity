using UnityEngine;

namespace AdorableAssets.MVVM
{
    public class StatsViewModel : ViewModelBase<StatsModel>
    {
        public int Health => GetPropertyValue<int>(EnumStatTypes.Health.ToString());
        public int MaxHealth => GetPropertyValue<int>(EnumStatTypes.MaxHealth.ToString());

        public void ApplyDamage()
        {
            Model.Health -= 500;
        }

        public void IncreaseMaximumHealth()
        {
            Model.MaxHealth += 200;
        }

        public void MoveToRight()
        {
            Model.Position += Vector3.right * Screen.width * 0.1f;
        }

        public void MoveToLeft()
        {
            Model.Position += Vector3.left * Screen.width * 0.1f;
        }
    }
}
