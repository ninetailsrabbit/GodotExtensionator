# Godot XTensions Pack

![license](https://badgen.net/static/License/MIT/yellow)
[![readme](https://badgen.net/static/README/📃/yellow)](https://github.com/ninetailsrabbit/Godot-Xtension-Pack/README.md)
![csharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

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
      - [Random enum values](#random-enum-values)
      - [Common use characters](#common-use-characters)

## Getting started

Unlocks a new level of functionality for your Godot projects. This comprehensive collection of C# extensions seamlessly integrates with the existing Godot classes, empowering you to streamline development, enhance game mechanics, and unleash your creative vision. Whether you're a seasoned Godot developer or just starting out, `Godot-XTension-Pack` provides the tools you need to elevate your c# games to the next level.

### .csproj

Add the package directly into your .csproj

```xml


<ItemGroup>
## Latest stable release
  <PackageReference Include="Ninetailsrabbit.Godot_XTension_Pack"/>

## Manual version
  <PackageReference Include="Ninetailsrabbit.Godot_XTension_Pack" Version="0.1.0" />
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

#### Random enum values

```csharp
EnumExtension.RandomEnum(Input.MouseModeEnum);
```

#### Common use characters

```csharp
StringExtension.HEX_CHARACTERS // "0123456789ABCDEF";
StringExtension.ASCII_ALPHANUMERIC // "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
StringExtension.ASCII_LETTERS // "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
StringExtension.ASCII_LOWERCASE // "abcdefghijklmnopqrstuvwxyz";
StringExtension.ASCII_UPPERCASE // "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
StringExtension.ASCII_DIGITS // "0123456789";
StringExtension.ASCII_PUNCTUATION // "!\"#$%&'()*+, -./:;<=>?@[\\]^_`{|}~";
```
