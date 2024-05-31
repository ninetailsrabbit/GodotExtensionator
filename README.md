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
    - [AudioStreamPlayer](#audiostreamplayer)
    - [Camera 3D](#camera-3d)
    - [Color](#color)
    - [Control](#control)
    - [Input](#input)
    - [Math](#math)
    - [String](#string)
    - [Mesh](#mesh)
    - [Node](#node)
    - [Node2D](#node2d)
    - [Node 3D](#node-3d)
    - [Scene Tree](#scene-tree)
    - [Viewport](#viewport)
    - [Vector](#vector)
    - [OSExtension](#osextension)
    - [Polygon2D](#polygon2d)
    - [Rect2](#rect2)
    - [Texture](#texture)
    - [Transform3D](#transform3d)
  - [Functionalities that can be used without an instance](#functionalities-that-can-be-used-without-an-instance)
    - [OSExtension](#osextension-1)
    - [Get autoload singleton on any place](#get-autoload-singleton-on-any-place)
    - [Getting random enum value](#getting-random-enum-value)
    - [Getting a random color](#getting-a-random-color)
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

# Using dotnet
dotnet add package Ninetailsrabbit.Godot_XTension_Pack --version 0.1.0
```

## Usage

Since these functions are C# extension methods, they become available directly on the types they extend. You only need to call them on instances of those types in your code, simplifying the syntax

### AudioStreamPlayer

These extension methods offer convenient ways to manipulate audio streams with features like pitch adjustment and ease curves. Their functionality is identical for both `AudioStreamPlayer2D` and `AudioStreamPlayer3D` nodes.

If an `AudioStreamPlayer` instance doesn't have an attached `AudioStream`, calling these extension methods will not produce any effect _(no playback or modification)_. Additionally, no errors will be thrown in this scenario.

```csharp
public partial class MyScene : Node {

  public AudioStreamPlayer audioPlayer;

  public override void _Ready() {
    audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

    // A pitch scale of 1.2f
    audioPlayer.PlayWithPitch(1.2f);
    // A random value from the range provided
    audioPlayer.PlayWithPitchRange(.9f, 1.2f);

    //Play the sound or music smoothly setting the duration until reach the player bus volume
    audioPlayer.PlayEase(1.5f);

    //First parameter is the duration in this case.
    audioPlayer.PlayEaseWithPitch(1.5f, 1.2f);
    audioPlayer.PlayEaseWithPitchRange(1.5f, .9f, 1.2f);

    //Shortcut to check if an audio player has an stream.
    audioPlayer.HasStream(); // true or false
  }

}

```

### Camera 3D

Mostly shorcuts to access common information when using 3D cameras

```csharp
public partial class MyScene : Node {

  public override void _Ready() {
    Camera3D camera = GetViewport().Camera3D();

    // / Gets the world-space origin of the ray projected from the center of the camera's viewport
    Vector3 center = camera.CenterByRayOrigin();

    // A shortcut for camera.GlobalTransform.Origin
    Vector3 center = camera.CenterByOrigin();

    // Retrieves the current forward direction where the camera is looking at
    // Useful for example to throw objects in the direction the camera is facing.
    Vector3 forwardDirection = camera.ForwardDirection();

    // If you want to detect whether an object is looking in the direction of the camera
    camera.IsFacingCamera(other3DNode); // true or false
  }

}
```

### Color

If you want to manage colours through code here are some functions that will be useful for you

```csharp
  var green = Colors.Green;
  var blue = Colors.Blue;

  // Returns the color in Vector3(r, g, b) format
  color.Vector3();

  // Returns the color with transparency value in Vector3(r, g, b, a) format
  color.Vector4();

  // Same as above but in HSV format Vector3(h, s, v)
  color.Vector3Hsv();
  color.Vector4Hsv(); // Vector3(h, s, v, a)

  //Checks if two colors are considered similar within a specified tolerance
  green.SimilarTo(blue, 95);

```

### Control

Simply extensions for nodes that inherit from `Control`

```csharp
public partial class MyUIScene : Control {

  public override void _Ready() {
      // Centers the current pivot offset
      this.CenterPivotOffset();

      // Change the mouse filters quickly
      this.IgnoreMouseEvents();
      this.StopMouseEvents();
      this.PassMouseEvents();
  }

}

```

### Input

Helpers to detect inputs in your game more easily and make them readable for humans.

```csharp
public override _Input(InputEvent @event) {

  // Detect if mouse button is clicked (one press)
  @event.IsMouseRightClick();
  @event.IsMouseLeftClick();

  // Detect if mouse button keeps pressed.
  @event.IsMouseRightButtonPressed();
  @event.IsMouseLeftButtonPressed();

  // Detect if the input was made on a gamepad.
  @event.IsControllerButton();
  @event.IsControllerAxis();
  @event.IsGamepadInput();

  // Detect if a numeric key is pressed (include numpad keys)
  @event.NumericKeyPressed();

  // Return the key on human readable format
  // Ctrl + X, Alt + Shift + 1 ...
  @event.ReadableKey()

  // Or You can do manually check or run directly from InputEvent
  if (@event is InputEventKey eventKey) {
    event.ReadableKey();
  }
}

```

### Math

Includes a lot of useful functions to speed up common calculations on game development. It uses the `Mathf` class from Godot so it's not full pure C#.

```csharp
// Normalize a float that represents an angle so it never goes beyond a full circle range.
720.NormalizeDegreesAngle(); // Becomes 360
12.56.NormalizeRadiansAngle() // Becomes TAU or 6.28..


// Syntactic sugar for check simple ranges. By default the min & max are inclusive
5.IsBetween(2, 6) // True
5.IsBetween(6, 2) // It works on the opposite way too
5.IsBetween(3, 5, false) // False, not inclusive
7.5f.IsBetween(7.6f, 12f) // False

// Convert an integer into his hexadecimal representation
13531865.Hexadecimal() //CE7AD9
"CE7AD9".DecimalFromHex() // This is part of StringExtension


// Separate a big number, the default separator it's a comma ","
1200000000.ThousandSeparator(".") // 1.200.000.000

// Round big numbers
1234567890.BigRound()  // Output: 1000000000
987654321.BigRound()  // Output: 987000000


// Applies a bias to a float value using a cubic function. It receives a factor between 0 and 1 and by adjusting the bias value, you can control for example, how much the dice is skewed towards higher numbers. A bias of 0.5 would result in a fair die roll. A bias closer to 1 would make it more likely to roll higher numbers

int diceSides = 6;
var randomValue = GD.Randf();
var biasedValue = randomValue.Bias(0.7f)

int rolledValue = Mathf.Floor(biasedValue * diceSides) + 1;

// The sigmoid function acts like a translator, taking any real number (x) as input and transforming it into a value between 0 and 1 (but never exactly 0 or 1). Think of it as a dimmer switch for number.

//The scaling factor allows you to adjust the steepness of the sigmoid curve, controlling how quickly it transitions between its output values of 0 and 1. This can be helpful for fine-tuning the behavior of the sigmoid function in different applications.

150.Sigmoid();
3500.Sigmoid(1.2f); // 1.2 scaling factor


// This function calculates the factorial of a given non-negative integer number and it uses a recursive approach
5.Factorial() // 120 --> 5! = 5 * 4 * 3 * 2 * 1

FactorialsFrom(5) // [1, 1, 2, 6, 24, 120]
/*
0! = 1 (by definition)
1! = 1 (by definition)
2! = 2 * 1 = 2
3! = 3 * 2 * 1 = 6
4! = 4 * 3 * 2 * 1 = 24
5! = 5 * 4 * 3 * 2 * 1 = 120
*/

FactorialsFrom(7) // [1, 1, 2, 6, 24, 120, 720, 5040]

// Conversions for Roman Numbers, only works for integers

// Integer to Roman Numeral Examples:
3.ToRomanNumber()  // III
47.ToRomanNumber()  // XLVII
1999.ToRomanNumber() // MCMXCIX
3549.ToRomanNumber() // MMMDXLIX
2024.ToRomanNumber() // MMXXIV

// Roman Numeral to Integer Examples (this is part from the StringExtension):
"III".RomanNumberToInteger() // 3
"XLVII".RomanNumberToInteger() // 47
"MCMXCIX".RomanNumberToInteger() // 1999
"MMMDXLIX".RomanNumberToInteger() // 3549
"MMXXIV".RomanNumberToInteger() // 2024

// Convert a number into its ordinal representation
// Useful for display position on leaderboards
1.ToOrdinal() // 1st
2.ToOrdinal() // 2nd
3.ToOrdinal() // 3rd
4.ToOrdinal() // 4th
21.ToOrdinal() //Output: 21st
32.ToOrdinal() //Output: 32nd
43.ToOrdinal() //Output: 43rd
54.ToOrdinal() //Output: 54th
101.ToOrdinal() //Output: 101st
111.ToOrdinal() //Output: 111th
212.ToOrdinal()

//Human readable format for big numbers
1234.56.PrettyNumber() // 1.2K
1234567.89.PrettyNumber() // 1.2M
1234567890.123.PrettyNumber() // 1.2B

// It allows to pass more suffixes in string[] format. They are sorted by exponent, so later suffixes are applied on larger exponents
1234567890.123.PrettyNumber(["", "K", "M", "BL", "T"]) // 1.2BL

// Binary string representation of a number
5.ToBinary() // "101"
13.ToBinary() // "1101"
255.ToBinary() // "11111111"

// Formatted seconds, A string representation of the formatted time in the format "MM:SS" or "MM:SS:mm", depending on the value of UseMilliseconds

// Without milliseconds
123.456.ToFormattedSeconds() // Result: "02:03"

// With milliseconds
123.456.ToFormattedSeconds(true) // Result: "02:03:45"
```

### String

This extensions provides helpful functions for manipulate or generate strings.

```csharp

// Detect if a url is valid
"https://google.es".IsValidUrl() // true
"http://api.local.dev".IsValidUrl() // true
"api.local.dev".IsValidUrl() // false

// Strip the BBCode from a text
"game [color=yellow]development[/color] it's hard".StripBBcode() // game development is hard

// Removes any text starting with "res://" followed by one or more non-space characters.
"res://assets/icons".StripGodotPath() // "assets/icons/"
"res ://  assets/icons".StripGodotPath() // "assets/icons/"

// Shortcuts for string.IsNullOrEmpty and string.IsNullOrWhitespace
"".IsNullOrEmpty() // true
" ".IsNullOrWhitespace() // true


// Check if the absolute file path is valid and the resource exists

"res://skills/resources/fire.tres".FilePathIsValid() // true
"resources/fire.tres".FilePathIsValid() // false, the path needs to be absolute

// Check if the absolute dir path is valid

"res://skills/resources".DirPathIsValid() // true
"resources".DirPathIsValid() // false, the path needs to be absolute

// Check if the directory exists on the godot executable path

"user://game/settings.ini".DirExistOnExecutablePath() // true if the executable is on "user://game" on the running computer.

// Shorcut to check equality ignoring case
".PNg".EqualsIgnoreCase(".png") // true
"Björk".EqualsCultureIgnoreCase("bjOrk") // true

```

### Mesh

Methods to shortcut some operations with meshes

```csharp

// If no Mesh shape is defined yet, a Vector3.Zero is returned.

public Vector3 RandomSurfacePosition(MeshInstance3D meshInstance) {

    // Returns a random point in the mesh surface
    return meshInstance.GetRandomSurfacePosition();

    // Can be used on Mesh class
    return meshInstance.Mesh.GetRandomSurfacePosition();
}

// Syntatic sugar to detect the mesh shape
meshInstance.HasMesh();
```

### Node

All the Godot nodes can use this extension methods, below you have the specific definitions for 2D and 3D ones.

```csharp
// Change the process mode easily with:
node.Enable()
node.Disable()
node.AlwaysProcess()
node.ProcessWhenPaused()

// Shortcut to get autoload nodes from the root

public partial class MySceneNode : Node {

  public GameGlobals GameGlobals;

  public override void _Ready() {
      GameGlobals = this.GetAutoloadNode<GameGlobals>();
      // You can pass a string name if the class name is not the same as the node name
      GameGlobals = this.GetAutoloadNode<GameGlobals>("RootGameGlobals");
  }
}

// Get children nodes recursively of custom class
var cells = node.GetNodesByClass<GridCell>();

// Get children nodes recursively of Godot type
var sprites = node.GetNodesByType<Sprite3D>();

// Get the first encountered node of custom class
node.FirstNodeOfClass<Board>();

// Get the first encountered node of Godot type
node.FirstNodeOfType<AnimatedSprite2D>();


// Retrieves the last child of this node or null
node.GetLastChild();

// Get all the ancestors from the node
node.GetAllAncestors();

// Get only the ancestors from the node
node.GetAllAncestors<Node2D>();

// Get all the childrens recursively from the node
node.GetAllChildren();

// Get all the childrens recursively of the type defined from the node
node.GetAllChildren<MeshInstance2D>();

//Check if a node is valid to apply operations on it.
node.IsValid();

// Remove a node safely
// Takes into account if it is not queued for deletion, if it is within the scene tree, etc.

node.Remove();

// Remove and queue free children, uses the above safely remove.
node.RemoveAndQueueFreeChildren();

// Queue free children, uses the above safely remove.
node.QueueFreeChildren();

// Shortcut to set owner to edited scene root, useful for tool scripts
node.SetOwnerToEditedSceneRoot();

// Get the current tree depth for the node
node.GetTreeDepth(); // 2 means that has 2 parents above until the root scene where it lives.

// Emit a signal safe only if exists without raising errors.
node.EmitSignalSafe();
```

### Node2D

```csharp
var sprite = new Sprite2D();
var anotherSprite = new Sprite2D();

// Get the current direction from this node to the mouse in the screen.
sprite.GetMouseDirection();

// Shortcuts for distance calculations
sprite.GlobalDistanceTo(anotherSprite);
sprite.LocalDistanceTo(anotherSprite);

// Shortcuts for direction calculations
sprite.GlobalDirectionTo(anotherSprite);
sprite.LocalDirectionTo(anotherSprite);

// Godot offers two types of z-index behavior:
//Relative: The z-index only affects the layering within its immediate parent node.

//Absolute: The z-index is considered relative to the entire scene root, making it independent of parent nodes.

//While individual nodes have their own z-index values, the function calculates the absolute z-index, which reflects the node's final position in the overall stacking order.
//By knowing the absolute z-index, you can ensure that specific nodes are always drawn on top of others, regardless of their parent-child relationships

// Get the absolute z-index for this node on the screen while the parent node has Z as relative.
sprite.GetAbsoluteZIndex();


// Get nearest node by distance

// These functions help you locate nodes within a specific distance range relative to a given point.
// Null is returned if no node was found in the selected distance

// Default distance range is between 0 and 9999
Node? nearestNode = sprite.GetNearestNodeByDistance([anotherSprite, Marker, OtherNode]);

// Define the distance range to check
Node? nearestNode = sprite.GetNearestNodeByDistance([anotherSprite, Marker, OtherNode], 30f, 1500f);

// Same to get farthest node
Node? nearestNode = sprite.GetFarthestNodeByDistance([anotherSprite, Marker, OtherNode]);
Node? nearestNode = sprite.GetFarthestNodeByDistance([anotherSprite, Marker, OtherNode], 30f, 1500f);

// Screen position in world coordinates
sprite.ScreenPosition() // Vector2(250, 100)

// Detects if the node is on the current visible in-game screen.
sprite.OnScreen()// true or false

// An optional margin (in pixels) to consider around the screen edges (default: 16.0f)
sprite.OnScreen(32f) // true or false

```

### Node 3D

Has available all the above `Node2D` methods except for the screen position ones.

```csharp
// You can detect if the current node 3D is facing another node

GetNode<MeshInstance3D>("MeshInTheWorld").IsFacing(otherNode3D);

```

### Scene Tree

```csharp
//The node extension actually uses the method from SceneTree to get autoload nodes so you can use this method from the tree.

public partial class MySceneNode : Node {

  public GameGlobals GameGlobals;

  public override void _Ready() {
      GameGlobals = this.GetTree().GetAutoloadNode<GameGlobals>();
      // You can pass a string name if the class name is not the same as the node name
      GameGlobals = this.GetTree().GetAutoloadNode<GameGlobals>("RootGameGlobals");
  }
}

//Shortcut to remove nodes safely from group
var tree = GetTree();

tree.RemoveNodesFromGroup("bullets");

// You can use this shorcut to await for the next frame to be processed
await tree.NextIdle // Process
await tree.NextPhysicsIdle //Physic Process


// Run a frame freeze effect in your game from the tree
tree.FrameFreeze(0.5f, 1.5f); // Slow on 0.5 time scale for 1.5 seconds

// Don't apply time scale changes on AudioServer
tree.FrameFreeze(0.5f, 1.5f, false);

// Gets the final transformation applied to the root node of the SceneTree, Useful to fix mouse handling when viewport it's stretched

tree.FinalTransform(); // Returns Transform2D

// Methods to quit, pause or resume the game.
tree.QuitGame();
tree.PauseGame();
tree.ResumeGame();
```

### Viewport

```csharp
public partial class MyScene: Node {

  public override void _Ready() {
      Viewport viewport = this.GetTree().GetViewport();

    //Gets the camera frame in 2D coordinates for the specified viewport
    // If no Camera2D exists, the visible rect from viewport is returned instead
      viewport.GetCamera2DFrame();

    // Converts a screen position (in normalized coordinates) to its corresponding world space position within the viewport
    Vector2 worldPosition = viewport.ScreenToWorld(new Vector2(200, 200));

    // Converts a world space position to its corresponding screen position (in normalized coordinates) within the viewport.
    viewport.WorldToScreen(worldPosition);

    // Screenshot related, this are similar to texture extensions
    viewport.Screenshot();
    viewport.ScreenshotToTextureRect();


    //Gets the current screen position of the mouse relative to the top-left corner of the Viewport.
    viewport.MouseScreenPosition();
    // This provides a position relative to the Viewport itself, independent of screen resolution, normalized between 0.0 and 1.0
    viewport.MouseScreenRelativePosition();

    //Gets the vector from the Viewport's center to the current mouse screen position
    viewport.MousePositionFromCenter();

    // Gets the mouse position relative to the Viewport's center, normalized between -1.0 and 1.0 on both axes
    viewport.MouseRelativeFromCenter();
  }
}
```

### Vector

This is the biggest one because in videogame development we are constantly working with vectors. Most of them works for both `Vector2` and `Vector3` unless otherwise defined.

```csharp
// Methods to get the opposite vector from the UpDirection of CharacterBody3D

Vector2.Right.UpDirectionOpposite() // Vector2.Left
Vector2.Up.UpDirectionOpposite() // Vector2.Down

Vector3.Right.UpDirectionOpposite() // Vector3.Left
Vector3.Forward.UpDirectionOpposite() // Vector3.Back

// It's useful for example to apply the gravity force depending where the up direction is pointing.
public void ApplyGravity(CharacterBody3D character, float force ) {
  character.Velocity += character.UpDirection.UpDirectionOpposite() * force * delta

  // You can use it from the character body also
  character.Velocity += character.UpDirectionOpposite() * force * delta
}

//  Limits the horizontal angle in radians of a Vector2 direction within a specified range.

// Imagine you have a character aiming a weapon with a limited horizontal firing range. This function can be used to ensure the aiming direction stays within the allowed range while preserving the vertical direction

Vector2 aiminDirection = Vector2(1, -0.5)  // Extracting direction from vector

float MaxHorizontalAngle = Mathf.PI / 3  // Limit angle to 60 degrees

Vector2 limitedDirection = MaxHorizontalAngle.LimitHorizontalAngle(aiminDirection);
// Output: (0.5, -0.866)


// Rotate vertically or horizontally a Vector3 with a random value
Vector3.One.RotateHorizontalRandom();
Vector3.One.RotateVerticalRandom();

// System numerics Vector2 to Godot.Vector2 and viceversa
System.Numerics.Vector2 numericVector = new Vector2(3, 5)
Godot.Vector2 godotVector = new Godot.Vector2(5, 5);

numericVector.ToGodotVector();
numericVector.ToGodotVectorI();

godotVector.ToNumericVector();

// System numerics Vector3 to Godot.Vector3 and viceversa
System.Numerics.Vector2 numericVector = new Vector2(3, 5, 8);
Godot.Vector2 godotVector = new Godot.Vector3(5, 5, 10);

numericVector.ToGodotVector();
numericVector.ToGodotVectorI();

godotVector.ToNumericVector();


// Flip a vector on a human-readable way
// This is exactly the same as prefix with -
var vector = Vector2.Right // (1, 0)
vector.Flip(); // (-1, 0)

var vector = Vector3.Up // (0, 1, 0)
vector.Flip() // (0, -1, 0)

// Reverse is also available
Vector2(3, 5).Reverse() // Vector2(5, 3)
Vector3(3, 5, 7).Reverse() // Vector2(7, 5, 3)

// Get a topdown Vector2 from a Vector3
// This function can be used to convert 3D positions of objects (represented by Vector3) to their corresponding 2D coordinates on the screen (represented by Vector2).
Vector3(100, 40, 150).GetTopdownVector(); // Vector2(100, 150)

// Extends a Vector in a specified direction by a given amount.
float jumpForce = 10f;

Vector3 jumpDirection = Vector3.Up.Extend(Mathf.Pi, jumpForce);

// Shorcut version to !IsZeroApprox()
new Vector2(0, 1).IsNotZeroApprox(); // true
Vector3.Zero.IsNotZeroApprox(); // false


//  Calculating the squared distance is computationally cheaper than calculating the actual distance using a square root operation. This can be beneficial for performance optimization when checking distances frequently

// While using squared distances offers a performance benefit, keep in mind that it doesn't give you the actual distance between the points. If you need the actual distance for calculations or other purposes, you'll need to perform a square root operation on the result

Vector2 from = new (3, 0),
Vector2 to = new(5, 2);

bool isOnDistance = from.IsWithinDistanceSquared(to, 5f); // true

Mathf.Sqrt(isOnDistance); // 2.8284271247461903


// TODO - CONTINUE WITH THE DOCUMENTATION
```

### OSExtension

General utilities that does not belongs to a particular place or uses `OS` singleton

```csharp
// Detect if the current build is running on a mobile
OSExtension.IsMobile();

// Detect if the current build is running on a SteamDeck
OSExtension.IsSteamDeck();

// Detect if multi threading is enabled on this project
OSExtension.IsMultithreadingEnabled();

// Open a external link if the url is valid. The url is encoded before opening it when web platform is detected
OSExtension.OpenExternalLink("https://github.com/ninetailsrabbit");

// Generate a random ID using the current unix time
OSExtension.GenerateRandomIdFromUnixTime();
```

### Polygon2D

Shortcuts to draw basic geometry shapes more quickly in a polygon.
The polygon points are cleared each time an extension function is called so only once shape can be draw each time.

```csharp

var polygon = new Polygon2D();

polygon.Circle();
polygon.PartialCircle();
polygon.Donut();
polygon.Rectangle();
polygon.RoundedRectangle();

```

### Rect2

Useful extensions to work with Rectangles

```csharp
var sprite = GetNode<Sprite2D>("Map");
var rect2 = sprite.texture.GetImage().GetUsedRect();

Vector2 mapPoint = rect2.RandomPoint();
```

### Texture

Useful methods to work with texture dimensions

```csharp
// Shortcut to get the Rect from a texture
sprite.texture.Dimensions();

// Gets the scaled dimensions of a TextureRect considering the texture's used rectangle and the scale applied.
var textureRect = new TextureRect(){Texture = "image.png"};
textureRect.Dimensions();

// This can used also on sprites
sprite.Dimensions();

// Get the real png Rect2I from png texture ignoring the transparent pixels. If the extension of the texture is not png, a Rect2 zero is returned.
textureRect.texture.GetPngRect();

```

### Transform3D

Get directions from a transform in a more human-readable way.

```csharp

var character = new CharacterBody3D();

// Access directions in the 3D space from a transform
Vector3 direction = character.GlobalTransform.Back()
Vector3 direction = character.GlobalTransform.Forward()
Vector3 direction = character.GlobalTransform.Left()
Vector3 direction = character.GlobalTransform.Right()
Vector3 direction = character.GlobalTransform.Up()
Vector3 direction = character.GlobalTransform.Down()

// Align directions easily on a Node3D
Transform3D newTransform = character.GlobalTransform.AlignUp()
Transform3D newTransform = character.GlobalTransform.AlignDown()
Transform3D newTransform = character.GlobalTransform.AlignRight()
Transform3D newTransform = character.GlobalTransform.AlignLeft()
Transform3D newTransform = character.GlobalTransform.AlignUp()

// Aligns the transform's rotation to face a target direction from a specified current direction. Useful for example to rotate smoothly a space ship

public class Spaceship : Node3D
{
    public Node3D Enemy; // Reference to the enemy Node3D

    public override void _Process(float delta)
    {
        if (Enemy is not null)
        {
        // Get current forward direction of the spaceship
        Vector3 currentDirection = Transform.basis.z;

        // Get direction from spaceship to enemy
        Vector3 targetDirection = this.GlobalDirectionTo(Enemy);

        // Align the spaceship towards the enemy
        Transform = this.Align(Transform, currentDirection, targetDirection);
        }
    }
}

// A more precise method to checks if two Transform3D objects are approximately equal in terms of their rotation and position

character.GlobalTransform.EqualTransformApprox(Enemy.GlobalTransform);


```

## Functionalities that can be used without an instance

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

#### Getting random enum value

```csharp
// Get a random value from the enum passed as parameter.
EnumExtension.RandomEnum(Input.MouseModeEnum);
```

#### Getting a random color

```csharp
//A random color that can receive alpha as parameter where 255 is no transparency and 0 fully transparent.
ColorExtension.RandomColor(255);
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
