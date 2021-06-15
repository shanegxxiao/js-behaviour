import { Component } from "./component-base";
import { compInfoMgr } from "./component-info-mgr";

class ComponentInstMgr {
    /**
     * 给定一个组件类名，创建对应的实例
     * @param className 组件类名
     * @returns 返回实例ID
     */
    public newComponent(className: string): number {
        const componentCls = compInfoMgr.findComponentConstructor(className);
        if (!componentCls) {
            console.error(`cannot find component constructor ${className}.`);
            return InvalidComponentID;
        }
        const componentId = ++this.componentIdSeed;
        this.components[componentId] = new componentCls();
        return componentId;
    }

    /**
     * 删除一个组件实例
     * @param componentId 组件实例ID
     */
    public delComponent(componentId: number) {
        delete this.components[componentId];
    }

    /**
     * 获取组件实例上的一个方法名
     * @param componentId 组件实例ID
     * @param methodName 方法名
     * @returns 绑定组件实例后的方法
     */
    public getComponentMethod(componentId: number, methodName: string): Function {
        const componentInst = this.components[componentId];
        if (!componentInst) {
            console.error(`cannot find component with id ${componentId}.`);
            return undefined;
        }
        const method = (componentInst as any)[methodName] as Function;
        if (!method) {
            return undefined;
        }
        return method.bind(componentInst);
    }

    private componentIdSeed = 0;
    private components: { [key: number]: Component } = {};
}

export const InvalidComponentID = -1;

export const compInstMgr = new ComponentInstMgr;