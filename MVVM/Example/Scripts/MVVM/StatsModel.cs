using System;
using System.Reflection;
using UnityEngine;

namespace AdorableAssets.MVVM
{
    [Serializable]
    public class StatsModel : IModel
    {
        private string _name;
        private int _health;
        private int _maxHealth;
        private Vector3 _position;
        public PropertyUpdatedEvent OnPropertyUpdated { get; set; }

        public T GetPropertyValue<T>(string propertyName)
        {
            PropertyInfo propertyInfo = GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                return (T)propertyInfo.GetValue(this);
            }
            else
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist in {nameof(GetType)}.");
            }
        }

        public void BroadcastAllProperties()
        {
            var propertiesInfo = GetType().GetProperties();
            foreach (var property in propertiesInfo)
            {
                OnPropertyUpdated?.Invoke(property.Name, property.GetValue(this));
            }
        }

        public StatsModel(int MaxHealth = 5000)
        {
            this.MaxHealth = MaxHealth;
            Health = this.MaxHealth;
      
            Position = new Vector3(Screen.width/2, Screen.height/2, 0);
        }

        public int Health
        {
            get => _health;
            set
            {
                if (_health != value)
                {
                    _health = value;
                    OnPropertyUpdated?.Invoke(EnumStatTypes.Health.ToString(), _health);
                    OnPropertyUpdated?.Invoke(EnumStatTypes.HealthPercentage.ToString(), HealthPercentage);
                }
            }
        }

        public int MaxHealth
        {
            get => _maxHealth;
            set
            {
                if (_maxHealth != value)
                {
                    _maxHealth = value;
                    OnPropertyUpdated?.Invoke(EnumStatTypes.MaxHealth.ToString(), _maxHealth);
                    OnPropertyUpdated?.Invoke(EnumStatTypes.HealthPercentage.ToString(), HealthPercentage);
                }
            }
        }
        public float HealthPercentage => (float)_health / _maxHealth;

        public Vector3 Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyUpdated?.Invoke(EnumStatTypes.Position.ToString(), _position);
                }
            }
        }

    }

}
