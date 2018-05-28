interface String {
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
interface Date {
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
interface Location {
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
interface Document {
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
interface Element {
    /**
     * 设置样式
     * @param element 页面元素
     * @param className 要设置的样式列表
     */
    MSetClass(className: string | string[]): any;
    /**
     * 添加样式
     * @param className 要添加的样式
     */
    MAddClass(className: string | string[]): any;
    /**
     * 删除样式
     * @param className 要删除的样式
     */
    MRemoveClass(className: string | string[]): any;
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
interface HTMLElement {
    /**
     * 获得自定义属性
     * @returns 自定义属性
     */
    MGetDataSet(): DOMStringMap | Object;
}
interface Event {
    /**
     * 获得事件触发元素
     * @returns 触发元素
     */
    MGetEventTarget(): Element | EventTarget;
}
interface Array<T> {
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
interface Math {
    /**
     * 返回一个随机数
     * @param min 最小值
     * @param max 最大值
     * @returns 随机数
     */
    MGetRandom(min: number, max: number): number;
    /**
     * 获取四边形的外接圆半径
     * @param length 长
     * @param width 宽
     * @param IsRound 是圆形
     */
    MGetCircumcircleRadius(length: number, width: number, IsRound: boolean): number;
}
declare namespace MateralTools {
    /**
     * 对象帮助类
     */
    class ToolsManager {
        /**
         * 获得对象类型
         * @param obj 传入对象
         * @param IncludeCustom 包括自定义类型
         * @returns 对象类型
         */
        static MGetType(obj: any, IncludeCustom?: boolean): string;
        /**
         * 克隆对象
         * @param obj 要克隆的对象
         */
        static Clone(obj: any): any;
    }
    enum TimeType {
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
        Milliseconds = 6,
    }
    /**
     * 返回对象类型
     */
    enum MResultType {
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
        Error = 2,
    }
    /**
     * 分页模型
     */
    class MPageModel {
        /**
         * 查询页数
         */
        PageIndex: number;
        /**
         * 每页显示数量
         */
        PageSize: number;
        /**
         * 总页数
         */
        PageCount: number;
        /**
         * 数据总数
         */
        DataCount: number;
    }
    /**
     * 返回模型
     */
    class MResultModel<T> {
        /**
         * 对象类型
         */
        ResultType: MResultType;
        /**
         * 返回消息
         */
        Message: string;
        /**
         * 携带数据
         */
        Data: T;
    }
    /**
     * 携带分页数据的返回对象
     */
    class MResultPageModel<T> extends MResultModel<T> {
        /**
         * 分页信息
         */
        PageInfo: MPageModel;
    }
    /**
     * 加密帮助类
     */
    class EncryptionManager {
        /**
         * 获取32位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        static Get32MD5Str(str: string, isLower?: boolean): string;
        /**
         * 获取16位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        static Get16MD5Str(str: string, isLower?: boolean): string;
        /**
         * 转换为二进制字符串
         * @param str 要转换的字符串
         * @returns 转换后的字符串
         */
        static ConvertToBinary(str: string): string;
        /**
         * 隐藏代码
         * @param codeStr 要隐藏的代码
         * @returns 隐藏后的代码
         */
        static HideCode(codeStr: string): string;
        /**
         * 显示代码
         * @param codeStr 被隐藏的代码
         * @returns 显示的代码
         */
        static ShowCode(codeStr: string): string;
    }
    /**
     * HttpMethod枚举
     */
    enum HttpMethod {
        GET = "get",
        POST = "post",
    }
    /**
     * HTTP头内容类型
     */
    enum HTTPHeadContentType {
        FormData = "multipart/form-data",
        FormUrlencoded = "application/x-www-form-urlencoded",
        Text = "text/plain",
        Json = "application/json",
        JavasScript = "application/javascript",
        XML = "application/xml",
        HTML = "text/html",
    }
    /**
     * Http配置类
     */
    class HttpConfigModel {
        /**
         * URL地址
         */
        url: string;
        /**
         * 要发送的数据
         */
        data: Object;
        /**
         * 成功方法
         */
        success: Function;
        /**
         * 失败方法
         */
        error: Function;
        /**
         * 成功错误都执行的方法
         */
        complete: Function;
        /**
         * HttpMethod类型
         */
        method: HttpMethod;
        /**
         * 超时时间
         */
        timeout: number;
        /**
         * 异步发送
         */
        async: boolean;
        /**
         * HTTP头类型
         */
        ContentType: HTTPHeadContentType;
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
        constructor(url: string, method?: HttpMethod, data?: Object, dataType?: HTTPHeadContentType, success?: Function, error?: Function, complete?: Function);
    }
    /**
     * Http帮助类
     */
    class HttpManager {
        /**
         * 获取XMLHttpRequest对象
         * @param config 配置对象
         * @returns HttpRequest对象
         */
        private static GetHttpRequest(config);
        /**
         * 状态更改方法
         * @param xhr XMLHttpRequest对象
         * @param config 配置对象
         */
        private static Readystatechange(xhr, config);
        /**
         * 序列化参数
         * @param data 要序列化的参数
         * @returns 序列化后的字符串
         */
        private static Serialize(data);
        /**
         * 发送Post请求
         * @param config 配置对象
         */
        private static SendPost(config);
        /**
         * 发送Get请求
         * @param config 配置对象
         */
        private static SendGet(config);
        /**
         * 发送请求
         * @param config 配置对象
         */
        static Send(config: HttpConfigModel): void;
    }
    /**
     * 本地存储帮助类
     */
    class LocalDataManager {
        /**
         * 是否支持本地存储
         * @returns 是否支持
         */
        static IsLocalStorage(): boolean;
        /**
         * 清空本地存储对象
         */
        static CleanLocalData(): void;
        /**
         * 移除本地存储对象
         * @param key Key值
         */
        static RemoveLocalData(key: string): void;
        /**
         * 设置本地存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        static SetLocalData(key: string, value: any, isJson?: boolean): void;
        /**
         * 获取本地存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据
         */
        static GetLocalData(key: string, isJson?: boolean): any;
        /**
         * 是否支持网页存储
         * @returns 是否支持
         */
        static IsSessionStorage(): boolean;
        /**
         * 清空网页存储对象
         */
        static CleanSessionData(): void;
        /**
         * 移除网页存储对象
         * @param key Key值
         */
        static RemoveSessionData(key: string): void;
        /**
         * 设置网页存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        static SetSessionData(key: string, value: any, isJson?: boolean): void;
        /**
         * 获取网页存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据
         */
        static GetSessionData(key: string, isJson?: boolean): any;
        /**
         * 获得有效时间
         * @param timeValue 值
         * @param timeType 单位
         * @returns 计算后的时间
         */
        private static GetTime(timeValue?, timeType?);
        /**
         * 设置一个Cookie
         * @param key Key值
         * @param value 要保存的值
         * @param time 持续时间
         * @param timeType 单位(默认s[秒])
         */
        static SetCookie(key: string, value: any, isJson?: boolean, timeValue?: number, timeType?: TimeType, path?: string): void;
        /**
         * 删除一个Cookie
         * @param key Key值
         */
        static RemoveCookie(key: string): void;
        /**
         * 获得所有Cookie
         * @returns Cookie对象
         */
        static GetAllCookie(): Object;
        /**
         * 获得Cookie
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns
         */
        static GetCookie(key: string, isJson?: boolean): any;
        /**
         * 设置数据
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         * @param time 时间
         * @param timeType 时间类型
         */
        static SetData(key: string, value: any, isJson?: boolean, time?: number, timeType?: TimeType): void;
        /**
         * 获得数据
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns 获取到的数据
         */
        static GetData(key: string, isJson?: boolean): any;
        /**
         * 移除数据
         * @param key Key值
         */
        static RemoveData(key: string): void;
    }
    /**
     * 实现引擎模型
     */
    class EngineInfoModel {
        Trident: boolean;
        Gecko: boolean;
        WebKit: boolean;
        KHTML: boolean;
        Presto: boolean;
        Version: string;
    }
    /**
     * 浏览器模型
     */
    class BrowserInfoModel {
        IE: boolean;
        Firefox: boolean;
        Safari: boolean;
        Konqueror: boolean;
        Opera: boolean;
        Chrome: boolean;
        Edge: boolean;
        QQ: boolean;
        UC: boolean;
        Maxthon: boolean;
        WeChat: boolean;
        Version: string;
    }
    /**
     * 系统模型
     */
    class SystemInfoModel {
        Windows: boolean;
        WindowsMobile: boolean;
        WindowsVersion: string;
        Mac: boolean;
        Unix: boolean;
        Linux: boolean;
        iPhone: boolean;
        iPod: boolean;
        iPad: boolean;
        IOS: boolean;
        IOSVersion: string;
        Android: boolean;
        AndroidVersion: string;
        NokiaN: boolean;
        Wii: boolean;
        PS: boolean;
    }
    /**
     * 客户端信息模型
     */
    class ClientInfoModel {
        private _engineM;
        private _browserM;
        private _systemM;
        /**
         * 实现引擎信息
         */
        readonly EngineInfoM: EngineInfoModel;
        /**
         * 浏览器信息
         */
        readonly BrowserInfoM: BrowserInfoModel;
        /**
         * 系统信息
         */
        readonly SystemInfoM: SystemInfoModel;
        /**
         * 客户端信息模型
         */
        constructor();
    }
}
