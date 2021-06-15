import { $typeof } from "puerts";
import { compInfoMgr, Constructor, PropertyInfo } from "./component-info-mgr"

/**
 * 组件类修饰器工厂，应用在非静态类上
 * @param componentPath 组件在编辑器的inspector面板显示的索引路径 
 * @returns 组件修饰器
 */
export function component(componentPath?: string): Function {
    return function(constructor: Function) {
        compInfoMgr.registerComponent(constructor.name, constructor as Constructor, componentPath);
        constructor.prototype.componentInfo = compInfoMgr.getComponentInfo(constructor.name);
    }
}

/**
 * 组件属性修改器工厂，应用在组件类的非静态变量上
 * @param option 属性选项
 * @returns 组件属性修饰器
 */
export function property(option: PropertyOption): Function;
/**
 * 组件属性修改器工厂，应用在组件类的非静态变量上
 * @param type 属性类型
 * @returns 组件属性修饰器
 */
export function property<T extends Constructor>(type: T): Function;
/**
 * 组件属性修改器工厂，应用在组件类的非静态变量上
 * @param type 属性类型，传入类型数组，表示当前属性类型是数组
 * @returns 组件属性修饰器
 */
export function property<T extends Constructor>(type: T[]): Function;
export function property(arg: any): Function {
    return function(target: any, propertyKey: string) {
        let propInfo: PropertyInfo = null;
        if (typeof arg === 'function') {
            propInfo = {
                name: propertyKey,
                type: $typeof((arg as Constructor)).FullName,
                isArray: false,
            };
        }
        else if (Array.isArray(arg)) {
            propInfo = {
                name: propertyKey,
                type: $typeof((arg as Constructor[])[0]).FullName,
                isArray: true,
            };
        }
        else {
            const isArray = Array.isArray(arg.type);
            propInfo = {
                name: propertyKey,
                type: isArray ? $typeof((arg.type as Constructor[])[0]).FullName : $typeof((arg.type as Constructor)).FullName,
                isArray: (arg.isArray === null || arg.isArray === undefined) ? isArray : arg.isArray,
                editable: arg.editable
            };
        }
        compInfoMgr.addPropertyToComponent(target.constructor.name, propInfo);
    }
}

type PropertyOption = {
    type: Constructor | Constructor[];
    isArray?: boolean;
    editable?: boolean;
}

type ArrayElement<ArrayType extends readonly unknown[]> = 
  ArrayType extends readonly (infer ElementType)[] ? ElementType : never;