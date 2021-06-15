using Puerts;
using UnityEditor;
using UnityEngine;

namespace Base.Editor
{
    public class JsLoaderEditor : ILoader
    {
        public bool FileExists(string filePath)
        {
            var assetPath = filePath.StartsWith("puerts/")
                ? $"{puertsJsPath}/{filePath}.txt"
                : $"{appJsPath}/{filePath}.txt";
            var uuid = AssetDatabase.AssetPathToGUID(assetPath);
            return !string.IsNullOrEmpty(uuid);
        }

        public string ReadFile(string filePath, out string debugPath)
        {
            debugPath = filePath;
            if (filePath.StartsWith("puerts/"))
            {
                var asset = Resources.Load<TextAsset>(filePath);
                if (!asset)
                {
                    Debug.LogError($"JsLoaderEditor, file not found:{filePath}");
                }

                return asset.text;
            }

            var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>($"{appJsPath}/{filePath}.txt");
            if (!textAsset)
            {
                Debug.LogError($"JsLoaderEditor, file not found:{filePath}");
            }

            return textAsset.text;
        }

        private readonly string puertsJsPath = "Assets/ThirdParty/Puerts/Src/Resources";
        private readonly string appJsPath = "Assets/Res/JavaScript";
    }
}
