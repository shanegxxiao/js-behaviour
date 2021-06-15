using Puerts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Base.Runtime
{
    public class JsLoaderRuntime : ILoader
    {
        public string ReadFile(string filePath, out string debugPath)
        {
            debugPath = filePath;
            if (filePath.StartsWith("puerts/"))
            {
                var asset = Resources.Load<TextAsset>(filePath);
                if (!asset)
                {
                    Debug.LogError($"JsEnvRuntime, file not found:{filePath}");
                }

                return asset.text;
            }

            var operation = Addressables.LoadAssetAsync<TextAsset>($"{appJsPath}/{filePath}.txt");
            operation.WaitForCompletion();
            if (!operation.Result)
            {
                Debug.LogError($"JsEnvRuntime, file not found:{filePath}");
            }

            return operation.Result.text;
        }

        public bool FileExists(string filePath)
        {
            return true;
        }

        private readonly string appJsPath = "Assets/Res/JavaScript";
    }
}
