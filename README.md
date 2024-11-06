

<h1 align="center">MVVM For Unity</h1>

<div align="center">
<img src="https://drive.google.com/uc?export=view&id=1CqKV46T1OPKX7DQ6_XTbcNkiSwPpcfW4"  height ="500" />
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

> If you implement the interfaces, ensure correct implementation of the required interfaces to maintain seamless communication between the components.

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

#### 1. Binding UI Components to Model Properties

```csharp
public class PlayerHealthView : ViewBase<StatsViewModel>
{
    protected override void RegisterBindings()
    {
        BindingManager
            .ForComponent<Image>("HealthBarImage", healthBarImage => healthBarImage.fillAmount)
            .ToProperties("Health")
            .OneWay();
    }
}
```

> In the Unity Editor, assign the **Image** component (or the GameObject containing the **Image** component) to the corresponding field. You can then bind it by using the **component name** and **type** with `ForComponent<Type>("Component Name", ...)`. 
<div align="center">
<img src="https://drive.google.com/uc?export=view&id=1ohSnanA3sMA5oKkoyEjXkh_doIpLarEf"  height ="350" style="display: inline-block;"/>
</div>

#### 2. Binding Multiple Components to the Same View

In some cases, you might want to bind multiple components to the same View. Below is an example where multiple UI elements from the same GameObject are bound to different properties of the `StatsViewModel`. This allows you to manage several visual elements at once with a single `View` class.

#### HealthBar Class

First, let’s define the `HealthBar` class which holds references to the `Image` and `Text` components:

```csharp
using UnityEngine;
using UnityEngine.UI;

namespace AdorableAssets.MVVM
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarFillImage;
        [SerializeField] private Text _healthValueText;

        public Image HealthBarFillImage  => _healthBarFillImage;
        public Text HealthValueText  => _healthValueText; 
    }
}
```

> The `HealthBar` class contains serialized fields for the `Image` and `Text` components that will be linked in the Unity Editor. These fields are used in the `View` class to set up bindings between the UI and the model properties.

#### PlayerHealthView Class

Now, let’s set up the `PlayerHealthView` class to bind multiple components (such as the health bar fill, color, and text) to different properties of the `StatsViewModel`.

```csharp
public class PlayerHealthView : ViewBase<StatsViewModel>
{
    [SerializeField] private HealthBar _healthBar;

    protected override void RegisterBindings()
    {
        // Binding health percentage to the fill amount of the HealthBar
        BindingManager
            .ForComponent<HealthBar>("PlayerHealthBar", healthBar => healthBar.HealthBarFillImage.fillAmount)
            .ToProperties(EnumStatTypes.HealthPercentage.ToString())
            .OneWay();

        // Binding health percentage to the color of the HealthBar's fill image
        BindingManager
            .ForComponent<HealthBar>("PlayerHealthBar", healthBar => healthBar.HealthBarFillImage.color)
            .ToProperties(EnumStatTypes.HealthPercentage.ToString())
            .UsingExpression(value =>
            {
                Color colorToReturn = Color.Lerp(Color.red, Color.green, (float)value);
                return colorToReturn;
            })
            .OneWay();

        // Binding health percentage to the color of the Health Value Text
        BindingManager
            .ForComponent<HealthBar>("PlayerHealthBar", healthBar => healthBar.HealthValueText.color)
            .ToProperties(EnumStatTypes.HealthPercentage.ToString())
            .UsingExpression(value =>
            {
                Color colorToReturn = Color.Lerp(Color.black, Color.white, (float)value);
                return colorToReturn;
            })
            .OneWay();

        // Binding health and max health to the Health Value Text
        BindingManager
            .ForComponent<HealthBar>("PlayerHealthBar", healthBar => healthBar.HealthValueText.text)
            .ToProperties(EnumStatTypes.Health.ToString(), EnumStatTypes.MaxHealth.ToString())
            .UsingExpression(value =>
            {
                string valueToReturn =
                ViewModel.Health
                + "/"
                + ViewModel.MaxHealth;
                return valueToReturn;
            })
            .OneWay();
    }
}
```

> In this example, multiple UI components from the same `HealthBar` GameObject are bound to properties from the `ViewModel`:
> - The `HealthBarFillImage.fillAmount` is bound to the `HealthPercentage` property.
> - The `HealthBarFillImage.color` and `HealthValueText.color` change based on the health percentage.
> - The `HealthValueText.text` is bound to the player's health and max health.

**This example is included within the asset**, and you can refer to it to see how multiple components can be bound to the same View. The `HealthBar` and `PlayerHealthView` classes are provided to help you understand how to work with bindings and UI components effectively.