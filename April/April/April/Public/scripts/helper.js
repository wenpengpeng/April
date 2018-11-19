/**
* 根据参数名从Url地址上获取参数值
* @param {} name 参数名
*/

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r)
        return unescape(r[2]);
    return null;
}

/**
* 从对象数组中删除属性为objPropery，值为objValue元素的对象
* @param Array arr  数组对象
* @param String objPropery  对象的属性
* @param String objPropery  对象的值
* @return Array 过滤后数组
*/
function remove(arr, objPropery, objValue) {
    return $.grep(arr, function (cur, i) {
        return cur[objPropery] != objValue;
    });
}

/**
* 从对象数组中获取属性为objPropery，值为objValue元素的对象
* @param Array arrPerson  数组对象
* @param String objPropery  对象的属性
* @param String objPropery  对象的值
* @return Array 过滤后的数组
*/
function get(arr, objPropery, objValue) {
    return $.grep(arr, function (cur, i) {
        return cur[objPropery] == objValue;
    });
}

/**
* JS数组分页
* @param {} pageNo  页码，默认第一页开始
* @param {} pageSize  页面大小
* @param {} array  需要分页的数据
* @returns {}
*/
function jsArrayPagination(pageNo, pageSize, array) {
    var offset = (pageNo - 1) * pageSize;
    return (offset + pageSize >= array.length) ? array.slice(offset, array.length) : array.slice(offset, offset + pageSize);
}

/**
* 数组求差集
* @param Array arr1  数组对象
* @param Array arr2  数组对象
*/
function difference(arr1, arr2) {
    var diff = [];
    var tmp = arr2;

    arr1.forEach(function (val1, i) {
        if (arr2.indexOf(val1) < 0) {
            diff.push(val1);
        } else {
            tmp.splice(tmp.indexOf(val1), 1);
        }
    });
    return diff.concat(tmp);
}

/**
* 获取链表
* @param string node 节点代码
* @param Array jsonArray json对象
* @param Array newArray  数组对象
*/
function getClist(node, jsonArray, newArray) {
    $.each(jsonArray, function (index, item) {
        if (item.from === node) {
            newArray.push(item);   //放入新数组中
            var jsonArrayTo = item.to;
            getClist(jsonArrayTo, jsonArray, newArray);
        }
    });
}

/**
* 生成GUID
*/
function generateGuid() {
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4"; // bits 12-15 of the time_hi_and_version field to 0010
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1); // bits 6-7 of the clock_seq_hi_and_reserved to 01
    s[8] = s[13] = s[18] = s[23] = "-";

    var uuid = s.join("");
    return uuid;
}

/**
 * 倒计时
 * @param {any} emment
 * @param {any} setDom
 * @param {any} callback
 */
function countDownSecond(emment, setDom, callback) {
    var me = $(emment);
    var time = 0;
    var datasecond = parseInt(me.attr("second"));
    me.attr("disabled", true);

    var timer = setInterval(function () {
        if (datasecond > 0) {
            datasecond -= 1;
            me.attr("second", datasecond);
            time = datasecond;
            setDom(time, me);
        } else {
            clearInterval(timer);
            if (callback != null) {
                me.attr("disabled", false);
                callback(me);
            } else {
                time = 0;
                setDom(time, me);
            }
        }
    }, 1000);
}

// #region 首页  大客户专用

/**
 * 倒计时
 * @param {any} emment
 * @param {any} setDom
 * @param {any} callback
 */
function countDownSecondAndTime(emment, setDom, callback) {
    var me = $(emment);
    var time = 0;
    var dataType = me.attr("data-type");
    var startSecond = parseInt(me.attr("startSecond"));
    var endSecond = parseInt(me.attr("endSecond"));
    me.attr("disabled", true);
    var timer = setInterval(function () {
        if (dataType === "1" && startSecond <= 1) {
            dataType = "2";
            me.attr("data-type", "2");
        }
        if (startSecond > 0 || endSecond > 0) {
            startSecond -= 1;
            me.attr("startSecond", startSecond);
            endSecond -= 1;
            me.attr("endSecond", endSecond);
            if (dataType === "1") {
                time = startSecond;
            } else if (dataType === "2") {
                time = endSecond;
            }
            setDom(time, me);
        } else {
            clearInterval(timer);
            if (callback != null) {
                me.attr("disabled", false);
                callback(me);
            } else {
                time = 0;
                setDom(time, me);
            }
        }
    }, 1000);
}

// #endregion

