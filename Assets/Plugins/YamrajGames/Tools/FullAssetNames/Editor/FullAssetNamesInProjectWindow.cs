#if UNITY_EDITOR
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace YamrajGames.EditorTools.FullAssetNames
{
    // Controls the custom rendering of asset names in the Project Window's Grid View.
    [InitializeOnLoad]
    public static class FullAssetNamesInProjectWindow
    {
        private static GUIStyle style;
        private static Color32 backgroundColor;
        private static Color32 selectedFrameColor;

        static FullAssetNamesInProjectWindow()
        {
            EditorApplication.projectWindowItemOnGUI -= Draw;
            EditorApplication.projectWindowItemOnGUI += Draw;

            // Subscribes to Play Mode changes to reset statics safely
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        // Ensures static UI variables are reset during Fast Play Mode transitions
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode || state == PlayModeStateChange.EnteredEditMode)
            {
                style = null;
            }
        }

        // Initializes and caches text styles lazily.
        private static void InitializeStyles()
        {
            style = new GUIStyle
            {
                fontSize = 10,
                margin = new RectOffset(0, 0, 0, 0),
            };

            if (EditorGUIUtility.isProSkin)
            {
                backgroundColor = new Color32(51, 51, 51, 255);
                selectedFrameColor = new Color32(44, 93, 135, 255);
            }
            else
            {
                backgroundColor = new Color32(190, 190, 190, 255);
                selectedFrameColor = new Color32(58, 114, 176, 255);
            }
        }

        // Intercepts the native GUI draw event to inject custom text rendering
        private static void Draw(string guid, Rect selectionRect)
        {
            // Lazy initialization
            if (style == null)
            {
                InitializeStyles();
            }

            if (!FullAssetNamesPreferences.PluginEnabled)
                return;

            if (!FullAssetNamesPreferences.EnableShowFullAssetName &&
                !FullAssetNamesPreferences.EnableCamelCaseSpacing &&
                !FullAssetNamesPreferences.ShowExtensions)
                return;

            if (selectionRect.height <= 20)
                return;

            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrWhiteSpace(path))
                return;

            var assetObject = AssetDatabase.LoadMainAssetAtPath(path);
            if (assetObject == null || IsRenaming(assetObject))
                return;

            string fileName = Path.GetFileNameWithoutExtension(path);
            string fileExtension = Path.GetExtension(path);

            if (FullAssetNamesPreferences.EnableCamelCaseSpacing)
            {
                fileName = FormatAssetName(fileName);
            }

            string finalName = FullAssetNamesPreferences.ShowExtensions ? fileName + fileExtension : fileName;
            GUIContent textContent = new GUIContent(finalName);

            if (FullAssetNamesPreferences.EnableShowFullAssetName)
            {
                style.wordWrap = true;
                style.clipping = TextClipping.Overflow;
                style.alignment = TextAnchor.UpperCenter;
            }
            else
            {
                style.wordWrap = false;
                style.clipping = TextClipping.Clip;
                if (style.CalcSize(textContent).x > selectionRect.width)
                    style.alignment = TextAnchor.UpperLeft;
                else
                    style.alignment = TextAnchor.UpperCenter;
            }

            float textHeight = FullAssetNamesPreferences.EnableShowFullAssetName
                ? style.CalcHeight(textContent, selectionRect.width)
                : 16f;

            var nameRect = new Rect(selectionRect.x, selectionRect.yMax - 12, selectionRect.width, textHeight + 4);

            if (Event.current.type == EventType.MouseDown && nameRect.Contains(Event.current.mousePosition))
            {
                Selection.activeObject = assetObject;
                Event.current.Use();
            }

            bool isSelected = Selection.Contains(assetObject);
            style.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : (isSelected ? Color.white : Color.black);

            var backgroundRect = new Rect(nameRect.x - 6, nameRect.y - 1, nameRect.width + 12, nameRect.height + 3);
            EditorGUI.DrawRect(backgroundRect, isSelected ? selectedFrameColor : backgroundColor);

            GUI.Label(nameRect, textContent, style);
        }

        // Prevents rendering overlap when the user is actively renaming an asset.
        private static bool IsRenaming(Object assetObject)
        {
            return Selection.activeObject == assetObject && EditorGUIUtility.editingTextField;
        }

        // Injects spaces into camelCase or PascalCase strings.
        private static string FormatAssetName(string text)
        {
            return Regex.Replace(text, "([a-z])([A-Z])", "$1 $2")
                        .Replace("_", " ")
                        .Replace("-", " ");
        }
    }
}
#endif