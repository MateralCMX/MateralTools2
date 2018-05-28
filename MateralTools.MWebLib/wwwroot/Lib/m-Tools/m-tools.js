"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
String.prototype.MIsEmpty = function () {
    return this === "";
};
String.prototype.MIsNullOrUndefinedOrEmpty = function () {
    return this === null || this === undefined || this.MIsEmpty();
};
String.prototype.MTrimLeft = function () {
    var str = this;
    while (str.substr(0, 1) === " ") {
        str = str.substr(1, str.length - 1);
    }
    return str;
};
String.prototype.MTrimRight = function () {
    var str = this;
    while (str.substr(str.length - 2, 1) === " ") {
        str = str.substr(0, str.length - 2);
    }
    return str;
};
String.prototype.MTrimAll = function () {
    var str = this;
    return str.replace(/\s/g, "");
};
String.prototype.MSimplifyMultiSpaceToSingle = function () {
    var str = this;
    return str.replace(/\s{2,}/g, " ");
};
if (!String.prototype.trim) {
    String.prototype.trim = function () {
        return this.MTrimLeft().MTrimRight();
    };
}
String.prototype.MPadLeft = function (length, character) {
    if (character === void 0) { character = " "; }
    var str = this;
    for (var i = str.length; i < length; i++) {
        str = character + str;
    }
    return str;
};
String.prototype.MPadRght = function (length, character) {
    if (character === void 0) { character = " "; }
    var str = this;
    for (var i = str.length; i < length; i++) {
        str = str + character;
    }
    return str;
};
Date.prototype.MGetTimeDifference = function (targetDate, timeType) {
    if (timeType === void 0) { timeType = MateralTools.TimeType.Seconds; }
    var timeDifference = this.getTime() - targetDate.getTime();
    switch (timeType) {
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
Date.prototype.MDateTimeFormat = function (formatStr) {
    if (formatStr === void 0) { formatStr = "yyyy/MM/dd HH:mm:ss"; }
    var dateTime = this;
    var formatData = {
        "M+": dateTime.getMonth() + 1,
        "d+": dateTime.getDate(),
        "H+": dateTime.getHours(),
        "m+": dateTime.getMinutes(),
        "s+": dateTime.getSeconds(),
        "q+": Math.floor((dateTime.getMonth() + 3) / 3),
        "S": dateTime.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(formatStr)) {
        formatStr = formatStr.replace(RegExp.$1, (dateTime.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var data in formatData) {
        if (new RegExp("(" + data + ")").test(formatStr)) {
            formatStr = formatStr.replace(RegExp.$1, (RegExp.$1.length == 1) ? (formatData[data]) : (("00" + formatData[data]).substr(("" + formatData[data]).length)));
        }
    }
    return formatStr;
};
Date.prototype.MGetInputDateTimeValueStr = function () {
    return this.MDateTimeFormat("yyyy-MM-ddTHH:mm:ss");
};
Date.prototype.MGetDateByTimeZone = function (timeZone) {
    if (timeZone === void 0) { timeZone = 8; }
    var date = this;
    var len = date.getTime();
    var offset = date.getTimezoneOffset() * 60000;
    var utcTime = len + offset;
    date = new Date(utcTime + 3600000 * timeZone);
    return date;
};
Date.prototype.MConvertTimeZone = function (timeZone) {
    if (timeZone === void 0) { timeZone = null; }
    var date = this;
    if (!timeZone) {
        timeZone = date.getTimezoneOffset() / 60;
    }
    date.setTime(date.getTime() - timeZone * 60 * 60 * 1000);
    return date;
};
Location.prototype.MGetURLParams = function () {
    var params = new Object();
    var paramsStr = window.location.search;
    var paramsStrs = new Array();
    if (!paramsStr.MIsNullOrUndefinedOrEmpty()) {
        paramsStr = paramsStr.substring(1, paramsStr.length);
        paramsStrs = paramsStr.split("&");
        for (var i = 0; i < paramsStrs.length; i++) {
            var temp = paramsStrs[i].split("=");
            if (temp.length == 2) {
                params[temp[0]] = temp[1];
            }
            else if (temp.length == 1) {
                params[temp[0]] = null;
            }
        }
    }
    return params;
};
Location.prototype.MGetURLParam = function (key) {
    var params = window.location.MGetURLParams();
    if (params[key]) {
        return params["key"];
    }
    else {
        return null;
    }
};
document.MGetScrollTop = function () {
    var scrollTop = 0;
    if (this.documentElement && this.documentElement.scrollTop) {
        scrollTop = this.documentElement.scrollTop;
    }
    else if (this.body) {
        scrollTop = this.body.scrollTop;
    }
    return scrollTop;
};
document.MGetClientHeight = function () {
    var clientHeight = 0;
    if (this.body.clientHeight && this.documentElement.clientHeight) {
        clientHeight = (this.body.clientHeight < this.documentElement.clientHeight) ? this.body.clientHeight : this.documentElement.clientHeight;
    }
    else {
        clientHeight = (this.body.clientHeight > this.documentElement.clientHeight) ? this.body.clientHeight : this.documentElement.clientHeight;
    }
    return clientHeight;
};
document.$ = function (element) {
    if (MateralTools.ToolsManager.MGetType(element) === "string") {
        element = document.getElementById(element);
    }
    return element;
};
Element.prototype.MSetClass = function (className) {
    var element = this;
    var classStr = "";
    var TypeStr = MateralTools.ToolsManager.MGetType(className);
    var ClassList;
    classStr = TypeStr === "Array" ? className.join(" ") : className;
    if (!classStr.MIsNullOrUndefinedOrEmpty()) {
        classStr = classStr.MSimplifyMultiSpaceToSingle().trim();
        element.setAttribute("class", classStr);
    }
    else {
        element.removeAttribute("class");
    }
};
Element.prototype.MAddClass = function (className) {
    var element = this;
    if (MateralTools.ToolsManager.MGetType(className) === "string") {
        className = className.split(" ");
    }
    for (var i = 0; i < className.length; i++) {
        element.classList.add(className[i]);
    }
};
Element.prototype.MRemoveClass = function (className) {
    var element = this;
    if (MateralTools.ToolsManager.MGetType(className) === "string") {
        className = className.split(" ");
    }
    for (var i = 0; i < className.length; i++) {
        element.classList.remove(className[i]);
    }
};
Element.prototype.MHasClass = function (className) {
    var element = this;
    var resM = true;
    if (MateralTools.ToolsManager.MGetType(className) === "string") {
        className = className.split(" ");
    }
    for (var i = 0; i < className.length && resM; i++) {
        resM = element.classList.contains(className[i]);
    }
    return resM;
};
Element.prototype.MGetElementsByClassName = function (className) {
    var element = this;
    className = className.trim();
    var resultM = new Array();
    var elements;
    if (element.getElementsByClassName) {
        elements = element.getElementsByClassName(className);
        return elements;
    }
    else {
        elements = element.getElementsByTagName("*");
        for (var i = 0; i < elements.length; i++) {
            if (elements[i].MHasClass(className)) {
                resultM.push(elements[i]);
            }
        }
        return resultM;
    }
};
Element.prototype.MGetElementsByName = function (name) {
    var element = this;
    name = name.trim();
    var resultM = new Array();
    var elements = element.getElementsByTagName("*");
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].name && elements[i].name == name) {
            resultM.push(elements[i]);
        }
    }
    return resultM;
};
if (!Element.prototype.addEventListener) {
    Element.prototype.addEventListener = function (type, listener, options) {
        Element["attachEvent"]("on" + type, listener);
    };
}
Element.prototype.MGetChildren = function () {
    var children;
    var element = this;
    if (element.children) {
        children = element.children;
    }
    else {
        children = new Array();
        var length_1 = element.childNodes.length;
        for (var i = 0; i < length_1; i++) {
            if (element.childNodes[i].nodeType == 1) {
                children.push(element.childNodes[i]);
            }
        }
    }
    return children;
};
Element.prototype.MGetComputedStyle = function () {
    var element = this;
    var cssStyle;
    if (element["currentStyle"]) {
        cssStyle = element["currentStyle"];
    }
    else {
        cssStyle = getComputedStyle(element);
    }
    return cssStyle;
};
HTMLElement.prototype.MGetDataSet = function () {
    var DataSet;
    var element = this;
    if (element.dataset) {
        DataSet = element.dataset;
    }
    else {
        DataSet = new Object();
        var length_2 = element.attributes.length;
        var item = void 0;
        for (var i = 0; i < length_2; i++) {
            item = element.attributes[i];
            if (item.specified && /^data-/.test(item.nodeName)) {
                DataSet[item.nodeName.substring(5)] = item.nodeValue;
            }
        }
    }
    return DataSet;
};
Event.prototype.MGetEventTarget = function () {
    var event = this;
    return event["target"] || event["srcElement"];
};
if (!window.addEventListener) {
    window.addEventListener = function (type, listener, options) {
        window["attachEvent"]("on" + type, listener);
    };
}
if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (searchElement, formIndex) {
        if (formIndex === void 0) { formIndex = 0; }
        var array = this;
        for (var i = formIndex; i < array.length; i++) {
            if (array[i] == searchElement) {
                return i;
            }
        }
        return -1;
    };
}
Array.prototype.MClear = function () {
    var array = this;
    array.splice(0, array.length);
    return array;
};
Array.prototype.MInsert = function (item, index) {
    var array = this;
    array.splice(index, 0, item);
    return array;
};
Array.prototype.MRemoveTo = function (index) {
    var array = this;
    var count = array.length;
    array.splice(index, 1);
    if (count === array.length && count === 1) {
        array = [];
    }
    return array;
};
Array.prototype.MRemove = function (item) {
    var array = this;
    var index = array.indexOf(item);
    if (index >= 0) {
        array = array.MRemoveTo(index);
    }
    return array;
};
Array.prototype.MRomeveAll = function (item) {
    var array = this;
    var index = array.indexOf(item);
    while (index >= 0) {
        array = array.MRemoveTo(index);
        index = array.indexOf(item);
    }
    return array;
};
if (!JSON.parse) {
    JSON.parse = function (jsonStr) {
        return eval("(" + jsonStr + ")");
    };
}
if (!JSON.stringify) {
    JSON.stringify = function (jsonObj) {
        var resM = "";
        var IsArray;
        var TypeStr;
        for (var key in jsonObj) {
            IsArray = false;
            TypeStr = MateralTools.ToolsManager.MGetType(jsonObj[key]);
            if (jsonObj instanceof Array) {
                IsArray = true;
            }
            if (TypeStr == "string") {
                if (IsArray) {
                    resM += "\"" + jsonObj[key].toString() + "\",";
                }
                else {
                    resM += "\"" + key + "\":\"" + jsonObj[key].toString() + "\",";
                }
            }
            else if (jsonObj[key] instanceof RegExp) {
                if (IsArray) {
                    resM += jsonObj[key].toString() + ",";
                }
                else {
                    resM += "\"" + key + "\":\"" + jsonObj[key].toString() + "\",";
                }
            }
            else if (jsonObj[key] instanceof Array) {
                resM += "\"" + key + "\":" + this.JSONStringify(jsonObj[key]) + ",";
            }
            else if (TypeStr == "boolean") {
                if (IsArray) {
                    resM += jsonObj[key].toString() + ",";
                }
                else {
                    resM += "\"" + key + "\":" + jsonObj[key].toString() + ",";
                }
            }
            else if (TypeStr == "number") {
                if (IsArray) {
                    resM += jsonObj[key].toString() + ",";
                }
                else {
                    resM += "\"" + key + "\":" + jsonObj[key].toString() + ",";
                }
            }
            else if (jsonObj[key] instanceof Object) {
                if (IsArray) {
                    resM += this.JSONStringify(jsonObj[key]) + ",";
                }
                else {
                    resM += "\"" + key + "\":" + this.JSONStringify(jsonObj[key]) + ",";
                }
            }
            else if (!jsonObj[key] || jsonObj[key] instanceof Function) {
                if (IsArray) {
                    resM += "null,";
                }
                else {
                    resM += "\"" + key + "\":null,";
                }
            }
        }
        if (IsArray) {
            resM = "[" + resM.slice(0, -1) + "]";
        }
        else {
            resM = "{" + resM.slice(0, -1) + "}";
        }
        return resM;
    };
}
Math.MGetRandom = function (min, max) {
    return Math.floor(Math.random() * max + min);
};
Math.MGetCircumcircleRadius = function (length, width, IsRound) {
    if (width === void 0) { width = length; }
    if (IsRound === void 0) { IsRound = true; }
    var max = Math.max(length, width);
    //正方形的对角线=边长^2*2
    var diameter = Math.sqrt(Math.pow(max, 2) * 2);
    //外接圆的直径=正方形的对角线
    //圆的半径=直径/2
    var radius = diameter / 2;
    if (IsRound) {
        radius = Math.round(radius);
    }
    return radius;
};
var MateralTools;
(function (MateralTools) {
    /**
     * 对象帮助类
     */
    var ToolsManager = /** @class */ (function () {
        function ToolsManager() {
        }
        /**
         * 获得对象类型
         * @param obj 传入对象
         * @param IncludeCustom 包括自定义类型
         * @returns 对象类型
         */
        ToolsManager.MGetType = function (obj, IncludeCustom) {
            if (IncludeCustom === void 0) { IncludeCustom = true; }
            var resStr = typeof obj;
            if (resStr === "object") {
                if (obj === null) {
                    resStr = "null";
                }
                else {
                    resStr = Object.prototype.toString.call(obj).slice(8, -1);
                    if (resStr === "Object" && obj.constructor && obj.constructor.name != "Object" && IncludeCustom) {
                        resStr = obj.constructor.name;
                    }
                }
            }
            return resStr.toLowerCase();
        };
        /**
         * 克隆对象
         * @param obj 要克隆的对象
         */
        ToolsManager.Clone = function (obj) {
            var ObjectType = MateralTools.ToolsManager.MGetType(obj, false);
            var result;
            if (ObjectType == "Object") {
                result = new Object();
            }
            else if (ObjectType == "array") {
                result = new Array();
            }
            else {
                result = obj;
            }
            for (var i in obj) {
                var copy = obj[i];
                var SubObjectType = MateralTools.ToolsManager.MGetType(copy, false);
                if (SubObjectType == "Object" || SubObjectType == "array") {
                    result[i] = arguments.callee(copy);
                }
                else {
                    result[i] = copy;
                }
            }
            return result;
        };
        return ToolsManager;
    }());
    MateralTools.ToolsManager = ToolsManager;
    /*
     * 时间类型
     */
    var TimeType;
    (function (TimeType) {
        /**
         * 年
         */
        TimeType[TimeType["Years"] = 0] = "Years";
        /**
         * 月
         */
        TimeType[TimeType["Months"] = 1] = "Months";
        /**
         * 日
         */
        TimeType[TimeType["Day"] = 2] = "Day";
        /**
         * 时
         */
        TimeType[TimeType["Hours"] = 3] = "Hours";
        /**
         * 分
         */
        TimeType[TimeType["Minutes"] = 4] = "Minutes";
        /**
         * 秒
         */
        TimeType[TimeType["Seconds"] = 5] = "Seconds";
        /**
         * 毫秒
         */
        TimeType[TimeType["Milliseconds"] = 6] = "Milliseconds";
    })(TimeType = MateralTools.TimeType || (MateralTools.TimeType = {}));
    /**
     * 返回对象类型
     */
    var MResultType;
    (function (MResultType) {
        /**
         * 成功
         */
        MResultType[MResultType["Success"] = 0] = "Success";
        /**
         * 失败
         */
        MResultType[MResultType["Fail"] = 1] = "Fail";
        /**
         * 错误
         */
        MResultType[MResultType["Error"] = 2] = "Error";
    })(MResultType = MateralTools.MResultType || (MateralTools.MResultType = {}));
    /**
     * 分页模型
     */
    var MPageModel = /** @class */ (function () {
        function MPageModel() {
        }
        return MPageModel;
    }());
    MateralTools.MPageModel = MPageModel;
    /**
     * 返回模型
     */
    var MResultModel = /** @class */ (function () {
        function MResultModel() {
        }
        return MResultModel;
    }());
    MateralTools.MResultModel = MResultModel;
    /**
     * 携带分页数据的返回对象
     */
    var MResultPageModel = /** @class */ (function (_super) {
        __extends(MResultPageModel, _super);
        function MResultPageModel() {
            return _super !== null && _super.apply(this, arguments) || this;
        }
        return MResultPageModel;
    }(MResultModel));
    MateralTools.MResultPageModel = MResultPageModel;
    /**
     * 加密帮助类
     */
    var EncryptionManager = /** @class */ (function () {
        function EncryptionManager() {
        }
        /**
         * 获取32位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        EncryptionManager.Get32MD5Str = function (str, isLower) {
            if (isLower === void 0) { isLower = false; }
            function l(a) {
                return h(g(o(a), a.length * 8));
            }
            function m(e) {
                var b = "0123456789ABCDEF";
                if (isLower === true) {
                    b = b.toLowerCase();
                }
                var c = "";
                var d;
                for (var a_1 = 0; a_1 < e.length; a_1++) {
                    d = e.charCodeAt(a_1);
                    c += b.charAt(d >>> 4 & 15) + b.charAt(d & 15);
                }
                return c;
            }
            function n(d) {
                var b = "";
                var c = -1;
                var a, e;
                while (++c < d.length) {
                    a = d.charCodeAt(c),
                        e = c + 1 < d.length ? d.charCodeAt(c + 1) : 0;
                    55296 <= a && a <= 56319 && 56320 <= e && e <= 57343 && (a = 65536 + ((a & 1023) << 10) + (e & 1023), c++);
                    a <= 127 ? b += String.fromCharCode(a) : a <= 2047 ? b += String.fromCharCode(192 | a >>> 6 & 31, 128 | a & 63) : a <= 65535 ? b += String.fromCharCode(224 | a >>> 12 & 15, 128 | a >>> 6 & 63, 128 | a & 63) : a <= 2097151 && (b += String.fromCharCode(240 | a >>> 18 & 7, 128 | a >>> 12 & 63, 128 | a >>> 6 & 63, 128 | a & 63));
                }
                return b;
            }
            function o(c) {
                var b = Array(c.length >> 2);
                for (var a_2 = 0; a_2 < b.length; a_2++) {
                    b[a_2] = 0;
                }
                for (var a_3 = 0; a_3 < c.length * 8; a_3 += 8) {
                    b[a_3 >> 5] |= (c.charCodeAt(a_3 / 8) & 255) << a_3 % 32;
                }
                return b;
            }
            function h(c) {
                var b = "";
                for (var a_4 = 0; a_4 < c.length * 32; a_4 += 8) {
                    b += String.fromCharCode(c[a_4 >> 5] >>> a_4 % 32 & 255);
                }
                return b;
            }
            function g(j, l) {
                j[l >> 5] |= 128 << l % 32, j[(l + 64 >>> 9 << 4) + 14] = l;
                var g = 1732584193;
                var h = -271733879;
                var i = -1732584194;
                var f = 271733878;
                for (var k = 0; k < j.length; k += 16) {
                    var n_1 = g;
                    var o_1 = h;
                    var p = i;
                    var m_1 = f;
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
                    g = e(g, n_1);
                    h = e(h, o_1);
                    i = e(i, p);
                    f = e(f, m_1);
                }
                return Array(g, h, i, f);
            }
            function f(a, b, c, d, f, g) {
                return e(j(e(e(b, a), e(d, g)), f), c);
            }
            function a(b, a, c, d, e, g, h) {
                return f(a & c | ~a & d, b, a, e, g, h);
            }
            function b(c, a, d, b, e, g, h) {
                return f(a & b | d & ~b, c, a, e, g, h);
            }
            function c(b, a, c, d, e, g, h) {
                return f(a ^ c ^ d, b, a, e, g, h);
            }
            function d(b, a, c, d, e, g, h) {
                return f(c ^ (a | ~d), b, a, e, g, h);
            }
            function e(b, c) {
                var a = (b & 65535) + (c & 65535);
                var d = (b >> 16) + (c >> 16) + (a >> 16);
                return d << 16 | a & 65535;
            }
            function j(a, b) {
                return a << b | a >>> 32 - b;
            }
            return m(l(n(str)));
        };
        /**
         * 获取16位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        EncryptionManager.Get16MD5Str = function (str, isLower) {
            if (isLower === void 0) { isLower = false; }
            return this.Get32MD5Str(str, isLower).substr(8, 16);
        };
        /**
         * 转换为二进制字符串
         * @param str 要转换的字符串
         * @returns 转换后的字符串
         */
        EncryptionManager.ConvertToBinary = function (str) {
            var StrList = Array.prototype.map.call(str, function (c) {
                return c.charCodeAt(0).toString(2);
            });
            var resStr = "";
            for (var i = 0; i < StrList.length; i++) {
                resStr += StrList[i].MPadLeft(8, "0");
            }
            return resStr;
        };
        /**
         * 隐藏代码
         * @param codeStr 要隐藏的代码
         * @returns 隐藏后的代码
         */
        EncryptionManager.HideCode = function (codeStr) {
            var resStr = this.ConvertToBinary(codeStr);
            resStr = resStr.replace(/0/g, "\u200d");
            resStr = resStr.replace(/1/g, "\u200c");
            return resStr;
        };
        /**
         * 显示代码
         * @param codeStr 被隐藏的代码
         * @returns 显示的代码
         */
        EncryptionManager.ShowCode = function (codeStr) {
            var resStr = codeStr.replace(/.{8}/g, function (u) {
                return String.fromCharCode(parseInt(u.replace(/\u200c/g, "1").replace(/\u200d/g, "0"), 2));
            });
            return resStr;
        };
        return EncryptionManager;
    }());
    MateralTools.EncryptionManager = EncryptionManager;
    /**
     * HttpMethod枚举
     */
    var HttpMethod;
    (function (HttpMethod) {
        HttpMethod["GET"] = "get";
        HttpMethod["POST"] = "post";
    })(HttpMethod = MateralTools.HttpMethod || (MateralTools.HttpMethod = {}));
    /**
     * HTTP头内容类型
     */
    var HTTPHeadContentType;
    (function (HTTPHeadContentType) {
        HTTPHeadContentType["FormData"] = "multipart/form-data";
        HTTPHeadContentType["FormUrlencoded"] = "application/x-www-form-urlencoded";
        HTTPHeadContentType["Text"] = "text/plain";
        HTTPHeadContentType["Json"] = "application/json";
        HTTPHeadContentType["JavasScript"] = "application/javascript";
        HTTPHeadContentType["XML"] = "application/xml";
        HTTPHeadContentType["HTML"] = "text/html";
    })(HTTPHeadContentType = MateralTools.HTTPHeadContentType || (MateralTools.HTTPHeadContentType = {}));
    /**
     * Http配置类
     */
    var HttpConfigModel = /** @class */ (function () {
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
        function HttpConfigModel(url, method, data, dataType, success, error, complete) {
            if (method === void 0) { method = HttpMethod.POST; }
            if (data === void 0) { data = null; }
            if (dataType === void 0) { dataType = HTTPHeadContentType.Json; }
            if (success === void 0) { success = null; }
            if (error === void 0) { error = null; }
            if (complete === void 0) { complete = null; }
            /**
             * 超时时间
             */
            this.timeout = 15000;
            /**
             * 异步发送
             */
            this.async = true;
            this.url = url;
            this.method = method;
            this.data = data;
            this.ContentType = dataType;
            this.success = success;
            this.error = error;
            this.complete = complete;
        }
        return HttpConfigModel;
    }());
    MateralTools.HttpConfigModel = HttpConfigModel;
    /**
     * Http帮助类
     */
    var HttpManager = /** @class */ (function () {
        function HttpManager() {
        }
        /**
         * 获取XMLHttpRequest对象
         * @param config 配置对象
         * @returns HttpRequest对象
         */
        HttpManager.GetHttpRequest = function (config) {
            var xhr;
            if (window["XMLHttpRequest"]) {
                xhr = new XMLHttpRequest();
            }
            else {
                xhr = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhr.onreadystatechange = function () {
                HttpManager.Readystatechange(xhr, config);
            };
            return xhr;
        };
        /**
         * 状态更改方法
         * @param xhr XMLHttpRequest对象
         * @param config 配置对象
         */
        HttpManager.Readystatechange = function (xhr, config) {
            if (xhr.readyState == 4) {
                var resM = void 0;
                try {
                    resM = JSON.parse(xhr.responseText);
                }
                catch (ex) {
                    resM = xhr.responseText;
                }
                if ((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304) {
                    if (config.complete) {
                        config.complete(resM, xhr, xhr.status);
                    }
                    if (config.success) {
                        config.success(resM, xhr, xhr.status);
                    }
                }
                else {
                    if (config.complete) {
                        config.complete(resM, xhr, xhr.status);
                    }
                    if (config.error) {
                        config.error(resM, xhr, xhr.status);
                    }
                }
            }
        };
        /**
         * 序列化参数
         * @param data 要序列化的参数
         * @returns 序列化后的字符串
         */
        HttpManager.Serialize = function (data) {
            var result = new Array();
            var value = "";
            for (var name_1 in data) {
                if (typeof data[name_1] === "function") {
                    continue;
                }
                if (MateralTools.ToolsManager.MGetType(data[name_1]) == "Object") {
                    result.push(this.Serialize(data[name_1]));
                }
                else {
                    name_1 = encodeURIComponent(name_1);
                    if (data[name_1]) {
                        value = data[name_1].toString();
                        value = encodeURIComponent(value);
                    }
                    else {
                        value = "";
                    }
                    result.push(name_1 + "=" + value);
                }
            }
            ;
            return result.join("&");
        };
        /**
         * 发送Post请求
         * @param config 配置对象
         */
        HttpManager.SendPost = function (config) {
            var xhr = this.GetHttpRequest(config);
            if (config.ContentType == HTTPHeadContentType.FormUrlencoded) {
                config.url += "?" + HttpManager.Serialize(config.data);
            }
            xhr.open(config.method, config.url, config.async);
            xhr.setRequestHeader("Content-type", config.ContentType);
            if (config.data) {
                switch (config.ContentType) {
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
            else {
                xhr.send(null);
            }
        };
        /**
         * 发送Get请求
         * @param config 配置对象
         */
        HttpManager.SendGet = function (config) {
            var xhr = HttpManager.GetHttpRequest(config);
            var url = config.url;
            if (config.data) {
                url += "?" + HttpManager.Serialize(config.data);
            }
            xhr.open(config.method, url, config.async);
            xhr.setRequestHeader("Content-type", HTTPHeadContentType.FormUrlencoded);
            xhr.send(null);
        };
        /**
         * 发送请求
         * @param config 配置对象
         */
        HttpManager.Send = function (config) {
            if (config.method == HttpMethod.POST) {
                HttpManager.SendPost(config);
            }
            else {
                HttpManager.SendGet(config);
            }
        };
        return HttpManager;
    }());
    MateralTools.HttpManager = HttpManager;
    /**
     * 本地存储帮助类
     */
    var LocalDataManager = /** @class */ (function () {
        function LocalDataManager() {
        }
        /**
         * 是否支持本地存储
         * @returns 是否支持
         */
        LocalDataManager.IsLocalStorage = function () {
            if (window.localStorage) {
                return true;
            }
            else {
                return false;
            }
        };
        /**
         * 清空本地存储对象
         */
        LocalDataManager.CleanLocalData = function () {
            if (this.IsLocalStorage()) {
                window.localStorage.clear();
            }
        };
        /**
         * 移除本地存储对象
         * @param key Key值
         */
        LocalDataManager.RemoveLocalData = function (key) {
            if (this.IsLocalStorage() && key) {
                window.localStorage.removeItem(key);
            }
        };
        /**
         * 设置本地存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        LocalDataManager.SetLocalData = function (key, value, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (this.IsLocalStorage() && key && value) {
                this.RemoveLocalData(key);
                if (isJson) {
                    window.localStorage.setItem(key, JSON.stringify(value));
                }
                else {
                    window.localStorage.setItem(key, value.toString());
                }
            }
        };
        /**
         * 获取本地存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据
         */
        LocalDataManager.GetLocalData = function (key, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (this.IsLocalStorage() == true && key) {
                if (isJson) {
                    return JSON.parse(window.localStorage.getItem(key));
                }
                else {
                    return window.localStorage.getItem(key);
                }
            }
            return null;
        };
        /**
         * 是否支持网页存储
         * @returns 是否支持
         */
        LocalDataManager.IsSessionStorage = function () {
            if (window.sessionStorage) {
                return true;
            }
            else {
                return false;
            }
        };
        /**
         * 清空网页存储对象
         */
        LocalDataManager.CleanSessionData = function () {
            if (this.IsSessionStorage() == true) {
                window.sessionStorage.clear();
            }
        };
        /**
         * 移除网页存储对象
         * @param key Key值
         */
        LocalDataManager.RemoveSessionData = function (key) {
            if (this.IsSessionStorage() == true && key) {
                window.sessionStorage.removeItem(key);
            }
        };
        /**
         * 设置网页存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        LocalDataManager.SetSessionData = function (key, value, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (!isJson && isJson != false) {
                isJson = true;
            }
            if (this.IsSessionStorage() && key && value) {
                this.RemoveSessionData(key);
                if (isJson) {
                    window.sessionStorage.setItem(key, JSON.stringify(value));
                }
                else {
                    window.sessionStorage.setItem(key, value.toString());
                }
            }
        };
        /**
         * 获取网页存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据
         */
        LocalDataManager.GetSessionData = function (key, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (this.IsSessionStorage() == true && key) {
                if (isJson) {
                    return JSON.parse(window.sessionStorage.getItem(key));
                }
                else {
                    return window.sessionStorage.getItem(key);
                }
            }
            return null;
        };
        /**
         * 获得有效时间
         * @param timeValue 值
         * @param timeType 单位
         * @returns 计算后的时间
         */
        LocalDataManager.GetTime = function (timeValue, timeType) {
            if (timeValue === void 0) { timeValue = 10000; }
            if (timeType === void 0) { timeType = TimeType.Minutes; }
            switch (timeType) {
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
        };
        /**
         * 设置一个Cookie
         * @param key Key值
         * @param value 要保存的值
         * @param time 持续时间
         * @param timeType 单位(默认s[秒])
         */
        LocalDataManager.SetCookie = function (key, value, isJson, timeValue, timeType, path) {
            if (isJson === void 0) { isJson = true; }
            if (timeValue === void 0) { timeValue = 60; }
            if (timeType === void 0) { timeType = TimeType.Minutes; }
            if (path === void 0) { path = "/"; }
            if (isJson) {
                document.cookie = key + "=" + JSON.stringify(value) + ";max-age=" + this.GetTime(timeValue, timeType) + ";path=" + path;
            }
            else {
                document.cookie = key + "=" + value + ";max-age=" + this.GetTime(timeValue, timeType) + ";path=" + path;
            }
        };
        /**
         * 删除一个Cookie
         * @param key Key值
         */
        LocalDataManager.RemoveCookie = function (key) {
            document.cookie = key + "=;max-age=0";
        };
        /**
         * 获得所有Cookie
         * @returns Cookie对象
         */
        LocalDataManager.GetAllCookie = function () {
            var cookies = document.cookie.split(";");
            var cookie = new Array();
            var LocalCookie = new Object();
            for (var i = 0; i < cookies.length; i++) {
                if (!cookies[i].MIsNullOrUndefinedOrEmpty()) {
                    cookie[i] = cookies[i].trim().split("=");
                    if (cookie[i][0] && cookie[i][1]) {
                        LocalCookie[cookie[i][0]] = cookie[i][1];
                    }
                }
            }
            return LocalCookie;
        };
        /**
         * 获得Cookie
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns
         */
        LocalDataManager.GetCookie = function (key, isJson) {
            if (isJson === void 0) { isJson = true; }
            var resM = this.GetAllCookie();
            if (isJson && resM && resM[key]) {
                return JSON.parse(resM[key]);
            }
            else {
                return null;
            }
        };
        /**
         * 设置数据
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         * @param time 时间
         * @param timeType 时间类型
         */
        LocalDataManager.SetData = function (key, value, isJson, time, timeType) {
            if (isJson === void 0) { isJson = true; }
            if (time === void 0) { time = 60; }
            if (timeType === void 0) { timeType = TimeType.Minutes; }
            if (this.IsLocalStorage()) {
                this.SetLocalData(key, value, isJson);
            }
            else {
                this.SetCookie(key, value, isJson, time, timeType);
            }
        };
        /**
         * 获得数据
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns 获取到的数据
         */
        LocalDataManager.GetData = function (key, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (this.IsLocalStorage()) {
                return this.GetLocalData(key, isJson);
            }
            else {
                return this.GetCookie(key, isJson);
            }
        };
        /**
         * 移除数据
         * @param key Key值
         */
        LocalDataManager.RemoveData = function (key) {
            if (this.IsLocalStorage()) {
                this.RemoveLocalData(key);
            }
            else {
                this.RemoveCookie(key);
            }
        };
        return LocalDataManager;
    }());
    MateralTools.LocalDataManager = LocalDataManager;
    /**
     * 实现引擎模型
     */
    var EngineInfoModel = /** @class */ (function () {
        function EngineInfoModel() {
            //是否为Trident引擎
            this.Trident = false;
            //是否为Gecko引擎
            this.Gecko = false;
            //是否为WebKit引擎
            this.WebKit = false;
            //是否为KHTML引擎
            this.KHTML = false;
            //是否为Presto引擎
            this.Presto = false;
            //具体版本号
            this.Version = "";
        }
        return EngineInfoModel;
    }());
    MateralTools.EngineInfoModel = EngineInfoModel;
    /**
     * 浏览器模型
     */
    var BrowserInfoModel = /** @class */ (function () {
        function BrowserInfoModel() {
            //是否为IE浏览器
            this.IE = false;
            //是否为Firefox浏览器
            this.Firefox = false;
            //是否为Safari浏览器
            this.Safari = false;
            //是否为Konqueror浏览器
            this.Konqueror = false;
            //是否为Opera浏览器
            this.Opera = false;
            //是否为Chrome浏览器
            this.Chrome = false;
            //是否为Edge浏览器
            this.Edge = false;
            //是否为QQ浏览器
            this.QQ = false;
            //是否为UC浏览器
            this.UC = false;
            //是否为Maxthon(遨游)浏览器
            this.Maxthon = false;
            //是否为微信浏览器
            this.WeChat = false;
            //具体版本号
            this.Version = "";
        }
        return BrowserInfoModel;
    }());
    MateralTools.BrowserInfoModel = BrowserInfoModel;
    /**
     * 系统模型
     */
    var SystemInfoModel = /** @class */ (function () {
        function SystemInfoModel() {
            //是否为Windows操作系统
            this.Windows = false;
            //是否为WindowsMobile操作系统
            this.WindowsMobile = false;
            //Windows版本
            this.WindowsVersion = "";
            //是否为Mac操作系统
            this.Mac = false;
            //是否为Unix操作系统
            this.Unix = false;
            //是否为Linux操作系统
            this.Linux = false;
            //是否为iPhone操作系统
            this.iPhone = false;
            //是否为iPod操作系统
            this.iPod = false;
            //是否为Windows操作系统
            this.iPad = false;
            //是否为Windows操作系统
            this.IOS = false;
            //IOS版本
            this.IOSVersion = "";
            //是否为Android操作系统
            this.Android = false;
            //Android版本
            this.AndroidVersion = "";
            //是否为NokiaN操作系统
            this.NokiaN = false;
            //是否为Wii操作系统
            this.Wii = false;
            //是否为PS操作系统
            this.PS = false;
        }
        return SystemInfoModel;
    }());
    MateralTools.SystemInfoModel = SystemInfoModel;
    /**
     * 客户端信息模型
     */
    var ClientInfoModel = /** @class */ (function () {
        /**
         * 客户端信息模型
         */
        function ClientInfoModel() {
            this._engineM = new EngineInfoModel();
            this._browserM = new BrowserInfoModel();
            this._systemM = new SystemInfoModel();
            //检测呈现引擎和浏览器
            var userAgent = navigator.userAgent;
            if (window["opera"]) {
                this._engineM.Version = this._engineM.Version = window["opera"].version();
                this._engineM.Presto = this._browserM.Opera = true;
            }
            else if (/AppleWebKit\/(\S+)/.test(userAgent)) {
                this._engineM.Version = RegExp["$1"];
                this._engineM.WebKit = true;
                if (/MicroMessenger\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.WeChat = true;
                }
                else if (/Edge\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Edge = true;
                }
                else if (/QQBrowser\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.QQ = true;
                }
                else if (/UBrowser\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.UC = true;
                }
                else if (/Maxthon\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Maxthon = true;
                }
                else if (/Chrome\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Chrome = true;
                }
                else if (/Safari\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Safari = true;
                }
                else {
                    if (this._engineM.WebKit) {
                        var safariVersion = "";
                        var WebKitVersion = parseInt(this._engineM.Version);
                        if (WebKitVersion < 100) {
                            safariVersion = "1";
                        }
                        else if (WebKitVersion < 312) {
                            safariVersion = "1.2";
                        }
                        else if (WebKitVersion < 412) {
                            safariVersion = "1.3";
                        }
                        else {
                            safariVersion = "2";
                        }
                        this._browserM.Version = safariVersion;
                        this._browserM.Safari = true;
                    }
                }
            }
            else if (/KHTML\/(\S+)/.test(userAgent) || /Konqueror\/([^;]+)/.test(userAgent)) {
                this._engineM.Version = this._browserM.Version = RegExp["$1"];
                this._engineM.KHTML = this._browserM.Konqueror = true;
            }
            else if (/rv:([^\)]+)\) Gecko\/\d{8}/.test(userAgent)) {
                this._engineM.Version = RegExp["$1"];
                this._engineM.Gecko = true;
                if (/Firefox\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Firefox = true;
                }
            }
            else if (/MSIE ([^;]+)/.test(userAgent)) {
                this._engineM.Version = this._browserM.Version = RegExp["$1"];
                this._engineM.Trident = this._browserM.IE = true;
            }
            else {
                if (window["ActiveXObject"] || "ActiveXObject" in window) {
                    this._engineM.Version = this._browserM.Version = "11";
                    this._engineM.Trident = this._browserM.IE = true;
                }
            }
            //检测平台
            var p = navigator.platform;
            this._systemM.Windows = p.indexOf("Win") == 0;
            if (this._systemM.Windows) {
                if (/Win(?:dows )?([^do]{2})\s?(\d+\.\d+)?/.test(userAgent)) {
                    if (RegExp["$1"] == "NT") {
                        switch (RegExp["$2"]) {
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
                    else if (RegExp["$1"] == "9X") {
                        this._systemM.WindowsVersion = "ME";
                    }
                    else {
                        this._systemM.WindowsVersion = RegExp["$1"];
                    }
                }
                if (this._systemM.WindowsVersion == "CE") {
                    this._systemM.WindowsMobile = true;
                }
                else if (this._systemM.WindowsVersion == "Ph") {
                    if (/Windows Phone OS (\d+.\d+)/.test(userAgent)) {
                        this._systemM.WindowsMobile = true;
                    }
                }
            }
            this._systemM.Mac = p.indexOf("Mac") == 0;
            if (this._systemM.Mac && userAgent.indexOf("Mobile") > -1) {
                if (/CPU (?:iPhone)?OS (\d+_\d+)/.test(userAgent)) {
                    this._systemM.IOS = true;
                    this._systemM.IOSVersion = RegExp["$1"].replace("_", ".");
                }
                else {
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
            if (/Android (\d+\.\d+)/.test(userAgent)) {
                this._systemM.Android = true;
                this._systemM.AndroidVersion = RegExp["$1"];
            }
        }
        Object.defineProperty(ClientInfoModel.prototype, "EngineInfoM", {
            /**
             * 实现引擎信息
             */
            get: function () {
                return MateralTools.ToolsManager.Clone(this._engineM);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(ClientInfoModel.prototype, "BrowserInfoM", {
            /**
             * 浏览器信息
             */
            get: function () {
                return MateralTools.ToolsManager.Clone(this._browserM);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(ClientInfoModel.prototype, "SystemInfoM", {
            /**
             * 系统信息
             */
            get: function () {
                return MateralTools.ToolsManager.Clone(this._systemM);
            },
            enumerable: true,
            configurable: true
        });
        return ClientInfoModel;
    }());
    MateralTools.ClientInfoModel = ClientInfoModel;
})(MateralTools || (MateralTools = {}));
//# sourceMappingURL=m-tools.js.map