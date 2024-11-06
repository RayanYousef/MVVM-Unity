using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdorableAssets.MVVM
{
    public interface IBindingSet
    {
        void BindToProperty(object propertyObject, PropertyInfo property, INotifyPropertyUpdated view, List<string> propertiesName);
        void SetExpressionFunction(Func<object, object> updateExpression);
    }
}
