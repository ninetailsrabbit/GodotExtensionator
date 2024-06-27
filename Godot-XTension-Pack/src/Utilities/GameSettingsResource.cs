using Godot;
using Godot.Collections;

namespace Godot_XTension_Pack {

    public partial class GameSettingsResource : Resource {

        public const string RESOLUTION_16_9 = "16:9";
        public const string RESOLUTION_4_3 = "4:3";
        public const string RESOLUTION_16_10 = "16:10";
        public const string RESOLUTION_21_9 = "21:9";


        #region Audio
        [Export]
        public Godot.Collections.Dictionary<string, float> AudioVolumes = new() {
        {"music", 1f },
        {"sfx", 1f },
        {"echosfx", 1f },
        {"voice", 1f },
        {"ui", 1f },
        {"ambient", 1f },
    };
        #endregion

        #region Gamepad

        [Export(PropertyHint.Range, "0.1f, 20f, 0.1f")] public float MouseSensitivity = 3f;
        [Export] public bool ControllerVibration = true;
        #endregion

        #region Analytics
        [Export] public bool AllowTelemetry = false;
        [Export] public Localization.LANGUAGES CurrentLanguage = Localization.LANGUAGES.ENGLISH;
        #endregion

        #region Graphics
        // Is intended for modify the value of the WorldEnviroment parameter 'tonemap_exposure'
        [Export(PropertyHint.Range, "0, 1f, 0.01f")] public float ScreenBrightness = 1f;
        //  Useful to know when to attenue flash bright or color effects on your game when this option is true.
        [Export] public bool PhotoSensitive = true;
        [Export] public bool ScreenShake = true;
        [Export] public DisplayServer.WindowMode DisplayMode = DisplayServer.WindowMode.Windowed;
        [Export] public DisplayServer.VSyncMode Vsync = DisplayServer.VSyncMode.Disabled;
        [Export] public Viewport.Msaa AntiaAliasing = Viewport.Msaa.Disabled;

        public Godot.Collections.Dictionary<string, Array<Vector2I>> Resolutions = new() {
          { RESOLUTION_16_9, new Array<Vector2I>() {
            new(320, 180),
            new(400, 224),
            new(640, 360),
            new(960, 540),
            new(1280, 720), // 720p
            new(1280, 800), // SteamDeck
            new(1366, 768),
            new(1600, 900),
            new(1920, 1080), // 1080p
            new(2560, 1440),
            new(3840, 2160),
          }},
          { RESOLUTION_4_3, new Array<Vector2I>() {
            new(512, 384),
            new(768, 576),
            new(1024, 768),
          }},
          { RESOLUTION_16_10, new Array<Vector2I>() {
            new(960, 600),
            new(1280, 800),
            new(1680, 1050),
            new(1920, 1200),
            new(2560, 1600),
          }},
          { RESOLUTION_21_9, new Array<Vector2I>() {
            new(1280, 540),
            new(1720, 720),
            new(2560, 1080),
            new(3440, 1440),
            new(3840, 2160), // 4K
            new(5120, 2880),
            new(7680, 4320), // 8K
          }},
        };


        // https://github.com/Calinou/godot-sponza/blob/master/scripts/settings_gui.gd
        public enum QUALITY_PRESETS {
            LOW, MEDIUM, HIGH, ULTRA
        }
        public QUALITY_PRESETS CurrentQualityPreset = QUALITY_PRESETS.MEDIUM;

        public System.Collections.Generic.Dictionary<QUALITY_PRESETS, GraphicQualityPreset> GraphicQualityPresets = new() {
        {
            QUALITY_PRESETS.LOW,
                new GraphicQualityPreset("For low-end PCs with integrated graphics, as well as mobile devices",
                new() {
                {"environment/glow_enabled", new GraphicQualityDisplay("Glow", 0, "Disabled")  },
                {"environment/ss_reflections_enabled", new GraphicQualityDisplay("SS Reflections", 0, "Disabled")},
                {"environment/ssao_enabled", new GraphicQualityDisplay("SSAO", 0, "Disabled")},
                {"rendering/anti_aliasing/quality/msaa_3d", new GraphicQualityDisplay("AntiAliasing 3D", (int)Viewport.Msaa.Disabled, "Disabled")},
            })
        },
        {
            QUALITY_PRESETS.MEDIUM,
                new GraphicQualityPreset("For mid-range PCs with slower dedicated graphics",
                new() {
                {"environment/glow_enabled", new GraphicQualityDisplay("Glow", 0, "Disabled")  },
                {"environment/ss_reflections_enabled", new GraphicQualityDisplay("SS Reflections", 0, "Disabled")},
                {"environment/ssao_enabled", new GraphicQualityDisplay("SSAO", 0, "Disabled")},
                {"rendering/anti_aliasing/quality/msaa_3d", new GraphicQualityDisplay("AntiAliasing 3D", (int)Viewport.Msaa.Msaa2X, "2×")},
            })
        },
         {
            QUALITY_PRESETS.HIGH,
                new GraphicQualityPreset("For recent PCs with mid-range dedicated graphics, or older PCs with high-end graphics",
                new() {
                {"environment/glow_enabled", new GraphicQualityDisplay("Glow", 1, "Enabled")  },
                {"environment/ss_reflections_enabled", new GraphicQualityDisplay("SS Reflections", 0, "Disabled")},
                {"environment/ssao_enabled", new GraphicQualityDisplay("SSAO", 1, "Medium-Quality")},
                {"rendering/anti_aliasing/quality/msaa_3d", new GraphicQualityDisplay("AntiAliasing 3D", (int)Viewport.Msaa.Msaa4X, "4×")},
            })
        },
          {
            QUALITY_PRESETS.ULTRA,
                new GraphicQualityPreset("For recent PCs with high-end dedicated graphics",
                new() {
                {"environment/glow_enabled", new GraphicQualityDisplay("Glow", 1, "Enabled")  },
                {"environment/ss_reflections_enabled", new GraphicQualityDisplay("SS Reflections", 1, "Enabled")},
                {"environment/ssao_enabled", new GraphicQualityDisplay("SSAO", 1, "High-Quality")},
                {"rendering/anti_aliasing/quality/msaa_3d", new GraphicQualityDisplay("AntiAliasing 3D", (int)Viewport.Msaa.Msaa8X, "8×")},
            })
        },

    };

        public readonly record struct GraphicQualityPreset(
            string Description,
            System.Collections.Generic.Dictionary<string, GraphicQualityDisplay> Settings
        );
        public readonly record struct GraphicQualityDisplay(string Name, int Enabled, string AvailableText);
        #endregion
    }

}