using Base.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Base.Editor {
    public class JsComponentInfoParser {
        public static Dictionary<string, ComponentInfo> GetAllComponentInfo() {
            var compInfos = new Dictionary<string, ComponentInfo>();
            var filePaths = GetAllMonoFiles();
            foreach (var filePath in filePaths) {
                var compInfo = ParseJsComponentInfo(filePath);
                if (compInfo == null) {
                    continue;
                }
                compInfos[compInfo.name] = compInfo;
            }
            return compInfos;
        }

        private static string[] GetAllMonoFiles() {
            return Directory.GetFiles(targetPath, "*.ts", SearchOption.AllDirectories);
        }

        private static ComponentInfo ParseJsComponentInfo(string filePath) {
            var jsMonoString = File.ReadAllText(filePath);
            var compStringMatche = regexCompString.Match(jsMonoString);
            if (!compStringMatche.Success) {
                return null;
            }
            var compString = compStringMatche.ToString();
            var compNameMatch = regexCompName.Match(compString);
            if (!compNameMatch.Success) {
                return null;
            }
            var compInfo = new ComponentInfo();
            compInfo.name = compNameMatch.ToString();
            compInfo.tsFilePath = filePath;
            var compCategoryMatch = regexCompCategory.Match(compString);
            if (compCategoryMatch.Success) {
                compInfo.category = compCategoryMatch.ToString();
            }
            else {
                compInfo.category = string.Empty;
            }
            compInfo.properties = null;
            return compInfo;
        }

        public static Dictionary<string, PropertyInfo> ParseJsComponentProperties(string filePath) {
            var properties = new Dictionary<string, PropertyInfo>();
            var jsMonoString = File.ReadAllText(filePath);
            var propStringMatches = regexPropString.Matches(jsMonoString);
            for (var i = 0; i < propStringMatches.Count; ++i) {
                var propMatch = propStringMatches[i];
                var propString = propMatch.ToString();
                var propNameMatch = regexPropName.Match(propString);
                if (!propNameMatch.Success) {
                    continue;
                }
                var propTypeMatch = regexPropType.Match(propString);
                if (!propTypeMatch.Success) {
                    continue;
                }
                var propInfo = new PropertyInfo();
                propInfo.name = propNameMatch.ToString();
                var propDefine = new PropertyDefine(propTypeMatch.ToString());
                propInfo.editable = propDefine.editable;
                propInfo.type = propDefine.type;
                propInfo.isArray = propDefine.isArray;
                properties[propInfo.name] = propInfo;
            }
            return properties;
        }

        private static readonly string targetPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../TsProj/src"));

        // 完整的组件定义文本
        private static Regex regexCompString = new Regex(@"@component[\s\S]+?class\s+?\w+(?=\s+extends\s+Component)", RegexOptions.Multiline);
        // 组件分类文本
        private static Regex regexCompCategory = new Regex(@"(?<=@component\s*\([""''])[\w/]*?(?=[""''])", RegexOptions.Multiline);
        // 组件名文本
        private static Regex regexCompName = new Regex(@"(?<=class\s+)\w+", RegexOptions.Multiline);

        // 完整的属性定义文本
        private static Regex regexPropString = new Regex(@"@property[\s\S]+?;");
        // 匹配属性类型（json文本、数组类型、类型）
        private static Regex regexPropType = new Regex(@"(?<=@property\s*\(\s*){[\s\S]+?}|(?<=@property\s*\(\s*)[[\s\S]+?]|(?<=@property\s*\(\s*)[\w\.]+");
        // 匹配属性名
        private static Regex regexPropName = new Regex(@"\w+(?=\s*:\s*[\w\.\[\]]+\s*;)");

        private class PropertyDefine {
            public PropertyDefine() {

            }
            public PropertyDefine(string typeString) {
                if (typeString.StartsWith("{") && typeString.EndsWith("}")) {
                    try {
                        typeString = regexTypeString.Replace(typeString, evaluator);
                        var propType = JsonConvert.DeserializeObject<PropertyDefine>(typeString);
                        type = propType.type;
                        isArray = propType.isArray;
                        editable = propType.editable;
                    }
                    catch (Exception err) {
                        Debug.LogError(err);
                    }
                }
                else if (typeString.StartsWith("[") && typeString.EndsWith("]")) {
                    isArray = true;
                    editable = true;
                    type = typeString.Substring(1, typeString.Length - 2).Trim();
                }
                else {
                    isArray = false;
                    editable = true;
                    type = typeString;
                }
            }

            public string type;
            public bool isArray;
            public bool editable;

            // 匹配属性json中字义的类型
            private static readonly Regex regexTypeString = new Regex(@"(?<=type\s*:\s*)\[?\s*[\w\.]+\s*\]?(?=\s*,?)");
            private static readonly MatchEvaluator evaluator = new MatchEvaluator(AddQuotationMark);
            private static string AddQuotationMark(Match match) {
                var matchString = match.ToString();
                matchString = matchString.Trim('[', ']');
                matchString = matchString.Trim();
                return $"\"{matchString}\"";
            }
        }
    }
}