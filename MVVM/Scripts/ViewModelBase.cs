using UnityEngine;

namespace AdorableAssets.MVVM
{
    public abstract class ViewModelBase<ModelType> : IViewModel where ModelType : IModel
    {
        public PropertyUpdatedEvent OnPropertyUpdated { get; set; }

        private IModel _modelBase;
        protected ModelType Model => ((ModelType)_modelBase);
        public T GetPropertyValue<T>(string propertyName) => _modelBase.GetPropertyValue<T>(propertyName);
        public void BroadcastAllProperties() => Model.BroadcastAllProperties();

        public void Initialize(IModel _modelBase)
        {
            this._modelBase = _modelBase;
            _modelBase.OnPropertyUpdated += OnPropertyUpdatedMethod;

            Debug.Log($"Constructed ViewModel {this} using Model{_modelBase}");
        }

        public virtual void OnPropertyUpdatedMethod(string propertyName, in object propertyNewValue)
        {
            OnPropertyUpdated?.Invoke(propertyName, propertyNewValue);
        }

    }
}
