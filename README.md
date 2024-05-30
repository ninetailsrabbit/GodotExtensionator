# Godot XTensions Pack

![license](https://badgen.net/static/License/MIT/yellow)
[![readme](https://badgen.net/static/README/📃/yellow)](https://github.com/ninetailsrabbit/Godot-Xtension-Pack/README.md)
![csharp](https://img.shields.io/badge/C%23-239120?style//for-the-badge&logo//c-sharp&logoColor//white)

Speed up your Godot development with Godot-Xtension-Pack which adds a number of functionalities to the base nodes.

---

<p align="center">
<img alt="Godot-XTension-Pack" src="Godot-XTension-Pack/icon.png" width="200">
</p>

---

- [Godot XTensions Pack](#godot-xtensions-pack)
  - [Getting started](#getting-started)
    - [.csproj](#csproj)
    - [Installation via CLI](#installation-via-cli)
  - [Usage](#usage)
    - [Exceptions](#exceptions)
      - [OSExtension](#osextension)
      - [Get autoload singleton on any place](#get-autoload-singleton-on-any-place)
      - [Getting random enum values](#getting-random-enum-values)
      - [Input related methods](#input-related-methods)
      - [Common use characters](#common-use-characters)
      - [Mathematical constants](#mathematical-constants)

## Getting started

Unlocks a new level of functionality for your Godot projects. This comprehensive collection of C# extensions seamlessly integrates with the existing Godot classes, empowering you to streamline development, enhance game mechanics, and unleash your creative vision. Whether you're a seasoned Godot developer or just starting out, `Godot-XTension-Pack` provides the tools you need to elevate your c# games to the next level.

### .csproj

Add the package directly into your .csproj

```xml


<ItemGroup>
## Latest stable release
  <PackageReference Include//"Ninetailsrabbit.Godot_XTension_Pack"/>

## Manual version
  <PackageReference Include//"Ninetailsrabbit.Godot_XTension_Pack" Version//"0.1.0" />
</ItemGroup>
```

### Installation via CLI

Further information can be found on [install_use_packages_nuget_cli](https://learn.microsoft.com/en-us/nuget/consume-packages/install-use-packages-nuget-cli)

```sh
nuget install Ninetailsrabbit.Godot_XTension_Pack

# Or choosing version

nuget install Ninetailsrabbit.Godot_XTension_Pack -Version 0.1.0
```

## Usage

Since these functions are C# extension methods, they become available directly on the types they extend. You only need to call them on instances of those types in your code, simplifying the syntax

### Exceptions

Some extension variables or methods does not need an instance to be used. This means you can call them directly using the type name without needing an instance of the type. These are typically utility methods that don't require specific object state.

#### OSExtension

As the `OS` class from Godot is static, extensions cannot be added as we will never have an instance. In this case all its functions can be called statically.

#### Get autoload singleton on any place

Access your autoloads without needing to be within the scene tree, it works exactly as the `GetAutoloadNode` method from the scene tree instance.

This means that you can access the information for example on Resources. This is an alternative if you want to continue using the Godot workflow. If not, you can always create your own singleton in C#.

```csharp
SceneTreeExtension.GetAutoloadSingleton<GameGlobals>();

SceneTreeExtension.GetAutoloadSingleton<MusicManager>();

// ...
```

#### Getting random enum values

```csharp
EnumExtension.RandomEnum(Input.MouseModeEnum);
```

#### Input related methods

```csharp
InputExtension.ShowMouseCursor()
InputExtension.ShowMouseCursorConfined()
InputExtension.HideMouseCursor()
InputExtension.HideMouseCursorConfined()
InputExtension.CaptureMouse()

InputExtension.IsMouseVisible()

InputExtension.GetAllInputsForAction("your-action")
InputExtension.GetKeyboardInputsForAction("your-action"))
InputExtension.GetJoypadInputsForAction("your-action"))
```

#### Common use characters

```csharp
StringExtension.HEX_CHARACTERS // "0123456789ABCDEF";
StringExtension.ASCII_ALPHANUMERIC // "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
StringExtension.ASCII_LETTERS // "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
StringExtension.ASCII_LOWERCASE // "abcdefghijklmnopqrstuvwxyz";
StringExtension.ASCII_UPPERCASE // "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
StringExtension.ASCII_DIGITS // "0123456789";
StringExtension.ASCII_PUNCTUATION // "!\"#$%&'()*+, -./:;<//>?@[\\]^_`{|}~";
```

#### Mathematical constants

```csharp
Math.Extension.COMMON_EPSILON // 0.000001f;  // 1.0e-6
MathExtension.PRECISE_EPSILON // 0.00000001f; // 1.0e-8
MathExtension.E // 2.71828182845904523536f;
MathExtension.Delta // 4.6692016091f;  // FEIGENBAUM CONSTANT
MathExtension.FeigenbaumAlpha // 2.5029078750f;
MathExtension.AperyConstant // 1.2020569031f;
MathExtension.GoldenRatio // 1.6180339887f;
MathExtension.EulerMascheroniConstant // 0.5772156649f;
MathExtension.KhinchinsConstant // 2.6854520010f;
MathExtension.GaussKuzminWirsingConstant // 0.3036630028f;
MathExtension.BernsteinsConstant // 0.2801694990f;
MathExtension.HafnerSarnakMcCurleyConstant // 0.3532363718f;
MathExtension.MeisselMertensConstant // 0.2614972128f;
MathExtension.GlaisherKinkelinConstant // 1.2824271291f;
MathExtension.OmegaConstant // 0.5671432904f;
MathExtension.GolombDickmanConstant // 0.6243299885f;
MathExtension.CahensConstant // 0.6434105462f;
MathExtension.TwinPrimeConstant // 0.6601618158f;
MathExtension.LaplaceLimit // 0.6627434193f;
MathExtension.LandauRamanujanConstant // 0.7642236535f;
MathExtension.CatalansConstant // 0.9159655941f;
MathExtension.ViswanathsConstant // 1.13198824f;
MathExtension.ConwaysConstant // 1.3035772690f;
MathExtension.MillsConstant // 1.3063778838f;
MathExtension.PlasticConstant // 1.3247179572f;
MathExtension.RamanujanSoldnerConstant // 1.4513692348f;
MathExtension.BackhouseConstant // 1.4560749485f;
MathExtension.PortersConstant // 1.4670780794f;
MathExtension.LiebsSquareIceConstant // 1.5396007178f;
MathExtension.ErdosBorweinConstant // 1.6066951524f;
MathExtension.NivensConstant // 1.7052111401f;
MathExtension.UniversalParabolicConstant // 2.2955871493f;
MathExtension.SierpinskisConstant // 2.5849817595f;
MathExtension.FransenRobinsonConstant // 2.807770f;
```
