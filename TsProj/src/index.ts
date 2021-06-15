import { System, UnityEngine } from "csharp";
import { Component } from "./component/component-base";
import { component, property } from "./component/component-decoration";
export * from "./component/component-info-mgr";
export * from "./component/component-inst-mgr";

/**
 * 使用component修饰器定义TestBehaviour为Js组件
 */
@component()
class TestBehaviour extends Component {
    /**
     * 使用property修饰器定义需要在Inspector上显示的属性及其类型
     */
    @property(UnityEngine.GameObject)
    prop1: UnityEngine.GameObject;

    /**
     * editable未实现，仅演示功能扩展模式
     */
    @property({
        type: System.Single,
        editable: true
    })
    prop2: number;

    /**
     * 数组的几种定义形式
     */
    @property({
        type: UnityEngine.GameObject,
        isArray: true
    })
    prop3: UnityEngine.GameObject[];
    @property([UnityEngine.Vector3])
    prop4: UnityEngine.Vector3[];
    @property({
        type: [System.UInt32]
    })
    prop5: number[];

    @property(UnityEngine.Camera)
    prop6: UnityEngine.Camera;

    @property(UnityEngine.Vector3)
    prop7: UnityEngine.Vector3;

    public Awake() {
        console.log("Awake");
        console.log(`prop1 = ${this.prop1}`);
        console.log(`prop2 = ${this.prop2}`);
        console.log(`prop3 = ${this.prop3}`);
        console.log(`prop4 = ${this.prop4.length}`);
        console.log(`prop5 = ${this.prop5}`);
        console.log(`prop6 = ${this.prop6}`);
        console.log(`prop7 = {${this.prop7.x}, ${this.prop7.y}, ${this.prop7.z}}`);
    }
    
    public Start() {
        console.log("Start");
    }

    public OnEnable() {
        console.log("OnEnable");
    }

    public OnDisable() {
        console.log("OnDisable");
    }

    public OnDestroy() {
        console.log("OnDestroy");
    }
}

@component("User")
class UserBehaviour1 {
}
@component("User")
class UserBehaviour2 {
}
@component("User")
class UserBehaviour3 {
}

@component("System")
class SystemBehaviour1 {
}
@component("System")
class SystemBehaviour2 {
}
@component("System")
class SystemBehaviour3 {
}