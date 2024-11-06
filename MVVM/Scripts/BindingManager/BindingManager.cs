using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace AdorableAssets.MVVM
{
    [Serializable]
    public class BindingManager : IBindingManager
    {
        [Header("View Components Settings")]
        [SerializeField] private List<BindingSetInfo> _viewVariables = new();

        private INotifyPropertyUpdated _viewNotifyPropertyUpdated;
        private string _componentName;
        private List<string> _propertiesName;
        private Func<object, object> _updateExpression;
        private PropertyInfo _property;
        private object _propertyObject;

        public void SetView(INotifyPropertyUpdated viewNotifyPropertyUpdated)
        {
            _viewNotifyPropertyUpdated = viewNotifyPropertyUpdated;
        }

        public void AddBindingSetInfo(BindingSetInfo bindingSet)
        {
            _viewVariables.Add(bindingSet);
        }

        public int GetViewVariablesCount => _viewVariables.Count;

        private void ResetBuilder()
        {
            _componentName = null;
            _propertiesName = null;
            _updateExpression = null;
        }

        public IBindingManager ForComponent<TComponent>(
            string componentName,
            Expression<Func<TComponent, object>> propertyToUpdate)
            where TComponent : Component
        {
            this._componentName = componentName;

            var propertyPath = GetPropertyPath(propertyToUpdate);
            var component = _viewVariables.Find(view => view.ComponentName == _componentName).Component.GetComponent<TComponent>();

            if(component == null)
            {
                throw new Exception("Check component name, make sure the name in the editor is similar to the name provided to BindingManager");
            }

            GetProperty(component, propertyPath);

            return this;
        }

        public string GetPropertyPath<TComponent>(Expression<Func<TComponent, object>> expression)
        {
            var propertyNames = new List<string>();
            var body = expression.Body as MemberExpression ?? ((UnaryExpression)expression.Body).Operand as MemberExpression;
            while (body != null)
            {
                propertyNames.Add(body.Member.Name);
                body = body.Expression as MemberExpression;
            }
            propertyNames.Reverse();
            return string.Join(".", propertyNames);
        }

        private void GetProperty(object obj, string propertyPath)
        {
            var properties = propertyPath.Split('.'); 
             _propertyObject = obj;

            for (int i = 0; i < properties.Length - 1; i++)
            {
                var property = _propertyObject.GetType().GetProperty(properties[i]);

                if (property == null)
                {
                    throw new InvalidOperationException($"Property '{properties[i]}' not found on {_propertyObject.GetType()}.");
                }

                _propertyObject = property.GetValue(_propertyObject);
            }

            _property = _propertyObject.GetType().GetProperty(properties.Last());

            if (_property == null)
            {
                throw new InvalidOperationException($"Property '{properties.Last()}' not found on {_propertyObject.GetType()}.");
            }
        }


        public IBindingManager ToProperties(params string[] propertyNames)
        {
            this._propertiesName = propertyNames.ToList();
            return this;
        }

        public IBindingManager UsingExpression(Func<object, object> updateExpression)
        {
            this._updateExpression = updateExpression;
            return this;
        }

        public void OneWay()
        {
            BindingSet bindingSet = new BindingSet();
            bindingSet?.BindToProperty(_propertyObject, _property, _viewNotifyPropertyUpdated, _propertiesName);
            bindingSet?.SetExpressionFunction(_updateExpression);
            ResetBuilder();
        }
    }
}