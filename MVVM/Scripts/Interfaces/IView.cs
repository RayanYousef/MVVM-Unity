using UnityEngine;

namespace AdorableAssets.MVVM
{
    public interface IView<TViewModel> : INotifyPropertyUpdated where TViewModel : IViewModel
    {
        void OnPropertyUpdatedMethod(string propertyName, in object propertyNewValue);
        void Initialize(IViewModel _viewModel);
        public BindingManager BindingManager { get; }
        public CanvasGroup CanvasGroup { get; }
        public void ShowUI();
        public void HideUI();
    }
}
