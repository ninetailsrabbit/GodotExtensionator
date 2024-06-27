namespace Godot_XTension_Pack {
    public static class Localization {
        public enum LANGUAGES {
            ENGLISH,
            CZECH,
            DANISH,
            GERMAN,
            GREEK,
            ESPERANTO,
            SPANISH,
            FRENCH,
            INDONESIAN,
            ITALIAN,
            LATVIAN,
            POLISH,
            PORTUGUESE_BRAZILIAN,
            PORTUGUESE,
            RUSSIAN,
            CHINESE_SIMPLIFIED,
            CHINESE_TRADITIONAL,
            NORWEGIAN_BOKMAL,
            HUNGARIAN,
            ROMANIAN,
            KOREAN,
            TURKISH,
            JAPANESE,
            UKRAINIAN
        }

        public static readonly Dictionary<LANGUAGES, string[]> AvailableLanguages = new() {
        { LANGUAGES.ENGLISH, new string[] { "en", "en_US", "English", "English" } },
        { LANGUAGES.FRENCH, new string[] { "fr", "fr_FR", "Français", "French" } },
        { LANGUAGES.CZECH, new string[] { "cs", "cs_CZ", "Czech", "Czech" } },
        { LANGUAGES.DANISH, new string[] { "da", "da_DK", "Dansk", "Danish" } },
        { LANGUAGES.GERMAN, new string[] { "de", "de_DE", "Deutsch", "German" } },
        { LANGUAGES.GREEK, new string[] { "el", "el_GR", "Ελληνικά", "Greek" } },
        { LANGUAGES.ESPERANTO, new string[] { "eo", "eo_UY", "Esperanto", "Esperanto" } },
        { LANGUAGES.SPANISH, new string[] { "es", "es_ES", "Español", "Spanish" } },
        { LANGUAGES.INDONESIAN, new string[] { "id", "id_ID", "Indonesian", "Indonesian" } },
        { LANGUAGES.ITALIAN, new string[] { "it", "it_IT", "Italiano", "Italian" } },
        { LANGUAGES.LATVIAN, new string[] { "lv", "lv_LV", "Latvian", "Latvian" } },
        { LANGUAGES.POLISH, new string[] { "pl", "pl_PL", "Polski", "Polish" } },
        { LANGUAGES.PORTUGUESE_BRAZILIAN, new string[] { "pt_BR", "pt_BR", "Português Brasileiro", "Brazilian Portuguese" } },
        { LANGUAGES.PORTUGUESE, new string[] { "pt", "pt_PT", "Português", "Portuguese" } },
        { LANGUAGES.RUSSIAN, new string[] { "ru", "ru_RU", "Русский", "Russian" } },
        { LANGUAGES.CHINESE_SIMPLIFIED, new string[] { "zh_CN", "zh_CN", "简体中文", "Chinese Simplified" } },
        { LANGUAGES.CHINESE_TRADITIONAL, new string[] { "zh_TW", "zh_TW", "繁體中文", "Chinese Traditional" } },
        { LANGUAGES.NORWEGIAN_BOKMAL, new string[] { "nb", "nb_NO", "Norsk Bokmål", "Norwegian Bokmål" } },
        { LANGUAGES.HUNGARIAN, new string[] { "hu", "hu_HU", "Magyar", "Hungarian" } },
        { LANGUAGES.ROMANIAN, new string[] { "ro", "ro_RO", "Română", "Romanian" } },
        { LANGUAGES.KOREAN, new string[] { "ko", "ko_KR", "한국어", "Korean" } },
        { LANGUAGES.TURKISH, new string[] { "tr", "tr_TR", "Türkçe", "Turkish" } },
        { LANGUAGES.JAPANESE, new string[] { "ja", "ja_JP", "日本語", "Japanese" } },
        { LANGUAGES.UKRAINIAN, new string[] { "uk", "uk_UA", "Українська", "Ukrainian" } },
    };

        public static string[] English() => AvailableLanguages[LANGUAGES.ENGLISH];
        public static string[] French() => AvailableLanguages[LANGUAGES.FRENCH];
        public static string[] Czech() => AvailableLanguages[LANGUAGES.CZECH];
        public static string[] Danish() => AvailableLanguages[LANGUAGES.DANISH];
        public static string[] German() => AvailableLanguages[LANGUAGES.GERMAN];
        public static string[] Greek() => AvailableLanguages[LANGUAGES.GREEK];
        public static string[] Esperanto() => AvailableLanguages[LANGUAGES.ESPERANTO];
        public static string[] Spanish() => AvailableLanguages[LANGUAGES.SPANISH];
        public static string[] Indonesian() => AvailableLanguages[LANGUAGES.INDONESIAN];
        public static string[] Italian() => AvailableLanguages[LANGUAGES.ITALIAN];
        public static string[] Latvian() => AvailableLanguages[LANGUAGES.LATVIAN];
        public static string[] Polish() => AvailableLanguages[LANGUAGES.POLISH];
        public static string[] PortugueseBrazilian() => AvailableLanguages[LANGUAGES.PORTUGUESE_BRAZILIAN];
        public static string[] Russian() => AvailableLanguages[LANGUAGES.RUSSIAN];
        public static string[] ChineseSimplified() => AvailableLanguages[LANGUAGES.CHINESE_SIMPLIFIED];
        public static string[] ChineseTraditional() => AvailableLanguages[LANGUAGES.CHINESE_TRADITIONAL];
        public static string[] NorwegianBokmal() => AvailableLanguages[LANGUAGES.NORWEGIAN_BOKMAL];
        public static string[] Hungarian() => AvailableLanguages[LANGUAGES.HUNGARIAN];
        public static string[] Romanian() => AvailableLanguages[LANGUAGES.ROMANIAN];
        public static string[] Korean() => AvailableLanguages[LANGUAGES.KOREAN];
        public static string[] Turkish() => AvailableLanguages[LANGUAGES.TURKISH];
        public static string[] Japanese() => AvailableLanguages[LANGUAGES.JAPANESE];
        public static string[] Ukrainian() => AvailableLanguages[LANGUAGES.UKRAINIAN];
    }

}