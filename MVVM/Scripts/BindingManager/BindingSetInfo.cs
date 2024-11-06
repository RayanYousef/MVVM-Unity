using System;
using UnityEngine;

namespace AdorableAssets.MVVM
{
    [Serializable]
    public struct BindingSetInfo
    {
        public BindingSetInfo(string dataType, Component component)
        {
            DataType = dataType;
            Component = component;
        }

        [field: SerializeField] public string DataType { get; private set; }
        [field: SerializeField] public Component Component { get; private set; }
    }
}