/**
 * 倒计时(列表)
 * @param {any} emment
 * @param {any} dom
 * @param {any} callback
 */
function countDown(emment, dom, callback) {
    $(emment).each(function () {
        var me = $(this);
        var timeArr = [];
        var datasecond = me.attr("datasecond");
        var timer = setInterval(function () {
            if (datasecond > 0) {
                datasecond -= 1;
                me.attr("datasecond", datasecond);
                var hour = Math.floor(datasecond / 3600);
                hour < 10 ? hour = "0" + hour : hour;
                var minute = Math.floor((datasecond - hour * 3600) / 60);
                minute < 10 ? minute = "0" + minute : minute;
                var second = Math.floor(datasecond - hour * 3600 - minute * 60);
                second < 10 ? second = "0" + second : second;
                timeArr[0] = hour;
                timeArr[1] = minute;
                timeArr[2] = second;
                dom(timeArr, me);
            } else {
                clearInterval(timer);
                if (callback != null) {
                    callback(me);
                } else {
                    timeArr[0] = 0;
                    timeArr[1] = 0;
                    timeArr[2] = 0;
                    dom(timeArr, me);
                }
            }
        }, 1000);
    });
}

/**
 * 分页数据
 * @param {any} pageNo
 * @param {any} pageSize
 * @param {any} array
 */
function pagination(pageNo, pageSize, array) {
    var offset = (pageNo - 1) * pageSize;
    return (offset + pageSize >= array.length) ? array.slice(offset, array.length) : array.slice(offset, offset + pageSize);
}

/**
 * 模糊查询
 * @param {any} keyWord
 * @param {any} list
 */
function searchByIndexOf(keyWord, proName, list) {
    if ($.isEmptyObject(keyWord)) {
        return list;
    }
    var len = list.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        //如果字符串中不包含目标字符会返回-1
        if (list[i][proName].indexOf(keyWord) >= 0) {
            arr.push(list[i]);
        }
    }
    return arr;
}

//#region wdj添加

/**
 * 自定义数组去重(只适合字符串数组)
 * @method 
 * @return {Array} 返回去重结果
 */
Array.prototype.distinct = function () {
    var self = this;
    var _a = this.concat().sort();
    _a.sort(function (a, b) {
        if (a == b) {
            var n = self.indexOf(a);
            self.splice(n, 1);
        }
    });
    return self;
};


/**
 * 自定义四舍五入
 * @method
 * @param {string} 数字
 * @param {number} 保留位数 默认保留两位
 * @return {number} 返回保留结果
 */
function GetRrounding(number, n) {
    if (!checkNumber(number)) {
        return 0;
    }
    if (n == undefined) {
        n = 2;
    }
    n = n ? parseInt(n) : 0;
    if (n <= 0) return Math.round(number);
    number = Math.round(number * Math.pow(10, n)) / Math.pow(10, n);
    return number;
}

/**
    * 自定义保留位数四舍
    * @method
    * @param {string} 数字
    * @param {number} 保留位数
    * @return {number} 返回保留结果
    */
function GiveUpNumber(number, n) {
    if (!checkNumber(number)) {
        return 0;
    }
    var regExp = new RegExp("^([1-9]\\d*|0)(\\.\\d{1," + n + "})?");
    if (regExp.test(number)) {
        return regExp.exec(number)[0];
    } else {
        return number;
    }
}



/**
 * 将数字金额转为中文大写
 * @method
 * @param {string} 字符串数字金额
 * @return {string} 返回中文大写金额
 */
