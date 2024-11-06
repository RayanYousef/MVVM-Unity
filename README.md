Here's a simplified and clearer explanation for your README:

---

## Installation

Add this package to your Unity project as a Git-based package:

```bash
https://github.com/RayanYousef/MVVM-Unity.git
```

Open Unity and go to **Window > Package Manager**, select **+ > Add package from git URL…**, and paste the link above.

## Usage Guide

### Step 1: Implementing Model, ViewModel, and View Classes

This asset provides base classes and interfaces to help you quickly create an MVVM structure in Unity. 

1. **Model**: Implement the `IModel` interface for your data layer. Models handle properties and notify listeners of changes.
2. **ViewModel**: Extend `ViewModelBase` to provide logic that connects models to views.
3. **View**: Extend `ViewBase<TViewModel>` to connect UI components to the `ViewModel`.

---

### Example: Setting Up a Simple Model

To illustrate, here’s a basic `StatsModel` example:

```csharp
using System;
using UnityEngine;

namespace AdorableAssets.MVVM
{
    [Serializable]
    public class StatsModel : IModel
    {
        public PropertyUpdatedEvent OnPropertyUpdated { get; set; }
        private int _health;
        
        public int Health
        {
            get => _health;
            set
            {
                if (_health != value)
                {
                    _health = value;
                    OnPropertyUpdated?.Invoke("Health", _health);
                }
            }
        }
    }
}
```

This simple `StatsModel` example:
- Declares a `Health` property.
- Raises the `OnPropertyUpdated` event when `Health` changes, ensuring updates can be reflected in the view.

### Step 2: Creating a ViewModel

To use this model, create a `ViewModel` by extending `ViewModelBase`:

```csharp
public class StatsViewModel : ViewModelBase<StatsModel>
{
    // Optionally, add ViewModel-specific methods here
}
```

### Step 3: Setting Up a View

To display data in Unity, create a `View` class by extending `ViewBase<TViewModel>`.

In your `View` class:
1. Bind UI components to model properties.
2. Link the View in Unity Editor.

#### 1-Binding UI components to model properties.

```csharp
public class PlayerHealthView : ViewBase<StatsViewModel>
{
    protected override void RegisterBindings()
    {
        BindingManager
            .ForComponent<Image>("HealthImage", healthImage => healthImage.fillAmount)
            .ToProperties("Health")
            .OneWay();
    }
}
```

#### 2-Linking the View in Unity Editor

*Refer to the images below* for step-by-step guidance on connecting the view components in the Unity Editor.
