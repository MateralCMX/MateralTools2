'use strict';
var MateralTools;
(function (MateralTools) {
    /**
     * 普通工具类
     */
    var ToolManager = /** @class */ (function () {
        function ToolManager() {
        }
        /**
         * 判断对象是否为Null
         * @param obj 任意值或对象
         * @returns 是否为Null
         */
        ToolManager.IsNull = function (obj) {
            return obj === null;
        };
        /**
         * 判断对象是否为Undefined
         * @param obj 任意值或对象
         * @returns 是否为Undefined
         */
        ToolManager.IsUndefined = function (obj) {
            return typeof obj === "undefined";
        };
        /**
         * 判断对象是否为Null或Undefined
         * @param obj 任意值或对象
         * @returns 是否为Null或Undefined
         */
        ToolManager.IsNullOrUndefined = function (obj) {
            return this.IsNull(obj) || this.IsUndefined(obj);
        };
        /**
         * 判断字符串是否为空字符串
         * @param str 字符串
         * @returns 是否为空字符串
         */
        ToolManager.IsEmpty = function (str) {
            return str === "";
        };
        /**
         * 判断字符串是否为Null或Undefined或空字符串
         * @param str 字符串
         * @returns 是否为Null或Undefined或空字符串
         */
        ToolManager.IsNullOrUndefinedOrEmpty = function (str) {
            return this.IsNull(str) || this.IsUndefined(str) || this.IsEmpty(str);
        };
        /**
         * 鉴别类型
         * @param obj 传入对象
         * @param IncludeCustom 包括自定义类型
         * @returns 对象类型
         */
        ToolManager.GetType = function (obj, IncludeCustom) {
            if (IncludeCustom === void 0) { IncludeCustom = true; }
            var resStr = typeof obj;
            if (resStr === "object") {
                if (this.IsNull(obj)) {
                    resStr = "null";
                }
                else {
                    resStr = Object.prototype.toString.call(obj).slice(8, -1);
                    if (resStr === "Object" && !this.IsNullOrUndefined(obj.constructor) && obj.constructor.name != "Object" && IncludeCustom) {
                        resStr = obj.constructor.name;
                    }
                }
            }
            return resStr.toLowerCase();
        };
        /**
         * 删除字符串两端空格
         * @param str 要删除空格的字符串
         * @returns 已删除空格的字符串
         */
        ToolManager.Trim = function (str) {
            if (this.IsNullOrUndefined(String.prototype.trim)) {
                while (str.substr(0, 1) === " ") {
                    str = str.substr(1, str.length - 1);
                }
                while (str.substr(str.length - 2, 1) === " ") {
                    str = str.substr(0, str.length - 2);
                }
            }
            else {
                str = str.trim();
            }
            return str;
        };
        /**
         * 获得URL参数
         * @returns URL参数
         */
        ToolManager.GetURLParams = function () {
            var params = new Object();
            var paramsStr = window.location.search;
            var paramsStrs = new Array();
            if (!this.IsNullOrUndefinedOrEmpty(paramsStr)) {
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
        /**
         * 左侧填充字符
         * @param str 原字符串
         * @param length 位数
         * @param character 填充字符
         */
        ToolManager.PadLeft = function (str, length, character) {
            if (character === void 0) { character = " "; }
            for (var i = str.length; i < length; i++) {
                str = character + str;
            }
            return str;
        };
        /**
         * 右侧填充字符
         * @param str 原字符串
         * @param length 位数
         * @param character 填充字符
         */
        ToolManager.PadRight = function (str, length, character) {
            if (character === void 0) { character = " "; }
            for (var i = str.length; i < length; i++) {
                str = str + character;
            }
            return str;
        };
        /**
         * 获得时间差
         * @param date1 时间1
         * @param date2 时间2
         * @param TimeType 返回类型[ms毫秒s秒m分钟H小时D天数]
         */
        ToolManager.GetTimeDifference = function (date1, date2, timeType) {
            if (timeType === void 0) { timeType = TimeType.Seconds; }
            var timeDifference = date1.getTime() - date2.getTime();
            switch (timeType) {
                case TimeType.Day:
                    timeDifference = Math.floor(timeDifference / (24 * 3600 * 1000));
                    break;
                case TimeType.Hours:
                    timeDifference = Math.floor(timeDifference / (3600 * 1000));
                    break;
                case TimeType.Minutes:
                    timeDifference = Math.floor(timeDifference / (60 * 1000));
                    break;
                case TimeType.Seconds:
                    timeDifference = Math.floor(timeDifference / 1000);
                    break;
                case TimeType.Milliseconds:
                    timeDifference = timeDifference;
                    break;
                default:
            }
            return timeDifference;
        };
        /**
         * 时间字符串格式化
         * @param dateTime 时间对象
         * @param formatStr 格式化字符串
         */
        ToolManager.DateTimeFormat = function (dateTime, formatStr) {
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
        /**
         * 获取Input dateTime设置值字符串
         * @param dateTime 要设置的时间
         */
        ToolManager.GetInputDateTimeValueStr = function (dateTime) {
            return ToolManager.DateTimeFormat(dateTime, "yyyy-MM-ddTHH:mm:ss");
        };
        /**
         * 获取对应时区时间
         * @param date 时间对象
         * @param timeZone 时区
         */
        ToolManager.GetDateByTimeZone = function (date, timeZone) {
            if (date === void 0) { date = new Date(); }
            if (timeZone === void 0) { timeZone = 8; }
            var len = date.getTime();
            var offset = date.getTimezoneOffset() * 60000;
            var utcTime = len + offset;
            date = new Date(utcTime + 3600000 * timeZone);
            return date;
        };
        /**
         * 将其他时区时间设置为对应时区时间
         * @param date 时间对象
         * @param timeZone 时区
         */
        ToolManager.SetLocalTime = function (date, timeZone) {
            if (date === void 0) { date = new Date(); }
            if (timeZone === void 0) { timeZone = date.getTimezoneOffset() / 60; }
            date.setTime(date.getTime() - timeZone * 60 * 60 * 1000);
            return date;
        };
        /**
         * 获取滚动条位置
         */
        ToolManager.GetScrollTop = function () {
            var scrollTop = 0;
            if (document.documentElement && document.documentElement.scrollTop) {
                scrollTop = document.documentElement.scrollTop;
            }
            else if (document.body) {
                scrollTop = document.body.scrollTop;
            }
            return scrollTop;
        };
        /**
         * 获取可见高度
         */
        ToolManager.GetClientHeight = function () {
            var clientHeight = 0;
            if (document.body.clientHeight && document.documentElement.clientHeight) {
                clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            }
            else {
                clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            }
            return clientHeight;
        };
        return ToolManager;
    }());
    MateralTools.ToolManager = ToolManager;
    /**
     * DOM帮助类
     */
    var DOMManager = /** @class */ (function () {
        function DOMManager() {
        }
        /**
         * 根据页面元素对象获得页面元素对象
         * @param element 页面元素
         * @returns 页面元素对象
         */
        DOMManager.$ = function (element) {
            if (ToolManager.GetType(element) === "string") {
                element = document.getElementById(element);
            }
            else {
                element = element;
            }
            return element;
        };
        /**
         * 设置样式
         * @param element 页面元素
         * @param className 要设置的样式列表
         */
        DOMManager.SetClass = function (element, className) {
            element = this.$(element);
            if (!ToolManager.IsNullOrUndefined(element)) {
                var classStr = "";
                var TypeStr = ToolManager.GetType(className);
                var ClassList = void 0;
                if (TypeStr === "string") {
                    classStr = className.replace(/\s{2,}/g, " ");
                    classStr = ToolManager.Trim(classStr);
                }
                else if (TypeStr === "Array") {
                    classStr = className.join(" ");
                }
                if (!ToolManager.IsNullOrUndefinedOrEmpty(classStr)) {
                    element.setAttribute("class", classStr);
                }
                else {
                    element.removeAttribute("class");
                }
            }
        };
        /**
         * 添加样式
         * @param id 页面元素ID
         * @param className 要添加的样式
         */
        DOMManager.AddClass = function (element, className) {
            element = this.$(element);
            if (ToolManager.GetType(className) === "string") {
                className = className.split(" ");
            }
            for (var i = 0; i < className.length; i++) {
                element.classList.add(className[i]);
            }
        };
        /**
         * 删除样式
         * @param element 页面元素
         * @param className 要删除的样式列表
         */
        DOMManager.RemoveClass = function (element, className) {
            element = this.$(element);
            if (ToolManager.GetType(className) === "string") {
                className = className.split(" ");
            }
            for (var i = 0; i < className.length; i++) {
                element.classList.remove(className[i]);
            }
        };
        /**
         * 是否有拥有样式
         * @param element 页面元素
         * @param className 要查找的样式列表
         * @returns 查询结果
         */
        DOMManager.HasClass = function (element, className) {
            var resM = true;
            element = this.$(element);
            if (ToolManager.GetType(className) === "string") {
                className = className.split(" ");
            }
            for (var i = 0; i < className.length && resM; i++) {
                resM = element.classList.contains(className[i]);
            }
            return resM;
        };
        /**
         * 根据ClassName获得元素对象
         * @param element 父元素
         * @param className ClassName
         * @returns Element集合
         */
        DOMManager.GetElementsByClassName = function (element, className) {
            element = this.$(element);
            var resultM = new Array();
            var elements;
            if (!ToolManager.IsNullOrUndefined(element)) {
                if (!ToolManager.IsNullOrUndefined(element["getElementsByClassName"])) {
                    elements = element["getElementsByClassName"](className);
                    return elements;
                }
                else {
                    elements = element.getElementsByTagName("*");
                    for (var i = 0; i < elements.length; i++) {
                        if (this.HasClass(elements[i], className)) {
                            resultM.push(elements[i]);
                        }
                    }
                    return resultM;
                }
            }
            return null;
        };
        /**
         * 根据Name获得元素对象
         * @param element 父元素
         * @param name Name
         * @returns Element集合
         */
        DOMManager.GetElementsByName = function (element, name) {
            element = this.$(element);
            var resultM = new Array();
            var elements;
            if (!ToolManager.IsNullOrUndefined(element)) {
                if (!ToolManager.IsNullOrUndefined(element["getElementsByName"])) {
                    elements = element["getElementsByName"](name);
                    return elements;
                }
                else {
                    elements = element.getElementsByTagName("*");
                    for (var i = 0; i < elements.length; i++) {
                        if (elements[i].name && elements[i].name == name) {
                            resultM.push(elements[i]);
                        }
                    }
                    return resultM;
                }
            }
            return null;
        };
        /**
         * 获得事件触发元素
         * @param event 事件对象
         * @returns 触发元素
         */
        DOMManager.GetEventTarget = function (event) {
            return event["srcElement"] || event["target"];
        };
        /**
         * 添加事件
         * @param thisWindow window对象
         * @param type 事件类型
         * @param fun 执行方法
         */
        DOMManager.AddEvent = function (element, type, fun) {
            var typeName = ToolManager.GetType(element);
            if (ToolManager.GetType(element) === "string" || ToolManager.GetType(element) === "HTMLElement") {
                element = this.$(element);
            }
            if (!ToolManager.IsNullOrUndefined(element)) {
                if (!ToolManager.IsNullOrUndefined(element["addEventListener"])) {
                    element["addEventListener"](type, fun);
                }
                else {
                    element["attachEvent"]("on" + type, fun);
                }
            }
        };
        /**
         * 获得子节点
         * @param element 父元素
         * @returns 子节点
         */
        DOMManager.GetChildren = function (element) {
            var children;
            element = this.$(element);
            if (!ToolManager.IsNullOrUndefined(element)) {
                if (element["children"]) {
                    children = element["children"];
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
            }
            return children;
        };
        /**
         * 获得自定义属性
         * @param element 父节点
         * @returns 自定义属性
         */
        DOMManager.GetDataSet = function (element) {
            var DataSet;
            element = this.$(element);
            if (!ToolManager.IsNullOrUndefined(element)) {
                if (!ToolManager.IsNullOrUndefined(element.dataset)) {
                    DataSet = element.dataset;
                }
                else {
                    DataSet = new Object();
                    var length_2 = element.attributes.length;
                    var item = void 0;
                    for (var i = 0; i < length_2; i++) {
                        item = element.attributes[i];
                        if (!ToolManager.IsNullOrUndefined(item.specified) && /^data-/.test(item.nodeName)) {
                            DataSet[item.nodeName.substring(5)] = item.nodeValue;
                        }
                    }
                }
                return DataSet;
            }
        };
        /**
         * 获得元素的实际样式
         * @param element 元素
         * @returns 实际样式
         */
        DOMManager.GetComputedStyle = function (element) {
            element = this.$(element);
            var cssStyle;
            if (element["currentStyle"]) {
                cssStyle = element["currentStyle"];
            }
            else {
                cssStyle = getComputedStyle(element);
            }
            return cssStyle;
        };
        /**
         * 设置<input type="date|time|datetime|datetime-local">的值
         * @param element 要设置的对象
         * @param dateTime 要设置的时间
         */
        DOMManager.SetInputDateTimeValue = function (element, dateTime) {
            element = this.$(element);
            if (!ToolManager.IsNullOrUndefined(element)) {
                var elementType = element.getAttribute("type");
                if (elementType === "date" || elementType === "time" || elementType === "datetime" || elementType === "datetime-local") {
                    element.value = ToolManager.GetInputDateTimeValueStr(dateTime);
                }
            }
        };
        /**
         * 获得输入的值
         * @param element 元素对象
         */
        DOMManager.GetInputValue = function (element) {
            element = DOMManager.$(element);
            if (!ToolManager.IsNullOrUndefined(element["value"])) {
                return element["value"];
            }
            return null;
        };
        return DOMManager;
    }());
    MateralTools.DOMManager = DOMManager;
    /**
     * 数组帮助类
     */
    var ArrayManager = /** @class */ (function () {
        function ArrayManager() {
        }
        /**
         * 查询所在数组的位序
         * @param array 要查询的数组
         * @param item 要查询的对象
         * @returns 位序
         */
        ArrayManager.ArrayIndexOf = function (array, item, formIndex) {
            if (formIndex === void 0) { formIndex = 0; }
            var index = -1;
            if (ToolManager.IsNullOrUndefined(array.indexOf)) {
                for (var i = formIndex; i < array.length; i++) {
                    if (array[i] == item) {
                        index = i;
                    }
                }
            }
            else {
                index = array.indexOf(item, formIndex);
            }
            return index;
        };
        /**
         * 清空数组
         * @param array 要清空的数组
         * @returns 清空后的数组
         */
        ArrayManager.ArrayClear = function (array) {
            array.splice(0, array.length);
            return array;
        };
        /**
         * 插入数组
         * @param array 要插入的数组
         * @param index 要插入的对象
         * @returns 插入后的数组
         */
        ArrayManager.ArrayInsert = function (array, item, index) {
            array.splice(index, 0, item);
            return array;
        };
        /**
         * 删除数组
         * @param array 要删除的数组
         * @param index 要删除的位序
         * @returns 删除后的数组
         */
        ArrayManager.ArrayRemoveTo = function (array, index) {
            var count = array.length;
            array.splice(index, 1);
            if (count === array.length && count === 1) {
                array = [];
            }
            return array;
        };
        /**
         * 删除数组
         * @param array 要删除的数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        ArrayManager.ArrayRemove = function (array, item) {
            var index = this.ArrayIndexOf(array, item);
            if (index >= 0) {
                this.ArrayRemoveTo(array, index);
            }
            return array;
        };
        /**
         * 删除所有数组
         * @param array 要删除的数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        ArrayManager.ArrayRomeveAll = function (array, item) {
            var index = this.ArrayIndexOf(array, item);
            while (index >= 0) {
                this.ArrayRemoveTo(array, index);
                index = this.ArrayIndexOf(array, item);
            }
            return array;
        };
        return ArrayManager;
    }());
    MateralTools.ArrayManager = ArrayManager;
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
                resStr += ToolManager.PadLeft(StrList[i], 8, "0");
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
     * Http配置类
     */
    var HttpConfigModel = /** @class */ (function () {
        /**
         *
         * @param url
         * @param type
         * @param data
         * @param dataType
         * @param success
         * @param error
         * @param complete
         */
        function HttpConfigModel(url, type, data, dataType, success, error, complete) {
            if (type === void 0) { type = "post"; }
            if (data === void 0) { data = null; }
            if (dataType === void 0) { dataType = "json"; }
            if (success === void 0) { success = null; }
            if (error === void 0) { error = null; }
            if (complete === void 0) { complete = null; }
            //超时时间
            this.timeout = 15000;
            //异步发送
            this.async = true;
            this.url = url;
            this.type = type;
            this.data = data;
            this.dataType = dataType;
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
            if (!ToolManager.IsNullOrUndefined(window["XMLHttpRequest"])) {
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
                    resM = JsonManager.JSONParse(xhr.responseText);
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
        HttpManager.serialize = function (data) {
            var result = new Array();
            var value = "";
            for (var name_1 in data) {
                if (typeof data[name_1] === "function") {
                    continue;
                }
                if (ToolManager.GetType(data[name_1]) == "Object") {
                    result.push(this.serialize(data[name_1]));
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
            xhr.open(config.type, config.url, config.async);
            switch (config.dataType) {
                case "json":
                    xhr.setRequestHeader("Content-type", "application/json");
                    break;
                case "data":
                    break;
                default:
                    break;
            }
            if (config.data) {
                switch (config.dataType) {
                    case "json":
                        xhr.send(JsonManager.JSONStringify(config.data));
                        break;
                    case "data":
                        xhr.send(config.data);
                        break;
                    default:
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
            config.type = config.type.toLowerCase();
            var url = config.url;
            if (config.data) {
                url += "?" + HttpManager.serialize(config.data);
            }
            xhr.open(config.type, url, config.async);
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhr.send(null);
        };
        /**
         * 发送请求
         * @param config 配置对象
         */
        HttpManager.Send = function (config) {
            config.type = config.type.toLowerCase();
            if (config.type == "post") {
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
     *JSON帮助类
     */
    var JsonManager = /** @class */ (function () {
        function JsonManager() {
        }
        /**
         * Json字符串转换为Json对象
         * @param jsonStr Json字符串
         * @returns Json对象
         */
        JsonManager.JSONParse = function (jsonStr) {
            var resM;
            if (!ToolManager.IsNullOrUndefined(JSON.parse)) {
                resM = JSON.parse(jsonStr);
            }
            else {
                resM = eval("(" + jsonStr + ")");
            }
            return resM;
        };
        /**
         * Json对象转换为Json字符串
         * @param jsonObj Json对象
         * @returns Json字符串
         */
        JsonManager.JSONStringify = function (jsonObj) {
            var resM;
            if (!ToolManager.IsNullOrUndefined(JSON.stringify)) {
                resM = JSON.stringify(jsonObj);
            }
            else {
                var IsArray = void 0;
                var TypeStr = void 0;
                for (var key in jsonObj) {
                    IsArray = false;
                    TypeStr = ToolManager.GetType(jsonObj[key]);
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
            }
            return resM;
        };
        return JsonManager;
    }());
    MateralTools.JsonManager = JsonManager;
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
                    window.localStorage.setItem(key, JsonManager.JSONStringify(value));
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
                    return JsonManager.JSONParse(window.localStorage.getItem(key));
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
                    window.sessionStorage.setItem(key, JsonManager.JSONStringify(value));
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
                    return JsonManager.JSONParse(window.sessionStorage.getItem(key));
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
                document.cookie = key + "=" + JsonManager.JSONStringify(value) + ";max-age=" + this.GetTime(timeValue, timeType) + ";path=" + path;
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
                if (!ToolManager.IsNullOrUndefinedOrEmpty(cookies[i])) {
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
                return JsonManager.JSONParse(resM[key]);
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
     * 数学帮助类
     */
    var MathManager = /** @class */ (function () {
        function MathManager() {
        }
        /**
         * 返回一个随机数
         * @param min 最小值
         * @param max 最大值
         * @returns 随机数
         */
        MathManager.prototype.GetRandom = function (min, max) {
            return Math.floor(Math.random() * max + min);
        };
        /**
         * 获取四边形的外接圆半径
         * @param length 长
         * @param width 宽
         * @param IsRound 是圆形
         */
        MathManager.prototype.GetCircumcircleRadius = function (length, width, IsRound) {
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
        return MathManager;
    }());
    MateralTools.MathManager = MathManager;
    /**
     * 对象帮助类
     */
    var ObjectManager = /** @class */ (function () {
        function ObjectManager() {
        }
        /**
         * 克隆对象
         * @param obj 要克隆的对象
         */
        ObjectManager.Clone = function (obj) {
            var ObjectType = ToolManager.GetType(obj, false);
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
                var SubObjectType = ToolManager.GetType(copy, false);
                if (SubObjectType == "Object" || SubObjectType == "array") {
                    result[i] = arguments.callee(copy);
                }
                else {
                    result[i] = copy;
                }
            }
            return result;
        };
        return ObjectManager;
    }());
    MateralTools.ObjectManager = ObjectManager;
    /*时间类型*/
    var TimeType;
    (function (TimeType) {
        /*年*/
        TimeType[TimeType["Years"] = 0] = "Years";
        /*月*/
        TimeType[TimeType["Months"] = 1] = "Months";
        /*日*/
        TimeType[TimeType["Day"] = 2] = "Day";
        /*时*/
        TimeType[TimeType["Hours"] = 3] = "Hours";
        /*分*/
        TimeType[TimeType["Minutes"] = 4] = "Minutes";
        /*秒*/
        TimeType[TimeType["Seconds"] = 5] = "Seconds";
        /*毫秒*/
        TimeType[TimeType["Milliseconds"] = 6] = "Milliseconds";
    })(TimeType = MateralTools.TimeType || (MateralTools.TimeType = {}));
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
            if (!ToolManager.IsNullOrUndefined(window["opera"])) {
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
                if (!ToolManager.IsNullOrUndefined(window["ActiveXObject"]) || "ActiveXObject" in window) {
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
                return ObjectManager.Clone(this._engineM);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(ClientInfoModel.prototype, "BrowserInfoM", {
            /**
             * 浏览器信息
             */
            get: function () {
                return ObjectManager.Clone(this._browserM);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(ClientInfoModel.prototype, "SystemInfoM", {
            /**
             * 系统信息
             */
            get: function () {
                return ObjectManager.Clone(this._systemM);
            },
            enumerable: true,
            configurable: true
        });
        return ClientInfoModel;
    }());
    MateralTools.ClientInfoModel = ClientInfoModel;
})(MateralTools || (MateralTools = {}));
var MDMa = MateralTools.DOMManager;
var MTMa = MateralTools.ToolManager;
var MLMa = MateralTools.LocalDataManager;
//# sourceMappingURL=m-tools.js.map