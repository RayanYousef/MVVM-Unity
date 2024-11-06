using UnityEngine;

namespace AdorableAssets.MVVM
{
    public class SystemExample : MonoBehaviour
    {
        [SerializeField] private PlayerTransformView _transformView;
        [SerializeField] private PlayerHealthView _healthView;

        private IViewModel _statsViewModel;
        private IModel _statsModel;

        private void Awake()
        {
            Setup();
        }

        private void Setup()
        {
            _statsModel = new StatsModel();
            _statsViewModel = new StatsViewModel();
            _statsViewModel.Initialize(_statsModel);

            _transformView.Initialize(_statsViewModel);
            _healthView.Initialize(_statsViewModel);
        }

        public void ApplyDamage() => (_statsViewModel as StatsViewModel).ApplyDamage();
        public void MaxHealthUpdate() => (_statsViewModel as StatsViewModel).IncreaseMaximumHealth();
        public void MoveRight() => (_statsViewModel as StatsViewModel).MoveToRight();
        public void MoveLeft() => (_statsViewModel as StatsViewModel).MoveToLeft();

    }
}
