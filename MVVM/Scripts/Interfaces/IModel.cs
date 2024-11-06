namespace AdorableAssets.MVVM
{
    public interface IModel : INotifyPropertyUpdated
    {
        /// <summary>
        /// Broadcasts the current values of all properties from the model to the view.
        /// This ensures that, upon initialization, the view is synchronized with the model's data.
        /// Typically called when any view is first initialized to provide an up-to-date display.
        /// </summary>
        void BroadcastAllProperties();
        T GetPropertyValue<T>(string propertyName);
    }
}
