using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdorableAssets.MVVM
{
    public class BindingSet : IBindingSet
    {
        private List<string> _observedPropertiesName;
        private Func<object, object> _expressionFunction;

        private PropertyInfo _property;
        private object _propertyObject;

        public void BindToProperty(object propertyObject, PropertyInfo property, INotifyPropertyUpdated viewNotifyPropertyUpdated, List<string> propertiesName)
        {
            _property = property;
            _propertyObject = propertyObject;
            ListenToProperties(propertiesName);
            RegisterView(viewNotifyPropertyUpdated);
        }

        public void SetExpressionFunction(Func<object, object> updateExpression)
        {
            _expressionFunction = updateExpression;
        }

        private void ListenToProperties(List<string> propertiesNames)
        {
            _observedPropertiesName = propertiesNames;
        }

        private void RegisterView(INotifyPropertyUpdated viewPropertyUpdated)
        {
            viewPropertyUpdated.OnPropertyUpdated += OnPropertyUpdated;
        }

        private void OnPropertyUpdated(string propertyName, in object newValue)
        {
            if (!_observedPropertiesName.Contains(propertyName))
                return;

            object valueToSend = newValue;

            // Apply the custom update function if provided
            if (_expressionFunction != null)
            {
                valueToSend = ApplyExpressionToValue(newValue);
            }

            ValidatePropertyType(ref valueToSend);
            _property.SetValue(_propertyObject, valueToSend);
        }

        private object ApplyExpressionToValue(object newValue)
        {
            var computedValue = _expressionFunction(newValue);
            ValidatePropertyType(ref computedValue);
            return computedValue;
        }

        private void ValidatePropertyType(ref object value)
        {
            if (_property != null)
            {
                Type propertyType = _property.PropertyType;

                try
                {
                    value = Convert.ChangeType(value, propertyType);
                }
                catch (InvalidCastException ex)
                {
                    throw new Exception($"Failed to convert value to target property type ({propertyType}).", ex);
                }
            }
            else throw new Exception($"Property of {this} is null");
        }

    }
}
