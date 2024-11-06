

# MVVM For Unity
<div align="center">
<img src="https://drive.google.com/uc?export=view&id=1kfZoxRDHgjRGHnxWMXDGHTlXi96mkgVH"  height ="500" />
</div>

This framework provides a structured Model-View-ViewModel (MVVM) system for Unity, designed to separate your app’s data (Model), user interface (View), and business logic (ViewModel).

Model holds the data and notifies the ViewModel of any changes.
ViewModel acts as an intermediary, exposing data and logic for the View without knowing any UI details.
View binds to the ViewModel, ensuring the UI reflects the data from the Model in real-time.

> **Limitations:** This system currently supports **One-Way Binding** only, meaning data flows from the Model to the View but cannot flow back from the View to the Model. Future updates will add Two-Way Binding for greater interactivity.

## Dependencies
No required dependencies

## Installation

Add this package to your Unity project as a Git-based package:

```bash
https://github.com/RayanYousef/MVVM-Unity.git
```

Open Unity and go to **Window > Package Manager**, select **+ > Add package from git URL…**, and paste the link above.


## Usage Guide

This MVVM framework provides base classes and interfaces to streamline setting up a model-view-viewmodel architecture in Unity. You can either:
- Extend the provided base classes for convenience.
- Or, if you need more control, implement the interfaces directly.

### Step 1: Implementing Model, ViewModel, and View Classes

This MVVM framework provides flexibility in setting up `Model`, `ViewModel`, and `View` components. You can either:

- **Extend the provided base classes** (`ViewModelBase` and `ViewBase<TViewModel>`) for a quick setup with built-in functionality.
- **Or, implement the interfaces** (`IModel`, `IViewModel`, and `IView`) directly if you need more customization.

> If you implement the interfaces, ensure correct implementation of the required interfaces to maintain seamless communication between the two components.

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
