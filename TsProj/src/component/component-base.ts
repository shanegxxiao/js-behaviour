import { Base } from "csharp";
import { ComponentInfo, PropertyInfo } from "./component-info-mgr";

/**
 * 组件接口，回调时机同unity的mono behaviour
 */
export abstract class Component {
    private BindProperty(instanceId: number): void {
        console.log(`instanceId = ${instanceId}`);
        this.behaviour = Base.Runtime.JsBehaviourMgr.Instance.Get(instanceId);
        this.compInfo = (this as any)["componentInfo"] as ComponentInfo;
        for (let propName in this.compInfo.properties) {
            const propInfo = this.compInfo.properties[propName];
            (this as any)[propInfo.name] = this.GetPropertyValue(propInfo);
        }
    }

    private UnbindProperty(): void {
        for (let propName in this.compInfo.properties) {
            const propInfo = this.compInfo.properties[propName];
            (this as any)[propInfo.name] = undefined;
        }
    }

    private GetPropertyValue(propInfo: PropertyInfo): any {
        switch (propInfo.type) {
            case "System.Single":
                return propInfo.isArray ?
                    this.ConvertTypedListToArray(this.behaviour.JsComponentProp.GetFloatList(propInfo.name)) :
                    this.behaviour.JsComponentProp.GetFloat(propInfo.name);
            case "System.String":
                return propInfo.isArray ?
                    this.ConvertTypedListToArray(this.behaviour.JsComponentProp.GetStringList(propInfo.name)) :
                    this.behaviour.JsComponentProp.GetString(propInfo.name);
            case "System.Int32":
            case "System.UInt32":
            case "System.Int16":
            case "System.UInt16":
                return propInfo.isArray ?
                    this.ConvertTypedListToArray(this.behaviour.JsComponentProp.GetIntList(propInfo.name)) :
                    this.behaviour.JsComponentProp.GetInt(propInfo.name);
            case "System.Int64":
            case "System.UInt64":
                return propInfo.isArray ?
                    this.ConvertTypedListToArray(this.behaviour.JsComponentProp.GetLongList(propInfo.name)) :
                    this.behaviour.JsComponentProp.GetLong(propInfo.name);
            case "UnityEngine.Vector2":
                return propInfo.isArray ?
                    this.ConvertTypedListToArray(this.behaviour.JsComponentProp.GetVector2List(propInfo.name)) :
                    this.behaviour.JsComponentProp.GetVector2(propInfo.name);
            case "UnityEngine.Vector3":
                return propInfo.isArray ?
                    this.ConvertTypedListToArray(this.behaviour.JsComponentProp.GetVector3List(propInfo.name)) :
                    this.behaviour.JsComponentProp.GetVector3(propInfo.name);
            default:
                if (this.behaviour.JsComponentProp.IsUEObject(propInfo.type)) {
                    return propInfo.isArray ?
                        this.ConvertTypedListToArray(this.behaviour.JsComponentProp.GetUEObjectList(propInfo.name)) :
                        this.behaviour.JsComponentProp.GetUEObject(propInfo.name);
                }
                return undefined;
        }
    }

    private ConvertTypedListToArray(list: any): any[] {
        let array: any[] = [];
        if (list?.Data == null) {
            return array;
        }
        for (let i = 0; i < list?.Data.Count; ++i) {
            array.push(list.Data.get_Item(i));
        }
        return array;
    }

    private compInfo: ComponentInfo;
    private behaviour: Base.Runtime.JsBehaviour;
}
