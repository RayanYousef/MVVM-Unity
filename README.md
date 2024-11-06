Here’s the updated usage guide incorporating all your points:

---

## Installation

Add this package to your Unity project as a Git-based package:

```bash
https://github.com/yourusername/AdorableAssets.MVVM.git
```

In Unity, navigate to **Window > Package Manager**, then select **+ > Add package from git URL…**, and paste the link above.

## Usage Guide

This MVVM framework provides base classes and interfaces to help you quickly set up models, views, and viewmodels in Unity. You can either:
- **Extend the provided base classes** (`ViewModelBase` and `ViewBase<TViewModel>`) for a convenient setup with built-in functionality.
- **Or, implement the interfaces** (`IModel`, `IViewModel`, and `IView`) directly if you need more control over your components, though you’ll need to implement the methods yourself.

> **Note**: This system currently supports **One-Way Binding** only, meaning data flows from the model to the view but does not flow back from the view to the model. **Two-Way Binding support is planned for a future update.**

---

### Step 1: Implementing Model, ViewModel, and View Classes

1. **Model**: Implement the `IModel` interface to set up your data layer. Models manage data properties and notify listeners of changes.
2. **ViewModel**: Extend `ViewModelBase` to simplify managing connections between models and views, or implement `IViewModel` if you prefer custom functionality.
3. **View**: Create a class that extends `ViewBase<TViewModel>` to bind UI components to a `ViewModel`. Alternatively, implement `IView` directly for a fully customized setup.

### Example: Setting Up a Simple Model

Here's an example of a basic `StatsModel`:

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

This simple `StatsModel`:
- Declares a `Health` property.
- Raises the `OnPropertyUpdated` event when `Health` changes, enabling updates to be reflected in any bound views.

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
2. Register the view with a `ViewModel`, which automatically calls `BroadcastAllProperties()` on initialization, ensuring the UI is up-to-date.

```csharp
public class PlayerHealthView : ViewBase<StatsViewModel>
{
    protected override void RegisterBindings()
    {
        BindingManager
            .ForComponent<HealthBar>("PlayerHealthBar", healthBar => healthBar.HealthBarFillImage.fillAmount)
            .ToProperties("Health")
            .OneWay();
    }
}
```

### Linking the View in Unity Editor

*Refer to the images below* for step-by-step guidance on connecting the view components in the Unity Editor.

---

This setup enables a flexible and decoupled UI framework for Unity, where each component has a clear responsibility. Two-Way Binding will be introduced in a future update for enhanced interactivity. 

Let me know if you’d like any further adjustments!