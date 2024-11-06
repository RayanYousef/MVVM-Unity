using System;
using System.Linq.Expressions;
using UnityEngine;

namespace AdorableAssets.MVVM
{
    public interface IBindingManager
    {
        void OneWay();
        void SetView(INotifyPropertyUpdated viewNotifyPropertyUpdated);
        IBindingManager ForComponent<TComponent>( string componentName, Expression<Func<TComponent, object>> componentPropertyExpression) where TComponent : Component;
        IBindingManager UsingExpression(Func<object, object> updateExpression);
        IBindingManager ToProperties(params string[] modelPropertyName);
    }
}