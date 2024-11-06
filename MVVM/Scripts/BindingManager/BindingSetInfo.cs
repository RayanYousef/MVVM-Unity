using System;
using UnityEngine;

namespace AdorableAssets.MVVM
{
    [Serializable]
    public struct BindingSetInfo
    {
        public BindingSetInfo(string componentName, Component component)
        {
            ComponentName = componentName;
            Component = component;
        }

        [field: SerializeField] public string ComponentName { get; private set; }
        [field: SerializeField] public Component Component { get; private set; }
    }
}
