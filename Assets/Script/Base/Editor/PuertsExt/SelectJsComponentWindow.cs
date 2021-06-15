using System;
using System.Collections.Generic;
using Base.Runtime;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Base.Editor
{
    public class SelectJsComponentDropdown: AdvancedDropdown
    {
        public event Action<string> OnItemSelected;

        public SelectJsComponentDropdown(Dictionary<string, ComponentInfo> jsComponentInfos) : base(new AdvancedDropdownState())
        {
            componentInfos = jsComponentInfos;
            minimumSize = new Vector2(200, 300);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("Js Components");

            foreach (var componentInfo in componentInfos.Values)
            {
                if (string.IsNullOrEmpty(componentInfo.path))
                {
                    root.AddChild(new AdvancedDropdownItem(componentInfo.name));
                    continue;
                }

                var item = root;
                var folders = componentInfo.path.Split('/');
                foreach (var folder in folders)
                {
                    item = GetChildDropdownItem(item, folder);
                }
                item.AddChild(new AdvancedDropdownItem(componentInfo.name));
            }

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            OnItemSelected?.Invoke(item.name);
        }

        private AdvancedDropdownItem GetChildDropdownItem(AdvancedDropdownItem item, string childName)
        {
            foreach (var child in item.children)
            {
                if (child.name == childName)
                {
                    return child;
                }
            }
            var childItem = new AdvancedDropdownItem(childName);
            item.AddChild(childItem);
            return childItem;
        }

        private Dictionary<string, ComponentInfo> componentInfos;
    }
}