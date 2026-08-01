#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace YamrajGames.EditorTools.FullAssetNames
{
    // Creates a custom preferences menu to toggle tool features and manages local EditorPrefs.
    public class FullAssetNamesPreferences : SettingsProvider
    {
        private static readonly string PrefKey_PluginEnabled = "YamrajGames_FullAssetNames_Master";
        private static readonly string PrefKey_EnableShowFullAssetName = "YamrajGames_FullAssetNames_ShowFull";
        private static readonly string PrefKey_EnableCamelCaseSpacing = "YamrajGames_FullAssetNames_CamelCase";
        private static readonly string PrefKey_ShowExtensions = "YamrajGames_FullAssetNames_Extensions";

        public static bool PluginEnabled => EditorPrefs.GetBool(PrefKey_PluginEnabled, true);
        public static bool EnableShowFullAssetName => EditorPrefs.GetBool(PrefKey_EnableShowFullAssetName, true);
        public static bool EnableCamelCaseSpacing => EditorPrefs.GetBool(PrefKey_EnableCamelCaseSpacing, true);
        public static bool ShowExtensions => EditorPrefs.GetBool(PrefKey_ShowExtensions, false);

        public FullAssetNamesPreferences() : base("Preferences/Yamraj Games", SettingsScope.User) { }

        // Renders the settings UI inside the Unity Preferences window.
        public override void OnGUI(string searchContext)
        {
            float originalLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 220f;

            GUILayout.Label("Full Asset Names", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            bool currentPlugin = PluginEnabled;
            GUIContent pluginContent = new GUIContent("Enable Plugin", "Turns the entire Full Asset Names extension on or off.");
            bool newPlugin = EditorGUILayout.Toggle(pluginContent, currentPlugin);

            if (newPlugin != currentPlugin)
            {
                EditorPrefs.SetBool(PrefKey_PluginEnabled, newPlugin);
                EditorApplication.RepaintProjectWindow();
            }

            EditorGUILayout.Space();
            EditorGUI.BeginDisabledGroup(!newPlugin);

            bool currentFullAsset = EnableShowFullAssetName;
            GUIContent fullAssetContent = new GUIContent("Enable Show Full Asset Name", "Wraps long asset names into multiple lines instead of truncating them.");
            bool newFullAsset = EditorGUILayout.Toggle(fullAssetContent, currentFullAsset);

            if (newFullAsset != currentFullAsset)
            {
                EditorPrefs.SetBool(PrefKey_EnableShowFullAssetName, newFullAsset);
                EditorApplication.RepaintProjectWindow();
            }

            bool currentCamelCase = EnableCamelCaseSpacing;
            GUIContent camelCaseContent = new GUIContent("Enable Expanded Filenames", "Adds spaces to camelCase or PascalCase names for better readability.");
            bool newCamelCase = EditorGUILayout.Toggle(camelCaseContent, currentCamelCase);

            if (newCamelCase != currentCamelCase)
            {
                EditorPrefs.SetBool(PrefKey_EnableCamelCaseSpacing, newCamelCase);
                EditorApplication.RepaintProjectWindow();
            }

            bool currentExtensions = ShowExtensions;
            GUIContent extensionsContent = new GUIContent("Show File Extensions", "Displays the file extension (e.g., .cs, .prefab) at the end of the name.");
            bool newExtensions = EditorGUILayout.Toggle(extensionsContent, currentExtensions);

            if (newExtensions != currentExtensions)
            {
                EditorPrefs.SetBool(PrefKey_ShowExtensions, newExtensions);
                EditorApplication.RepaintProjectWindow();
            }

            EditorGUI.EndDisabledGroup();
            EditorGUIUtility.labelWidth = originalLabelWidth;
        }

        [SettingsProvider]
        public static SettingsProvider CreatePreferencesProvider()
        {
            return new FullAssetNamesPreferences();
        }
    }
}
#endif