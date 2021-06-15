/**
 * 组件信息管理
 */
class ComponentInfoMgr {
    /**
     * 获取指定名字的组件信息JSON字符串，名字为空时返回所有组件信息
     * @param componentName 组件名字
     * @returns json格式的组件信息字符串
     */
    public getJsonString(componentName?: string): string {
        if (!componentName) {
            return JSON.stringify(this.componentInfos);
        }
        const componentInfo = this.componentInfos[componentName];
        if (!componentInfo) {
            return null;
        }
        return JSON.stringify(componentInfo);
    }

    /**
     * 获取指定名字的组件信息，名字不能为空
     * @param componentName 组件名
     * @returns 组件信息
     */
    public getComponentInfo(componentName: string): ComponentInfo {
        return this.componentInfos[componentName];
    }

    /**
     * 注册组件
     * @param name 组件名字
     * @param cls 组件类
     * @param path 组件在编辑器的inspector面板显示的索引路径
     */
    public registerComponent<T extends Constructor>(name: string, cls?: T, path?: string) {
        let compInfo = this.componentInfos[name];
        if (!compInfo) {
            compInfo = {
                name,
                path,
                properties: {}
            }
            this.componentInfos[name] = compInfo;
        }
        if (cls) {
            this.componentClass[name] = cls;
        }
        compInfo.path = compInfo.path || path;
    }

    /**
     * 向组件中添加组件的属性信息
     * @param component 组件名字
     * @param propInfo 组件信息
     */
    public addPropertyToComponent(component: string, propInfo: PropertyInfo) {
        this.registerComponent(component);
        const comp = this.componentInfos[component];
        comp.properties[propInfo.name] = propInfo;
    }

    /**
     * 查找组件构造函数
     * @param component 组件名
     * @returns 组件构造函数
     */
    public findComponentConstructor(component: string): Constructor {
        return this.componentClass[component];
    }

    private componentClass: { [key: string]: Constructor } = {};
    private componentInfos: ComponentInfos = {};
}

/**
 * 组件集合，由组件名索引
 */
 export type ComponentInfos = {
    [key: string]: ComponentInfo
};

/**
 * 组件信息
 */
export type ComponentInfo = {
    name: string,
    path: string,
    properties: { [key: string]: PropertyInfo },
};

/**
 * 组件的属性信息
 */
export type PropertyInfo = {
    name: string;
    type: string;
    isArray?: boolean;
    editable?: boolean;
}


export type Constructor = new (...args:any[]) => any;

export const compInfoMgr = new ComponentInfoMgr();