function ArabiaToChinese(money) {
    var cnNums = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖"); //汉字的数字
    var cnIntRadice = new Array("", "拾", "佰", "仟"); //基本单位
    var cnIntUnits = new Array("", "万", "亿", "兆"); //对应整数部分扩展单位
    var cnDecUnits = new Array("角", "分", "毫", "厘"); //对应小数部分单位
    var cnInteger = "整"; //整数金额时后面跟的字符
    var cnIntLast = "元"; //整型完以后的单位
    var maxNum = 999999999999999.9999; //最大处理的数字

    var integerNum; //金额整数部分
    var decimalNum; //金额小数部分
    var chineseStr = ""; //输出的中文金额字符串
    var parts; //分离金额后用的数组，预定义
    if (money == "") {
        return "";
    }
    money = parseFloat(money);
    if (money >= maxNum) {
        alert('超出最大处理数字:' + maxNum);
        return "";
    }
    if (money == 0) {
        chineseStr = cnNums[0] + cnIntLast + cnInteger;
        //chineseStr = cnNums[0] + cnIntLast;
        //document.getElementById("show").value=ChineseStr;
        return chineseStr;
    }
    money = money.toString(); //转换为字符串
    if (money.indexOf(".") == -1) {
        integerNum = money;
        decimalNum = '';
    } else {
        parts = money.split(".");
        integerNum = parts[0];
        decimalNum = parts[1].substr(0, 4);
    }
    if (parseInt(integerNum, 10) > 0) {//获取整型部分转换
        var zeroCount = 0;
        var intLen = integerNum.length;
        for (i = 0; i < intLen; i++) {
            n = integerNum.substr(i, 1);
            p = intLen - i - 1;
            q = p / 4;
            m = p % 4;
            if (n == "0") {
                zeroCount++;
            } else {
                if (zeroCount > 0) {
                    chineseStr += cnNums[0];
                }
                zeroCount = 0; //归零
                chineseStr += cnNums[parseInt(n)] + cnIntRadice[m];
            }
            if (m == 0 && zeroCount < 4) {
                chineseStr += cnIntUnits[q];
            }
        }
        chineseStr += cnIntLast;
        //整型部分处理完毕
    }
    if (decimalNum != '') { //小数部分
        var decLen = decimalNum.length;
        for (i = 0; i < decLen; i++) {
            n = decimalNum.substr(i, 1);
            if (n != '0') {
                chineseStr += cnNums[Number(n)] + cnDecUnits[i];
            }
        }
    }
    if (chineseStr == '') {
        chineseStr += cnNums[0] + cnIntLast + cnInteger;
        chineseStr += cnNums[0] + cnIntLast;
    } else if (decimalNum == '') {
        chineseStr += cnInteger;
    }
    return chineseStr;
}


/**
 * 将小数拆分为整数+小数形式返回数组
 * @method
 * @param {String} 字符串数字
 * @return {Array} 返回数组 数组下表[0]部分为是否成功，数组[1]部分为整数，数组[2]为小数
 */
function getDecimal(num) {
    var numArray = new Array();
    // 将输入的内容转为float类型（如果是小数为了保留小数部分）
    if (checkNumber(num)) {
        var isFloat = parseFloat(num);

        if (isFloat === 0) {
            numArray[0] = true;
            numArray[1] = parseFloat("0.0");// 整数部分
            numArray[2] = parseFloat("0.0");
        } else {
            // 是数字类型
            numArray[0] = true;
            //判断是否有小数
            if (isFloat.toString().indexOf(".") < 0) {
                numArray[1] = isFloat;// 整数部分
                numArray[2] = parseFloat("0.0");
            } else {
                var numArr = isFloat.toString().split(".");
                numArray[1] = parseInt(numArr[0]);// 整数部分
                numArray[2] = parseFloat('0.' + numArr[1]);// 小数部分
            }
        }
    } else {
        numArray[0] = false;
    }
    return numArray;
}


/**
* 解决小数精度问题
* @param {*数字 } a
* @param {*数字 } b
* @param {*符号 } sign
* fixedFloat(0.3, 0.2, '-')
 * 参考：https://my.oschina.net/cjlice/blog/1616682
*/

function fixedFloat(a, b, sign) {
    function handle(x) {
        var y = String(x);
        var p = y.lastIndexOf('.');
        if (p === -1) {
            return [y, 0];
        } else {
            return [y.replace('.', ''), y.length - p - 1];
        }
    }
    // v 操作数1, w 操作数2, s 操作符, t 精度
    function operate(v, w, s, t) {
        switch (s) {
            case '+':
                return (v + w) / t;
            case '-':
                return (v - w) / t;
            case '*':
                return (v * w) / (t * t);
            case '/':
                return (v / w);
        }
    }

    var c = handle(a);
    var d = handle(b);
    var k = 0;

    if (c[1] === 0 && d[1] === 0) {
        return operate(+c[0], +d[0], sign, 1);
    } else {
        k = Math.pow(10, Math.max(c[1], d[1]));
        if (c[1] !== d[1]) {
            if (c[1] > d[1]) {
                d[0] += padding0(c[1] - d[1]);
            } else {
                c[0] += padding0(d[1] - c[1]);
            }
        }
        return operate(+c[0], +d[0], sign, k);
    }
}

// 补0
function padding0(p) {
    var z = '';
    while (p--) {
        z += '0';
    }
    return z;
}

