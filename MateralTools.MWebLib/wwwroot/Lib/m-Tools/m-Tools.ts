"use strict";
interface String
{
    /**
     * 判断字符串是否为空字符串
     * @returns 是否为空字符串
     */
    MIsEmpty(): boolean;
    /**
     * 判断字符串是否为Null或Undefined或空字符串
     * @returns 是否为Null或Undefined或空字符串
     */
    MIsNullOrUndefinedOrEmpty(): boolean;
    /**
     * 移除左边空格
     * @returns 处理过的字符串
     */
    MTrimLeft(): string;
    /**
     * 移除左边空格
     * @returns 处理过的字符串
     */
    MTrimRight(): string;
    /**
     * 移除所有空格
     * @returns 处理过的字符串
     */
    MTrimAll(): string;
    /**
     * 移除多余空格(连续的空格将变成一个)
     * @returns 处理过的字符串
     */
    MSimplifyMultiSpaceToSingle(): string;
    /**
    * 左侧填充字符
    * @param length 位数
    * @param character 填充字符
    */
    MPadLeft(length: number, character: string): string;
    /**
    * 右侧填充字符
    * @param length 位数
    * @param character 填充字符
    */
    MPadRght(length: number, character: string): string;
}
String.prototype.MIsEmpty = function (): boolean
{
    return this === "";
}
String.prototype.MIsNullOrUndefinedOrEmpty = function (): boolean
{
    return this === null || this === undefined || (this as string).MIsEmpty();
}
String.prototype.MTrimLeft = function (): string
{
    let str = this as string;
    while (str.substr(0, 1) === " ")
    {
        str = str.substr(1, str.length - 1);
    }
    return str;
};
String.prototype.MTrimRight = function (): string
{
    let str = this as string;
    while (str.substr(str.length - 2, 1) === " ")
    {
        str = str.substr(0, str.length - 2);
    }
    return str;
};
String.prototype.MTrimAll = function (): string
{
    let str = this as string;
    return str.replace(/\s/g, "");
};
String.prototype.MSimplifyMultiSpaceToSingle = function (): string
{
    let str = this as string;
    return str.replace(/\s{2,}/g, " ");
};
if (!String.prototype.trim)
{
    String.prototype.trim = function (): string
    {
        return (this as string).MTrimLeft().MTrimRight();
    }
}
String.prototype.MPadLeft = function (length: number, character: string = " "): string
{
    let str = this as string;
    for (let i = str.length; i < length; i++)
    {
        str = character + str;
    }
    return str;
};
String.prototype.MPadRght = function (length: number, character: string = " "): string
{
    let str = this as string;
    for (let i = str.length; i < length; i++)
    {
        str = str + character;
    }
    return str;
};
interface Date
{
    /**
     * 获得时间差
     * @param targetDate 对比时间
     * @param TimeType 返回类型
     * @returns 时间差
     */
    MGetTimeDifference(targetDate: Date, timeType: MateralTools.TimeType): number;
    /**
     * 时间字符串格式化
     * @param formatStr 格式化字符串[y年|M月|d日|H时|m分|s秒|S毫秒|q季度]
     * @returns 格式化后的时间字符串
     */
    MDateTimeFormat(formatStr: string): string;
    /**
     * 获取Input dateTime设置值字符串
     * @param dateTime 要设置的时间
     * @returns 可以设置给Input的时间值
     */
    MGetInputDateTimeValueStr(): string;
    /**
     * 获取对应时区时间
     * @param timeZone 时区
     * @returns 对应时区时间
     */
    MGetDateByTimeZone(timeZone: number): Date;
    /**
     * 将其他时区时间转换为对应时区时间
     * @param timeZone 时区[null则自动为本地时区]
     * @returns 对应时区时间
     */
    MConvertTimeZone(timeZone: number): Date;
}
Date.prototype.MGetTimeDifference = function (targetDate: Date, timeType: MateralTools.TimeType = MateralTools.TimeType.Seconds): number
{
    let timeDifference: number = (this as Date).getTime() - targetDate.getTime();
    switch (timeType)
    {
        case MateralTools.TimeType.Day:
            timeDifference = Math.floor(timeDifference / (24 * 3600 * 1000));
            break;
        case MateralTools.TimeType.Hours:
            timeDifference = Math.floor(timeDifference / (3600 * 1000));
            break;
        case MateralTools.TimeType.Minutes:
            timeDifference = Math.floor(timeDifference / (60 * 1000));
            break;
        case MateralTools.TimeType.Seconds:
            timeDifference = Math.floor(timeDifference / 1000);
            break;
        case MateralTools.TimeType.Milliseconds:
            timeDifference = timeDifference;
            break;
        default:
    }
    return timeDifference;
};
Date.prototype.MDateTimeFormat = function (formatStr: string = "yyyy/MM/dd HH:mm:ss"): string
{
    let dateTime = this as Date;
    let formatData: Object = {
        "M+": dateTime.getMonth() + 1, //月份 
        "d+": dateTime.getDate(), //日 
        "H+": dateTime.getHours(), //小时 
        "m+": dateTime.getMinutes(), //分 
        "s+": dateTime.getSeconds(), //秒 
        "q+": Math.floor((dateTime.getMonth() + 3) / 3), //季度 
        "S": dateTime.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(formatStr))
    {
        formatStr = formatStr.replace(RegExp.$1, (dateTime.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var data in formatData)
    {
        if (new RegExp("(" + data + ")").test(formatStr))
        {
            formatStr = formatStr.replace(RegExp.$1, (RegExp.$1.length == 1) ? (formatData[data]) : (("00" + formatData[data]).substr(("" + formatData[data]).length)));
        }
    }
    return formatStr;
};
Date.prototype.MGetInputDateTimeValueStr = function (): string
{
    return (this as Date).MDateTimeFormat("yyyy-MM-ddTHH:mm:ss");
};
Date.prototype.MGetDateByTimeZone = function (timeZone: number = 8): Date
{
    let date = this as Date;
    var len = date.getTime();
    var offset = date.getTimezoneOffset() * 60000;
    var utcTime = len + offset;
    date = new Date(utcTime + 3600000 * timeZone);
    return date;
};
Date.prototype.MConvertTimeZone = function (timeZone: number = null): Date
{
    let date = this as Date;
    if (!timeZone)
    {
        timeZone = date.getTimezoneOffset() / 60;
    }
    date.setTime(date.getTime() - timeZone * 60 * 60 * 1000);
    return date;
};
interface Location
{
    /**
     * 获得URL参数
     * @returns URL参数
     */
    MGetURLParams(): Object;
    /**
     * 获得URL参数
     * @param key 键
     * @returns URL参数
     */
    MGetURLParam(key: string): string;
}
Location.prototype.MGetURLParams = function (): Object
{
    let params: Object = new Object();
    let paramsStr: string = window.location.search;
    let paramsStrs: string[] = new Array<string>();
    if (!paramsStr.MIsNullOrUndefinedOrEmpty())
    {
        paramsStr = paramsStr.substring(1, paramsStr.length);
        paramsStrs = paramsStr.split("&");
        for (let i = 0; i < paramsStrs.length; i++)
        {
            let temp = paramsStrs[i].split("=");
            if (temp.length == 2)
            {
                params[temp[0]] = temp[1];
            }
            else if (temp.length == 1)
            {
                params[temp[0]] = null;
            }
        }
    }
    return params;
};
Location.prototype.MGetURLParam = function (key: string): string
{
    let params = window.location.MGetURLParams();
    if (params[key])
    {
        return params["key"] as string;
    }
    else
    {
        return null;
    }
};
interface Document
{
    /**
     * 获取滚动条位置
     * @returns 滚动条位置
     */
    MGetScrollTop(): number;
    /**
     * 获取可见高度
     * @returns 可见高度
     */
    MGetClientHeight(): number;
    /**
     * 根据页面元素对象ID获得页面元素对象
     * @param element 页面元素
     * @returns 页面元素对象
     */
    $(element: string | HTMLElement | Element): HTMLElement;
}
document.MGetScrollTop = function (): number
{
    let scrollTop = 0;
    if (this.documentElement && this.documentElement.scrollTop)
    {
        scrollTop = this.documentElement.scrollTop;
    }
    else if (this.body)
    {
        scrollTop = this.body.scrollTop;
    }
    return scrollTop;
};
document.MGetClientHeight = function (): number
{
    let clientHeight = 0;
    if (this.body.clientHeight && this.documentElement.clientHeight)
    {
        clientHeight = (this.body.clientHeight < this.documentElement.clientHeight) ? this.body.clientHeight : this.documentElement.clientHeight;
    }
    else
    {
        clientHeight = (this.body.clientHeight > this.documentElement.clientHeight) ? this.body.clientHeight : this.documentElement.clientHeight;
    }
    return clientHeight;
};
document.$ = function (element: string | HTMLElement): HTMLElement
{
    if (MateralTools.ToolsManager.MGetType(element) === "string")
    {
        element = document.getElementById(element as string);
    }
    return element as HTMLElement;
};
interface Element
{
    /**
     * 设置样式
     * @param element 页面元素
     * @param className 要设置的样式列表
     */
    MSetClass(className: string | string[]);
    /**
     * 添加样式
     * @param className 要添加的样式
     */
    MAddClass(className: string | string[]);
    /**
     * 删除样式
     * @param className 要删除的样式
     */
    MRemoveClass(className: string | string[]);
    /**
     * 是否有拥有样式
     * @param className 要查找的样式列表
     * @returns 查询结果
     */
    MHasClass(className: string | string[]): boolean;
    /**
     * 根据ClassName获得元素对象
     * @param className ClassName
     * @returns Element集合
     */
    MGetElementsByClassName(className: string): Array<Element> | NodeListOf<Element>;
    /**
     * 根据Name获得元素对象
     * @param name Name
     * @returns Element集合
     */
    MGetElementsByName(name: string): Array<Element> | NodeListOf<Element>;
    /**
     * 获得子节点
     * @returns 子节点
     */
    MGetChildren(): HTMLCollection | Array<Node>;
    /**
     * 获得元素的实际样式
     * @returns 实际样式
     */
    MGetComputedStyle(): CSSStyleDeclaration;
}
Element.prototype.MSetClass = function (className: string | string[])
{
    let element = this as Element;
    let classStr: string = "";
    let TypeStr: string = MateralTools.ToolsManager.MGetType(className);
    let ClassList: string[];
    classStr = TypeStr === "Array" ? (className as string[]).join(" ") : className as string;
    if (!classStr.MIsNullOrUndefinedOrEmpty())
    {
        classStr = classStr.MSimplifyMultiSpaceToSingle().trim();
        element.setAttribute("class", classStr);
    }
    else
    {
        element.removeAttribute("class");
    }
};
Element.prototype.MAddClass = function (className: string | string[])
{
    let element = this as Element;
    if (MateralTools.ToolsManager.MGetType(className) === "string")
    {
        className = (className as string).split(" ");
    }
    for (var i = 0; i < className.length; i++)
    {
        element.classList.add(className[i]);
    }
};
Element.prototype.MRemoveClass = function (className: string | string[])
{
    let element = this as Element;
    if (MateralTools.ToolsManager.MGetType(className) === "string")
    {
        className = (className as string).split(" ");
    }
    for (var i = 0; i < className.length; i++)
    {
        element.classList.remove(className[i]);
    }
};
Element.prototype.MHasClass = function (className: string | string[]): boolean
{
    let element = this as Element;
    let resM = true;
    if (MateralTools.ToolsManager.MGetType(className) === "string")
    {
        className = (className as string).split(" ");
    }
    for (var i = 0; i < className.length && resM; i++)
    {
        resM = element.classList.contains(className[i]);
    }
    return resM;
};
Element.prototype.MGetElementsByClassName = function (className: string): Array<Element> | NodeListOf<Element>
{
    let element = this as Element;
    className = className.trim();
    let resultM: Array<Element> = new Array<Element>();
    let elements: NodeListOf<Element>;
    if (element.getElementsByClassName)
    {
        elements = element.getElementsByClassName(className);
        return elements;
    }
    else
    {
        elements = element.getElementsByTagName("*");
        for (let i = 0; i < elements.length; i++)
        {
            if (elements[i].MHasClass(className))
            {
                resultM.push(elements[i]);
            }
        }
        return resultM;
    }
};
Element.prototype.MGetElementsByName = function (name: string): Array<Element>
{
    let element = this as Element;
    name = name.trim();
    let resultM: Array<Element> = new Array<Element>();
    let elements: NodeListOf<Element> = element.getElementsByTagName("*");
    for (let i = 0; i < elements.length; i++)
    {
        if ((elements[i] as any).name && (elements[i] as any).name == name)
        {
            resultM.push(elements[i]);
        }
    }
    return resultM;
};
if (!Element.prototype.addEventListener)
{
    Element.prototype.addEventListener = function <K extends keyof ElementEventMap>(type: K, listener: (this: Element, ev: ElementEventMap[K]) => any, options?: boolean | AddEventListenerOptions)
    {
        Element["attachEvent"]("on" + type, listener);
    }
}
Element.prototype.MGetChildren = function (): HTMLCollection | Array<Node>
{
    let children: HTMLCollection | Array<Node>;
    let element = this as HTMLElement;
    if (element.children)
    {
        children = element.children;
    }
    else
    {
        children = new Array<Node>();
        let length = element.childNodes.length;
        for (let i = 0; i < length; i++)
        {
            if (element.childNodes[i].nodeType == 1)
            {
                (children as Array<Node>).push(element.childNodes[i]);
            }
        }
    }
    return children;
};
Element.prototype.MGetComputedStyle = function (): CSSStyleDeclaration
{
    let element = this as Element;
    let cssStyle: CSSStyleDeclaration;
    if (element["currentStyle"])
    {
        cssStyle = element["currentStyle"];
    }
    else
    {
        cssStyle = getComputedStyle(element);
    }
    return cssStyle;
};
interface HTMLElement
{
    /**
     * 获得自定义属性
     * @returns 自定义属性
     */
    MGetDataSet(): DOMStringMap | Object;
}
HTMLElement.prototype.MGetDataSet = function (): DOMStringMap | Object
{
    let DataSet: DOMStringMap | Object;
    let element = this as HTMLElement;
    if (element.dataset)
    {
        DataSet = element.dataset;
    }
    else
    {
        DataSet = new Object();
        let length: number = element.attributes.length;
        let item: any;
        for (let i = 0; i < length; i++)
        {
            item = element.attributes[i];
            if (item.specified && /^data-/.test(item.nodeName))
            {
                DataSet[item.nodeName.substring(5)] = item.nodeValue;
            }
        }
    }
    return DataSet;
}
interface Event
{
    /**
     * 获得事件触发元素
     * @returns 触发元素 
     */
    MGetEventTarget(): Element | EventTarget;
}
Event.prototype.MGetEventTarget = function (): Element | EventTarget
{
    let event = this as Event;
    return event["target"] || event["srcElement"];
}
if (!window.addEventListener)
{
    window.addEventListener = function <K extends keyof WindowEventMap>(type: K, listener: (this: Window, ev: WindowEventMap[K]) => any, options?: boolean | AddEventListenerOptions)
    {
        window["attachEvent"]("on" + type, listener);
    }
}
interface Array<T>
{
    /**
     * 清空数组
     * @returns 清空后的数组
     */
    MClear<T>(): Array<T>;
    /**
     * 插入数组
     * @param index 要插入的对象
     * @returns 插入后的数组
     */
    MInsert<T>(item: T, index: number): Array<T>;
    /**
     * 删除数组
     * @param index 要删除的位序
     * @returns 删除后的数组
     */
    MRemoveTo<T>(index: number): Array<T>;
    /**
     * 删除数组
     * @param item 要删除的对象
     * @returns 删除后的数组
     */
    MRemove<T>(item: T): Array<T>;
    /**
     * 删除所有数组
     * @param item 要删除的对象
     * @returns 删除后的数组
     */
    MRomeveAll<T>(item: T): Array<T>;
}
if (!Array.prototype.indexOf)
{
    Array.prototype.indexOf = function (searchElement: any, formIndex: number = 0): number
    {
        let array = this as Array<any>;
        for (let i = formIndex; i < array.length; i++)
        {
            if (array[i] == searchElement)
            {
                return i;
            }
        }
        return -1;
    }
}
Array.prototype.MClear = function <T>(): Array<T>
{
    let array = this as Array<T>;
    array.splice(0, array.length);
    return array;
};
Array.prototype.MInsert = function <T>(item: T, index: number): Array<T>
{
    let array = this as Array<T>;
    array.splice(index, 0, item);
    return array;
};
Array.prototype.MRemoveTo = function <T>(index: number): Array<T>
{
    let array = this as Array<T>;
    let count = array.length;
    array.splice(index, 1);
    if (count === array.length && count === 1)
    {
        array = [];
    }
    return array;
};
Array.prototype.MRemove = function <T>(item: T): Array<T>
{
    let array = this as Array<T>;
    let index: number = array.indexOf(item);
    if (index >= 0)
    {
        array = array.MRemoveTo(index);
    }
    return array;
};
Array.prototype.MRomeveAll = function <T>(item: T): Array<T>
{
    let array = this as Array<T>;
    let index: number = array.indexOf(item);
    while (index >= 0)
    {
        array = array.MRemoveTo(index);
        index = array.indexOf(item);
    }
    return array;
};
if (!JSON.parse)
{
    JSON.parse = function (jsonStr: string): any
    {
        return eval("(" + jsonStr + ")");
    }
}
if (!JSON.stringify)
{
    JSON.stringify = function (jsonObj: any): string
    {
        let resM: string = "";
        let IsArray: boolean;
        let TypeStr: string;
        for (let key in jsonObj)
        {
            IsArray = false;
            TypeStr = MateralTools.ToolsManager.MGetType(jsonObj[key]);
            if (jsonObj instanceof Array)
            {
                IsArray = true;
            }
            if (TypeStr == "string")
            {
                if (IsArray)
                {
                    resM += "\"" + jsonObj[key].toString() + "\",";
                }
                else
                {
                    resM += "\"" + key + "\":\"" + jsonObj[key].toString() + "\",";
                }
            }
            else if (jsonObj[key] instanceof RegExp)
            {
                if (IsArray)
                {
                    resM += jsonObj[key].toString() + ",";
                }
                else
                {
                    resM += "\"" + key + "\":\"" + jsonObj[key].toString() + "\",";
                }
            }
            else if (jsonObj[key] instanceof Array)
            {
                resM += "\"" + key + "\":" + this.JSONStringify(jsonObj[key]) + ",";
            }
            else if (TypeStr == "boolean")
            {
                if (IsArray)
                {
                    resM += jsonObj[key].toString() + ",";
                }
                else
                {
                    resM += "\"" + key + "\":" + jsonObj[key].toString() + ",";
                }
            }
            else if (TypeStr == "number")
            {
                if (IsArray)
                {
                    resM += jsonObj[key].toString() + ",";
                }
                else
                {
                    resM += "\"" + key + "\":" + jsonObj[key].toString() + ",";
                }
            }
            else if (jsonObj[key] instanceof Object)
            {
                if (IsArray)
                {
                    resM += this.JSONStringify(jsonObj[key]) + ",";
                }
                else
                {
                    resM += "\"" + key + "\":" + this.JSONStringify(jsonObj[key]) + ",";
                }
            }
            else if (!jsonObj[key] || jsonObj[key] instanceof Function)
            {
                if (IsArray)
                {
                    resM += "null,";
                }
                else
                {
                    resM += "\"" + key + "\":null,";
                }
            }
        }
        if (IsArray)
        {
            resM = "[" + resM.slice(0, -1) + "]";
        }
        else
        {
            resM = "{" + resM.slice(0, -1) + "}";
        }
        return resM;
    }
}
interface Math
{
    /**
     * 返回一个随机数
     * @param min 最小值
     * @param max 最大值
     * @returns 随机数
     */
    GetRandom(min: number, max: number): number;
    /**
     * 获取四边形的外接圆半径
     * @param length 长
     * @param width 宽
     * @param IsRound 是圆形
     */
    GetCircumcircleRadius(length: number, width: number, IsRound: boolean): number;
}
Math.GetRandom = function (min: number, max: number): number
{
    return Math.floor(Math.random() * max + min);
};
Math.GetCircumcircleRadius = function (length: number, width: number = length, IsRound: boolean = true): number
{
    let max: number = Math.max(length, width);
    //正方形的对角线=边长^2*2
    let diameter: number = Math.sqrt(Math.pow(max, 2) * 2);
    //外接圆的直径=正方形的对角线
    //圆的半径=直径/2
    let radius: number = diameter / 2;
    if (IsRound)
    {
        radius = Math.round(radius);
    }
    return radius;
}
namespace MateralTools
{
    /**
     * 对象帮助类
     */
    export class ToolsManager
    {
        /**
         * 获得对象类型
         * @param obj 传入对象
         * @param IncludeCustom 包括自定义类型
         * @returns 对象类型 
         */
        public static MGetType(obj: any, IncludeCustom: boolean = true): string
        {
            let resStr: string = typeof obj;
            if (resStr === "object")
            {
                if (obj === null)
                {
                    resStr = "null";
                }
                else
                {
                    resStr = Object.prototype.toString.call(obj).slice(8, -1);
                    if (resStr === "Object" && obj.constructor && obj.constructor.name != "Object" && IncludeCustom)
                    {
                        resStr = obj.constructor.name;
                    }
                }
            }
            return resStr.toLowerCase();
        }
        /**
         * 克隆对象
         * @param obj 要克隆的对象
         */
        public static Clone(obj: any): any
        {
            let ObjectType: string = MateralTools.ToolsManager.MGetType(obj, false);
            let result: any;
            if (ObjectType == "Object")
            {
                result = new Object();
            }
            else if (ObjectType == "array")
            {
                result = new Array();
            }
            else
            {
                result = obj;
            }
            for (var i in obj)
            {
                let copy = obj[i];
                let SubObjectType: string = MateralTools.ToolsManager.MGetType(copy, false);
                if (SubObjectType == "Object" || SubObjectType == "array")
                {
                    result[i] = arguments.callee(copy);
                }
                else
                {
                    result[i] = copy;
                }
            }
            return result;
        }
    }
    /*
     * 时间类型
     */
    export enum TimeType
    {
        /**
         * 年
         */
        Years = 0,
        /**
         * 月
         */
        Months = 1,
        /**
         * 日
         */
        Day = 2,
        /**
         * 时
         */
        Hours = 3,
        /**
         * 分
         */
        Minutes = 4,
        /**
         * 秒
         */
        Seconds = 5,
        /**
         * 毫秒
         */
        Milliseconds = 6
    }
    /**
     * 返回对象类型
     */
    export enum MResultType
    {
        /**
         * 成功
         */
        Success = 0,
        /**
         * 失败
         */
        Fail = 1,
        /**
         * 错误
         */
        Error = 2
    }
    /**
     * 分页模型
     */
    export class MPageModel
    {
        /**
         * 查询页数
         */
        public PageIndex: number;
        /**
         * 每页显示数量
         */
        public PageSize: number;
        /**
         * 总页数
         */
        public PageCount: number
        /**
         * 数据总数
         */
        public DataCount: number;
    }
    /**
     * 返回模型
     */
    export class MResultModel<T> {
        /**
         * 对象类型
         */
        public ResultType: MResultType;
        /**
         * 返回消息
         */
        public Message: string;
        /**
         * 携带数据
         */
        public Data: T;
    }
    /**
     * 携带分页数据的返回对象
     */
    export class MResultPageModel<T> extends MResultModel<T>{
        /**
         * 分页信息
         */
        public PageInfo: MPageModel;
    }
    /**
     * 加密帮助类
     */
    export class EncryptionManager
    {
        /**
         * 获取32位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        public static Get32MD5Str(str: string, isLower: boolean = false): string
        {
            function l(a)
            {
                return h(g(o(a), a.length * 8));
            }
            function m(e)
            {
                let b = "0123456789ABCDEF";
                if (isLower === true)
                {
                    b = b.toLowerCase();
                }
                let c = "";
                let d;
                for (let a = 0; a < e.length; a++)
                {
                    d = e.charCodeAt(a);
                    c += b.charAt(d >>> 4 & 15) + b.charAt(d & 15);
                }
                return c;
            }
            function n(d)
            {
                let b = "";
                let c = -1;
                let a, e;
                while (++c < d.length)
                {
                    a = d.charCodeAt(c),
                        e = c + 1 < d.length ? d.charCodeAt(c + 1) : 0;
                    55296 <= a && a <= 56319 && 56320 <= e && e <= 57343 && (a = 65536 + ((a & 1023) << 10) + (e & 1023), c++);
                    a <= 127 ? b += String.fromCharCode(a) : a <= 2047 ? b += String.fromCharCode(192 | a >>> 6 & 31, 128 | a & 63) : a <= 65535 ? b += String.fromCharCode(224 | a >>> 12 & 15, 128 | a >>> 6 & 63, 128 | a & 63) : a <= 2097151 && (b += String.fromCharCode(240 | a >>> 18 & 7, 128 | a >>> 12 & 63, 128 | a >>> 6 & 63, 128 | a & 63));
                }
                return b;
            }
            function o(c)
            {
                let b = Array(c.length >> 2);
                for (let a = 0; a < b.length; a++)
                {
                    b[a] = 0;
                }
                for (let a = 0; a < c.length * 8; a += 8)
                {
                    b[a >> 5] |= (c.charCodeAt(a / 8) & 255) << a % 32;
                }
                return b;
            }
            function h(c)
            {
                let b = "";
                for (let a = 0; a < c.length * 32; a += 8)
                {
                    b += String.fromCharCode(c[a >> 5] >>> a % 32 & 255);
                }
                return b;
            }
            function g(j, l)
            {
                j[l >> 5] |= 128 << l % 32, j[(l + 64 >>> 9 << 4) + 14] = l;
                let g = 1732584193;
                let h = -271733879;
                let i = -1732584194;
                let f = 271733878;
                for (let k = 0; k < j.length; k += 16)
                {
                    let n = g;
                    let o = h;
                    let p = i;
                    let m = f;
                    g = a(g, h, i, f, j[k + 0], 7, -680876936);
                    f = a(f, g, h, i, j[k + 1], 12, -389564586);
                    i = a(i, f, g, h, j[k + 2], 17, 606105819);
                    h = a(h, i, f, g, j[k + 3], 22, -1044525330);
                    g = a(g, h, i, f, j[k + 4], 7, -176418897);
                    f = a(f, g, h, i, j[k + 5], 12, 1200080426);
                    i = a(i, f, g, h, j[k + 6], 17, -1473231341);
                    h = a(h, i, f, g, j[k + 7], 22, -45705983);
                    g = a(g, h, i, f, j[k + 8], 7, 1770035416);
                    f = a(f, g, h, i, j[k + 9], 12, -1958414417);
                    i = a(i, f, g, h, j[k + 10], 17, -42063);
                    h = a(h, i, f, g, j[k + 11], 22, -1990404162);
                    g = a(g, h, i, f, j[k + 12], 7, 1804603682);
                    f = a(f, g, h, i, j[k + 13], 12, -40341101);
                    i = a(i, f, g, h, j[k + 14], 17, -1502002290);
                    h = a(h, i, f, g, j[k + 15], 22, 1236535329);
                    g = b(g, h, i, f, j[k + 1], 5, -165796510);
                    f = b(f, g, h, i, j[k + 6], 9, -1069501632);
                    i = b(i, f, g, h, j[k + 11], 14, 643717713);
                    h = b(h, i, f, g, j[k + 0], 20, -373897302);
                    g = b(g, h, i, f, j[k + 5], 5, -701558691);
                    f = b(f, g, h, i, j[k + 10], 9, 38016083);
                    i = b(i, f, g, h, j[k + 15], 14, -660478335);
                    h = b(h, i, f, g, j[k + 4], 20, -405537848);
                    g = b(g, h, i, f, j[k + 9], 5, 568446438);
                    f = b(f, g, h, i, j[k + 14], 9, -1019803690);
                    i = b(i, f, g, h, j[k + 3], 14, -187363961);
                    h = b(h, i, f, g, j[k + 8], 20, 1163531501);
                    g = b(g, h, i, f, j[k + 13], 5, -1444681467);
                    f = b(f, g, h, i, j[k + 2], 9, -51403784);
                    i = b(i, f, g, h, j[k + 7], 14, 1735328473);
                    h = b(h, i, f, g, j[k + 12], 20, -1926607734);
                    g = c(g, h, i, f, j[k + 5], 4, -378558);
                    f = c(f, g, h, i, j[k + 8], 11, -2022574463);
                    i = c(i, f, g, h, j[k + 11], 16, 1839030562);
                    h = c(h, i, f, g, j[k + 14], 23, -35309556);
                    g = c(g, h, i, f, j[k + 1], 4, -1530992060);
                    f = c(f, g, h, i, j[k + 4], 11, 1272893353);
                    i = c(i, f, g, h, j[k + 7], 16, -155497632);
                    h = c(h, i, f, g, j[k + 10], 23, -1094730640);
                    g = c(g, h, i, f, j[k + 13], 4, 681279174);
                    f = c(f, g, h, i, j[k + 0], 11, -358537222);
                    i = c(i, f, g, h, j[k + 3], 16, -722521979);
                    h = c(h, i, f, g, j[k + 6], 23, 76029189);
                    g = c(g, h, i, f, j[k + 9], 4, -640364487);
                    f = c(f, g, h, i, j[k + 12], 11, -421815835);
                    i = c(i, f, g, h, j[k + 15], 16, 530742520);
                    h = c(h, i, f, g, j[k + 2], 23, -995338651);
                    g = d(g, h, i, f, j[k + 0], 6, -198630844);
                    f = d(f, g, h, i, j[k + 7], 10, 1126891415);
                    i = d(i, f, g, h, j[k + 14], 15, -1416354905);
                    h = d(h, i, f, g, j[k + 5], 21, -57434055);
                    g = d(g, h, i, f, j[k + 12], 6, 1700485571);
                    f = d(f, g, h, i, j[k + 3], 10, -1894986606);
                    i = d(i, f, g, h, j[k + 10], 15, -1051523);
                    h = d(h, i, f, g, j[k + 1], 21, -2054922799);
                    g = d(g, h, i, f, j[k + 8], 6, 1873313359);
                    f = d(f, g, h, i, j[k + 15], 10, -30611744);
                    i = d(i, f, g, h, j[k + 6], 15, -1560198380);
                    h = d(h, i, f, g, j[k + 13], 21, 1309151649);
                    g = d(g, h, i, f, j[k + 4], 6, -145523070);
                    f = d(f, g, h, i, j[k + 11], 10, -1120210379);
                    i = d(i, f, g, h, j[k + 2], 15, 718787259);
                    h = d(h, i, f, g, j[k + 9], 21, -343485551);
                    g = e(g, n);
                    h = e(h, o);
                    i = e(i, p);
                    f = e(f, m);
                }
                return Array(g, h, i, f);
            }
            function f(a, b, c, d, f, g)
            {
                return e(j(e(e(b, a), e(d, g)), f), c);
            }
            function a(b, a, c, d, e, g, h)
            {
                return f(a & c | ~a & d, b, a, e, g, h);
            }
            function b(c, a, d, b, e, g, h)
            {
                return f(a & b | d & ~b, c, a, e, g, h);
            }
            function c(b, a, c, d, e, g, h)
            {
                return f(a ^ c ^ d, b, a, e, g, h);
            }
            function d(b, a, c, d, e, g, h)
            {
                return f(c ^ (a | ~d), b, a, e, g, h);
            }
            function e(b, c)
            {
                let a = (b & 65535) + (c & 65535);
                let d = (b >> 16) + (c >> 16) + (a >> 16);
                return d << 16 | a & 65535;
            }
            function j(a, b)
            {
                return a << b | a >>> 32 - b;
            }
            return m(l(n(str)));
        }
        /**
         * 获取16位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        public static Get16MD5Str(str: string, isLower: boolean = false): string
        {
            return this.Get32MD5Str(str, isLower).substr(8, 16);
        }
        /**
         * 转换为二进制字符串
         * @param str 要转换的字符串
         * @returns 转换后的字符串 
         */
        public static ConvertToBinary(str: string): string
        {
            let StrList: string = Array.prototype.map.call(str, function (c)
            {
                return c.charCodeAt(0).toString(2);
            });
            let resStr: string = "";
            for (let i = 0; i < StrList.length; i++)
            {
                resStr += StrList[i].MPadLeft(8, "0");
            }
            return resStr;
        }
        /**
         * 隐藏代码
         * @param codeStr 要隐藏的代码
         * @returns 隐藏后的代码
         */
        public static HideCode(codeStr: string): string
        {
            let resStr: string = this.ConvertToBinary(codeStr);
            resStr = resStr.replace(/0/g, "\u200d");
            resStr = resStr.replace(/1/g, "\u200c");
            return resStr;
        }
        /**
         * 显示代码
         * @param codeStr 被隐藏的代码
         * @returns 显示的代码 
         */
        public static ShowCode(codeStr: string): string
        {
            let resStr: string = codeStr.replace(/.{8}/g, function (u)
            {
                return String.fromCharCode(parseInt(u.replace(/\u200c/g, "1").replace(/\u200d/g, "0"), 2))
            });
            return resStr;
        }
    }
    /**
     * HttpMethod枚举
     */
    export enum HttpMethod
    {
        GET = "get",
        POST = "post",
    }
    /**
     * HTTP头内容类型
     */
    export enum HTTPHeadContentType
    {
        FormData = "multipart/form-data",
        FormUrlencoded = "application/x-www-form-urlencoded",
        Text = "text/plain",
        Json = "application/json",
        JavasScript = "application/javascript",
        XML = "application/xml",
        HTML = "text/html"
    }
    /**
     * Http配置类
     */
    export class HttpConfigModel
    {
        /**
         * URL地址
         */
        public url: string;
        /**
         * 要发送的数据
         */
        public data: Object;
        /**
         * 成功方法
         */
        public success: Function;
        /**
         * 失败方法
         */
        public error: Function;
        /**
         * 成功错误都执行的方法
         */
        public complete: Function;
        /**
         * HttpMethod类型
         */
        public method: HttpMethod;
        /**
         * 超时时间
         */
        public timeout: number = 15000;
        /**
         * 异步发送
         */
        public async: boolean = true;
        /**
         * HTTP头类型
         */
        public ContentType: HTTPHeadContentType;
        /**
         * 构造方法
         * @param url URL地址
         * @param method HttpMethod类型
         * @param data 要发送的数据
         * @param dataType 数据类型
         * @param success 成功方法
         * @param error 失败方法
         * @param complete 成功错误都执行的方法
         */
        constructor(url: string, method: HttpMethod = HttpMethod.POST, data: Object = null, dataType: HTTPHeadContentType = HTTPHeadContentType.Json, success: Function = null, error: Function = null, complete: Function = null)
        {
            this.url = url;
            this.method = method;
            this.data = data;
            this.ContentType = dataType;
            this.success = success;
            this.error = error;
            this.complete = complete;
        }
    }
    /**
     * Http帮助类
     */
    export class HttpManager
    {
        /**
         * 获取XMLHttpRequest对象
         * @param config 配置对象
         * @returns HttpRequest对象
         */
        private static GetHttpRequest(config: HttpConfigModel): XMLHttpRequest
        {
            let xhr: XMLHttpRequest;
            if (window["XMLHttpRequest"])
            {
                xhr = new XMLHttpRequest();
            }
            else
            {
                xhr = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhr.onreadystatechange = function ()
            {
                HttpManager.Readystatechange(xhr, config);
            }
            return xhr;
        }
        /**
         * 状态更改方法
         * @param xhr XMLHttpRequest对象
         * @param config 配置对象
         */
        private static Readystatechange(xhr: XMLHttpRequest, config: HttpConfigModel): void
        {
            if (xhr.readyState == 4)
            {
                let resM: any;
                try
                {
                    resM = JSON.parse(xhr.responseText);
                }
                catch (ex)
                {
                    resM = xhr.responseText;
                }
                if ((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304)
                {
                    if (config.complete)
                    {
                        config.complete(resM, xhr, xhr.status);
                    }
                    if (config.success)
                    {
                        config.success(resM, xhr, xhr.status);
                    }
                }
                else
                {
                    if (config.complete)
                    {
                        config.complete(resM, xhr, xhr.status);
                    }
                    if (config.error)
                    {
                        config.error(resM, xhr, xhr.status);
                    }
                }
            }
        }
        /**
         * 序列化参数
         * @param data 要序列化的参数
         * @returns 序列化后的字符串 
         */
        private static Serialize(data: Object): string
        {
            let result: string[] = new Array<string>();
            let value: string = "";
            for (let name in data)
            {
                if (typeof data[name] === "function")
                {
                    continue;
                }
                if (MateralTools.ToolsManager.MGetType(data[name]) == "Object")
                {
                    result.push(this.Serialize(data[name]));
                }
                else
                {
                    name = encodeURIComponent(name);
                    if (data[name])
                    {
                        value = data[name].toString();
                        value = encodeURIComponent(value);
                    }
                    else
                    {
                        value = "";
                    }
                    result.push(name + "=" + value);
                }
            };
            return result.join("&");
        }
        /**
         * 发送Post请求
         * @param config 配置对象
         */
        private static SendPost(config: HttpConfigModel): void
        {
            let xhr: XMLHttpRequest = this.GetHttpRequest(config);
            if (config.ContentType == HTTPHeadContentType.FormUrlencoded)
            {
                config.url += "?" + HttpManager.Serialize(config.data);
            }
            xhr.open(config.method, config.url, config.async);
            xhr.setRequestHeader("Content-type", config.ContentType);
            if (config.data)
            {
                switch (config.ContentType)
                {
                    case HTTPHeadContentType.Json:
                        xhr.send(JSON.stringify(config.data));
                        break;
                    case HTTPHeadContentType.FormUrlencoded:
                        xhr.send(null);
                        break;
                    default:
                        xhr.send(config.data);
                        break;
                }
            }
            else
            {
                xhr.send(null);
            }
        }
        /**
         * 发送Get请求
         * @param config 配置对象
         */
        private static SendGet(config: HttpConfigModel): void
        {
            let xhr: XMLHttpRequest = HttpManager.GetHttpRequest(config);
            let url: string = config.url;
            if (config.data)
            {
                url += "?" + HttpManager.Serialize(config.data);
            }
            xhr.open(config.method, url, config.async);
            xhr.setRequestHeader("Content-type", HTTPHeadContentType.FormUrlencoded);
            xhr.send(null);
        }
        /**
         * 发送请求
         * @param config 配置对象
         */
        public static Send(config: HttpConfigModel): void
        {
            if (config.method == HttpMethod.POST)
            {
                HttpManager.SendPost(config);
            }
            else
            {
                HttpManager.SendGet(config);
            }
        }
    }
    /**
     * 本地存储帮助类
     */
    export class LocalDataManager
    {
        /**
         * 是否支持本地存储
         * @returns 是否支持
         */
        public static IsLocalStorage(): boolean
        {
            if (window.localStorage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /**
         * 清空本地存储对象
         */
        public static CleanLocalData(): void
        {
            if (this.IsLocalStorage())
            {
                window.localStorage.clear();
            }
        }
        /**
         * 移除本地存储对象
         * @param key Key值
         */
        public static RemoveLocalData(key: string): void
        {
            if (this.IsLocalStorage() && key)
            {
                window.localStorage.removeItem(key);
            }
        }
        /**
         * 设置本地存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        public static SetLocalData(key: string, value: any, isJson: boolean = true): void
        {
            if (this.IsLocalStorage() && key && value)
            {
                this.RemoveLocalData(key);
                if (isJson)
                {
                    window.localStorage.setItem(key, JSON.stringify(value));
                }
                else
                {
                    window.localStorage.setItem(key, value.toString());
                }
            }
        }
        /**
         * 获取本地存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据 
         */
        public static GetLocalData(key: string, isJson: boolean = true): any
        {
            if (this.IsLocalStorage() == true && key)
            {
                if (isJson)
                {
                    return JSON.parse(window.localStorage.getItem(key));
                }
                else
                {
                    return window.localStorage.getItem(key);
                }
            }
            return null;
        }
        /**
         * 是否支持网页存储
         * @returns 是否支持 
         */
        public static IsSessionStorage(): boolean
        {
            if (window.sessionStorage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /**
         * 清空网页存储对象
         */
        public static CleanSessionData(): void
        {
            if (this.IsSessionStorage() == true)
            {
                window.sessionStorage.clear();
            }
        }
        /**
         * 移除网页存储对象
         * @param key Key值
         */
        public static RemoveSessionData(key: string)
        {
            if (this.IsSessionStorage() == true && key)
            {
                window.sessionStorage.removeItem(key);
            }
        }
        /**
         * 设置网页存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        public static SetSessionData(key: string, value: any, isJson: boolean = true)
        {
            if (!isJson && isJson != false)
            {
                isJson = true;
            }
            if (this.IsSessionStorage() && key && value)
            {
                this.RemoveSessionData(key);
                if (isJson)
                {
                    window.sessionStorage.setItem(key, JSON.stringify(value));
                }
                else
                {
                    window.sessionStorage.setItem(key, value.toString());
                }
            }
        }
        /**
         * 获取网页存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据 
         */
        public static GetSessionData(key: string, isJson: boolean = true): any
        {
            if (this.IsSessionStorage() == true && key)
            {
                if (isJson)
                {
                    return JSON.parse(window.sessionStorage.getItem(key));
                }
                else
                {
                    return window.sessionStorage.getItem(key);
                }
            }
            return null;
        }
        /**
         * 获得有效时间
         * @param timeValue 值
         * @param timeType 单位
         * @returns 计算后的时间
         */
        private static GetTime(timeValue: number = 10000, timeType: TimeType = TimeType.Minutes): number
        {
            switch (timeType)
            {
                case TimeType.Years:
                    timeValue = 60 * 60 * 24 * 365 * timeValue * 1000;
                    break;
                case TimeType.Months:
                    timeValue = 60 * 60 * 24 * 30 * timeValue * 1000;
                    break;
                case TimeType.Day:
                    timeValue = 60 * 60 * 24 * timeValue * 1000;
                    break;
                case TimeType.Hours:
                    timeValue = 60 * 60 * timeValue * 1000;
                    break;
                case TimeType.Minutes:
                    timeValue = 60 * timeValue * 1000;
                    break;
                case TimeType.Seconds:
                    timeValue = timeValue * 1000;
                    break;
                case TimeType.Milliseconds:
                    timeValue = timeValue;
                    break;
            }
            return timeValue;
        }
        /**
         * 设置一个Cookie
         * @param key Key值
         * @param value 要保存的值
         * @param time 持续时间
         * @param timeType 单位(默认s[秒])
         */
        public static SetCookie(key: string, value: any, isJson: boolean = true, timeValue: number = 60, timeType: TimeType = TimeType.Minutes, path: string = "/")
        {
            if (isJson)
            {
                document.cookie = key + "=" + JSON.stringify(value) + ";max-age=" + this.GetTime(timeValue, timeType) + ";path=" + path;
            }
            else
            {
                document.cookie = key + "=" + value + ";max-age=" + this.GetTime(timeValue, timeType) + ";path=" + path;
            }
        }
        /**
         * 删除一个Cookie
         * @param key Key值
         */
        public static RemoveCookie(key: string)
        {
            document.cookie = key + "=;max-age=0";
        }
        /**
         * 获得所有Cookie
         * @returns Cookie对象 
         */
        public static GetAllCookie(): Object
        {
            let cookies: string[] = document.cookie.split(";");
            let cookie: Array<string[]> = new Array();
            let LocalCookie = new Object();
            for (var i = 0; i < cookies.length; i++)
            {
                if (!cookies[i].MIsNullOrUndefinedOrEmpty())
                {
                    cookie[i] = cookies[i].trim().split("=");
                    if (cookie[i][0] && cookie[i][1])
                    {
                        LocalCookie[cookie[i][0]] = cookie[i][1];
                    }
                }
            }
            return LocalCookie;
        }
        /**
         * 获得Cookie
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns
         */
        public static GetCookie(key: string, isJson: boolean = true): any
        {
            let resM: Object = this.GetAllCookie();
            if (isJson && resM && resM[key])
            {
                return JSON.parse(resM[key]);
            }
            else
            {
                return null;
            }
        }
        /**
         * 设置数据
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         * @param time 时间
         * @param timeType 时间类型
         */
        public static SetData(key: string, value: any, isJson: boolean = true, time: number = 60, timeType: TimeType = TimeType.Minutes): void
        {
            if (this.IsLocalStorage())
            {
                this.SetLocalData(key, value, isJson);
            }
            else
            {
                this.SetCookie(key, value, isJson, time, timeType);
            }
        }
        /**
         * 获得数据
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns 获取到的数据
         */
        public static GetData(key: string, isJson: boolean = true): any
        {
            if (this.IsLocalStorage())
            {
                return this.GetLocalData(key, isJson);
            }
            else
            {
                return this.GetCookie(key, isJson);
            }
        }
        /**
         * 移除数据
         * @param key Key值
         */
        public static RemoveData(key: string): void
        {
            if (this.IsLocalStorage())
            {
                this.RemoveLocalData(key);
            }
            else
            {
                this.RemoveCookie(key);
            }
        }
    }
    /**
     * 实现引擎模型
     */
    export class EngineInfoModel
    {
        //是否为Trident引擎
        public Trident: boolean = false;
        //是否为Gecko引擎
        public Gecko: boolean = false;
        //是否为WebKit引擎
        public WebKit: boolean = false;
        //是否为KHTML引擎
        public KHTML: boolean = false;
        //是否为Presto引擎
        public Presto: boolean = false;
        //具体版本号
        public Version: string = "";
    }
    /**
     * 浏览器模型
     */
    export class BrowserInfoModel
    {
        //是否为IE浏览器
        public IE: boolean = false;
        //是否为Firefox浏览器
        public Firefox: boolean = false;
        //是否为Safari浏览器
        public Safari: boolean = false;
        //是否为Konqueror浏览器
        public Konqueror: boolean = false;
        //是否为Opera浏览器
        public Opera: boolean = false;
        //是否为Chrome浏览器
        public Chrome: boolean = false;
        //是否为Edge浏览器
        public Edge: boolean = false;
        //是否为QQ浏览器
        public QQ: boolean = false;
        //是否为UC浏览器
        public UC: boolean = false;
        //是否为Maxthon(遨游)浏览器
        public Maxthon: boolean = false;
        //是否为微信浏览器
        public WeChat: boolean = false;
        //具体版本号
        public Version: string = "";
    }
    /**
     * 系统模型
     */
    export class SystemInfoModel
    {
        //是否为Windows操作系统
        public Windows: boolean = false;
        //是否为WindowsMobile操作系统
        public WindowsMobile: boolean = false;
        //Windows版本
        public WindowsVersion: string = "";
        //是否为Mac操作系统
        public Mac: boolean = false;
        //是否为Unix操作系统
        public Unix: boolean = false;
        //是否为Linux操作系统
        public Linux: boolean = false;
        //是否为iPhone操作系统
        public iPhone: boolean = false;
        //是否为iPod操作系统
        public iPod: boolean = false;
        //是否为Windows操作系统
        public iPad: boolean = false;
        //是否为Windows操作系统
        public IOS: boolean = false;
        //IOS版本
        public IOSVersion: string = "";
        //是否为Android操作系统
        public Android: boolean = false;
        //Android版本
        public AndroidVersion: string = "";
        //是否为NokiaN操作系统
        public NokiaN: boolean = false;
        //是否为Wii操作系统
        public Wii: boolean = false;
        //是否为PS操作系统
        public PS: boolean = false;
    }
    /**
     * 客户端信息模型
     */
    export class ClientInfoModel
    {
        private _engineM: EngineInfoModel = new EngineInfoModel();
        private _browserM: BrowserInfoModel = new BrowserInfoModel();
        private _systemM: SystemInfoModel = new SystemInfoModel();
        /**
         * 实现引擎信息
         */
        public get EngineInfoM(): EngineInfoModel
        {
            return (MateralTools.ToolsManager.Clone(this._engineM) as EngineInfoModel);
        }
        /**
         * 浏览器信息
         */
        public get BrowserInfoM(): BrowserInfoModel
        {
            return (MateralTools.ToolsManager.Clone(this._browserM) as BrowserInfoModel);
        }
        /**
         * 系统信息
         */
        public get SystemInfoM(): SystemInfoModel
        {
            return (MateralTools.ToolsManager.Clone(this._systemM) as SystemInfoModel);
        }
        /**
         * 客户端信息模型
         */
        constructor()
        {
            //检测呈现引擎和浏览器
            let userAgent: string = navigator.userAgent;
            if (window["opera"])
            {
                this._engineM.Version = this._engineM.Version = window["opera"].version();
                this._engineM.Presto = this._browserM.Opera = true;
            }
            else if (/AppleWebKit\/(\S+)/.test(userAgent))
            {
                this._engineM.Version = RegExp["$1"];
                this._engineM.WebKit = true;
                if (/MicroMessenger\/(\S+)/.test(userAgent))
                {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.WeChat = true;
                }
                else if (/Edge\/(\S+)/.test(userAgent))
                {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Edge = true;
                }
                else if (/QQBrowser\/(\S+)/.test(userAgent))
                {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.QQ = true;
                }
                else if (/UBrowser\/(\S+)/.test(userAgent))
                {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.UC = true;
                }
                else if (/Maxthon\/(\S+)/.test(userAgent))
                {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Maxthon = true;
                }
                else if (/Chrome\/(\S+)/.test(userAgent))
                {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Chrome = true;
                }
                else if (/Safari\/(\S+)/.test(userAgent))
                {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Safari = true;
                }
                else
                {
                    if (this._engineM.WebKit)
                    {
                        let safariVersion: string = "";
                        let WebKitVersion: number = parseInt(this._engineM.Version);
                        if (WebKitVersion < 100)
                        {
                            safariVersion = "1";
                        }
                        else if (WebKitVersion < 312)
                        {
                            safariVersion = "1.2";
                        }
                        else if (WebKitVersion < 412)
                        {
                            safariVersion = "1.3";
                        }
                        else
                        {
                            safariVersion = "2";
                        }
                        this._browserM.Version = safariVersion;
                        this._browserM.Safari = true;
                    }
                }
            }
            else if (/KHTML\/(\S+)/.test(userAgent) || /Konqueror\/([^;]+)/.test(userAgent))
            {
                this._engineM.Version = this._browserM.Version = RegExp["$1"];
                this._engineM.KHTML = this._browserM.Konqueror = true;
            }
            else if (/rv:([^\)]+)\) Gecko\/\d{8}/.test(userAgent))
            {
                this._engineM.Version = RegExp["$1"];
                this._engineM.Gecko = true;
                if (/Firefox\/(\S+)/.test(userAgent))
                {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Firefox = true;
                }
            }
            else if (/MSIE ([^;]+)/.test(userAgent))
            {
                this._engineM.Version = this._browserM.Version = RegExp["$1"];
                this._engineM.Trident = this._browserM.IE = true;
            }
            else
            {
                if (window["ActiveXObject"] || "ActiveXObject" in window)
                {
                    this._engineM.Version = this._browserM.Version = "11";
                    this._engineM.Trident = this._browserM.IE = true
                }
            }
            //检测平台
            var p = navigator.platform;
            this._systemM.Windows = p.indexOf("Win") == 0;
            if (this._systemM.Windows)
            {
                if (/Win(?:dows )?([^do]{2})\s?(\d+\.\d+)?/.test(userAgent))
                {
                    if (RegExp["$1"] == "NT")
                    {
                        switch (RegExp["$2"])
                        {
                            case "5.0":
                                this._systemM.WindowsVersion = "2000";
                                break;
                            case "5.1":
                                this._systemM.WindowsVersion = "XP";
                                break;
                            case "6.0":
                                this._systemM.WindowsVersion = "Vista";
                                break;
                            case "6.1":
                                this._systemM.WindowsVersion = "7";
                                break;
                            case "6.2":
                                this._systemM.WindowsVersion = "8";
                                break;
                            case "10.0":
                                this._systemM.WindowsVersion = "10";
                                break;
                            default:
                                this._systemM.WindowsVersion = "NT";
                                break;
                        }
                    }
                    else if (RegExp["$1"] == "9X")
                    {
                        this._systemM.WindowsVersion = "ME";
                    }
                    else
                    {
                        this._systemM.WindowsVersion = RegExp["$1"];
                    }
                }
                if (this._systemM.WindowsVersion == "CE")
                {
                    this._systemM.WindowsMobile = true;
                }
                else if (this._systemM.WindowsVersion == "Ph")
                {
                    if (/Windows Phone OS (\d+.\d+)/.test(userAgent))
                    {
                        this._systemM.WindowsMobile = true;
                    }
                }
            }
            this._systemM.Mac = p.indexOf("Mac") == 0;
            if (this._systemM.Mac && userAgent.indexOf("Mobile") > -1)
            {
                if (/CPU (?:iPhone)?OS (\d+_\d+)/.test(userAgent))
                {
                    this._systemM.IOS = true;
                    this._systemM.IOSVersion = RegExp["$1"].replace("_", ".");
                }
                else
                {
                    this._systemM.IOS = true;
                    this._systemM.IOSVersion = "2";
                }
            }
            this._systemM.Unix = p.indexOf("X11") == 0;
            this._systemM.Linux = p.indexOf("Linux") == 0;
            this._systemM.iPhone = p.indexOf("iPhone") == 0;
            this._systemM.iPod = p.indexOf("iPod") == 0;
            this._systemM.iPad = p.indexOf("iPad") == 0;
            this._systemM.NokiaN = userAgent.indexOf("NokiaN") > -1;
            this._systemM.Wii = userAgent.indexOf("Wii") > -1;
            this._systemM.PS = /playstation/i.test(userAgent);
            if (/Android (\d+\.\d+)/.test(userAgent))
            {
                this._systemM.Android = true;
                this._systemM.AndroidVersion = RegExp["$1"];
            }
        }
    }
}