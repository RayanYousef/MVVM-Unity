using UnityEngine;
namespace AdorableAssets.MVVM
{
    public abstract class ViewBase<TViewModel> : MonoBehaviour, IView<TViewModel> where TViewModel: IViewModel
    {
        public PropertyUpdatedEvent OnPropertyUpdated { get; set; }

        [field: SerializeField] public BindingManager BindingManager { get; private set; } = new ();
        [field: SerializeField] public CanvasGroup CanvasGroup { get; set; }

        protected TViewModel ViewModel => (TViewModel) _viewModel;
        private IViewModel _viewModel;

        protected abstract void RegisterBindings();

        public void Initialize(IViewModel _viewModel)
        {
            BindingManager.SetView(this);
            this._viewModel = _viewModel;
            this.ViewModel.OnPropertyUpdated += OnPropertyUpdatedMethod;
            RegisterBindings();
            _viewModel.BroadcastAllProperties();
        }

        public virtual void HideUI()
        {
            if (CanvasGroup != null)
            {
                CanvasGroup.alpha = 0;
            }
            else
            gameObject.SetActive(false);
        }

        public virtual void ShowUI()
        {
            if (CanvasGroup != null)
            {
                CanvasGroup.alpha = 1;
            }
            else
            gameObject.SetActive(true);
        }

        public virtual void OnPropertyUpdatedMethod(string propertyName, in object propertyNewValue)
        {
            OnPropertyUpdated?.Invoke(propertyName, propertyNewValue);
        }
    }
}