// 加
function plus(a, b) {
    return fixedFloat(a, b, '+');
}
// 减
function minus(a, b) {
    return fixedFloat(a, b, '-');
}
// 乘
function multiply(a, b) {
    return fixedFloat(a, b, '*');
}
// 除
function division(a, b) {
    return fixedFloat(a, b, '/');
}


/**
* 自定义加法
* @method
* @param {String} 字符串
* @param {String} 字符串
* @return {number} 返回计算结果
*/
function addition(numOne, numTwo) {
    return plus(numOne, numTwo);
}

/**
 * 自定义乘法
 * @method
 * @param {String} 字符串
 * @param {String} 字符串
 * @return {number} 返回计算结果
 */
function multiplication(numOne, numTwo) {
    return multiply(numOne, numTwo);
};


/**
 * 判断输入是否是数字(包含小数正负)
 * @method
 * @param {String} 字符串
 * @return {true/false} 返回true/false
 */
function checkNumber(theObj) {
    var reg = /^(\\-|\+)?\d+(\.\d+)?$/;
    if (reg.test(theObj)) {
        return true;
    }
    return false;
}
//#endregion

// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
// ReSharper disable once NativeTypePrototypeExtending
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "H+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (o.hasOwnProperty(k))
            if (new RegExp("(" + k + ")").test(fmt))
                fmt = fmt.replace(RegExp.$1,
                    (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
/**
 * 
 * @param {any} time 待格式化的时间
 * @return 返回格式化好的时间
 */
function FormattingJsonTime(time) {
    if (!time) {
        return null;
    }
    time = "" + time + "";
    var info = time.replace(/T/, " ");
    var date = new Date(info);
    return date.Format("yyyy-MM-dd HH:mm");
    //return new Date(time).toISOString().replace(/T/g, ' ').replace(/\.[\d]{3}Z/, '');
}
function FormattingTimeWithFormatStr(time, format) {
    if (!time) {
        return null;
    }
    time = "" + time + "";
    var info = time.replace(/T/, " ");
    var date = new Date(info);
    if (!format)
        return date.Format("yyyy-MM-dd HH:mm");
    else
        return date.Format(format);
}
/**
 * 对字符串类型的时间进行精确到分钟的处理,如果不符合格式则原样返回
 * @param {any} dateStr 需进行处理的时间字符串
 */
function FormatStringDate(dateStr) {
    var n = (dateStr.split(":")).length - 1;
    if (n !== 2)
        return dateStr;
    var index = dateStr.lastIndexOf(":");
    if (index < 0)
        return dateStr;
    else
        return dateStr.substring(0, index);
}
//最小值
Array.prototype.min = function () {
    var min = this[0];
    var len = this.length;
    for (var i = 1; i < len; i++) {
        if (this[i] < min) {
            min = this[i];
        }
    }
    return min;
}
//最大值
Array.prototype.max = function () {
    var max = parseFloat(this[0]);
    var len = this.length;
    for (var i = 1; i < len; i++) {
        var vl = parseFloat(this[i]);
        if (vl > max) {
            max = vl;
        }
    }
    return max;
}

/**
 * *
 * @param {} fractionDigits 
 * @returns {} 
 */
Number.prototype.toFixed = function (fractionDigits) {
    //没有对fractionDigits做任何处理，假设它是合法输入  
    return (parseInt(this * Math.pow(10, fractionDigits) + 0.5) / Math.pow(10, fractionDigits)).toString();
}

/**
 * 设置Cookie
 * @param {any} cName
 * @param {any} value
 * @param {any} expiredays
 */
function setCookie(cName, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = cName + "=" + escape(value) +
        ((expiredays == null) ? "" : "; expires=" + exdate.toGMTString());
}

/**
 * 获取Cookie
 * @param {any} name
 */
function getCookie(name) {
    var strCookie = document.cookie;
    var arrCookie = strCookie.split("; ");
    for (var i = 0; i < arrCookie.length; i++) {
        var arr = arrCookie[i].split("=");
        if (arr[0] === name) return arr[1];
    }
    return "";
}

/**
 * 只能输入数字
 * @param {any} obj
 */
function TextOnlyNumber(obj) {
    var isMatchedNumber = obj.value.match(/^\d+$/);
    obj.value = isMatchedNumber == null ? "" : obj.value;

}

/**
 //* 随机数
 * @param {any} n
 */
function RndNum(n) {
    var rnd = "";
    for (var i = 0; i < n; i++)
        rnd += Math.floor(Math.random() * 10);
    return rnd;
}