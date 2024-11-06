namespace AdorableAssets.MVVM
{
    public interface INotifyPropertyUpdated
    {
        public PropertyUpdatedEvent OnPropertyUpdated { get; set; }
    }
}
