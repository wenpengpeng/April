/*
创建：MJZ
日期：2014年4月28日 21:10:18
说明：Jquery插件封装类
版本：1.1.1
***********修订：MJZ 2015年10月27日17:05:04*********
日志：
1.新增idCutStr，nameCutStr属性
2.调整ajax返回JSON参数数据
*********************************************************
备注：
AJAX请求的URL返回的是一个JSON格式的数据如：
{"info":"数据加载成功","status":"y","records":[
    {"selectId": "1","selectName": "选项1"},
    {"selectId": "2","selectName": "选项2"},
    {"selectId": "3","selectName": "选项3"},
    {"selectId": "4","selectName": "选项4"}
]}
*/

(function ($) {
    /*函数：级联选择*/
    $.fn.CascadeSelect = function (options) {
        var objElement = this;
        if (objElement.length === 0) {
            alert("无级联的对象：\n 1.请确认选择器元素存在!\n 2.请确认class和id选择符号正确!");
            return;
        }
        /*默认值对象*/
        var defalutVal = {
            /*下拉框数量，默认为3个*/
            selectNum: 3,
            /*下拉框的样式，默认为宽度100像素*/
            selectCss: {
                "width": 100
            },
            /*地区选择之后获取值和文本的选择器，默认为#hidAreaId和#hidAreaName*/
            assignValueSelector: "#hidAreaId",
            assignTextSelector: "#hidAreaName",
            elementWrapper: true,
            /*ajaxURL连接地址，默认为地区选择地址*/
            ajaxUrl: "/tools/Area_ajax.ashx",
            /*Id赋值的分割符，默认为，号*/
            idCutStr: ",",
            /*Name赋值的分割符，默认为，号*/
            nameCutStr: ",",
            /*Id赋值的值是val还是使用text方法赋值数据*/
            idSssignType: "val",
            /*name赋值的值是val还是使用text方法赋值数据*/
            nameSssignType: "val",
            /*最顶级ID ，默认为0*/
            parentId: "0",
            /*下拉框的Id值，如果页面有多个地方需要使用级联插件的话必须保证每个赋值不一样*/
            selectIdChar: "mjzSelect",
            /*是否自动创建下拉选择框，默认为自动创建*/
            isAutoCreateSelect: true,
            /*是否自动给第一个选择赋值,默认为为第一个赋值*/
            isAssignFirstValue: true,
            /*是否自动赋值，默认为不自动赋值*/
            isAutoAssignValue: false,
            /*是否启用缓存，默认为启用*/
            isEnableCache: false
        };
        /*合并参数*/
        $.extend(defalutVal, options);
        /*申明一个下拉框的样式字符*/
        var charId = defalutVal.selectIdChar;
        //清空对象
        objElement.html("");
        /*是否自己创建对象*/
        if (defalutVal.isAutoCreateSelect) {
            for (var i = 0; i < defalutVal.selectNum; i++) {
                charId += 1;
                /*创建select对象*/
                var objSelect = document.createElement('select');
                $(objSelect).attr({
                    "id": charId,
                    "class": "mjzCascade" //给下拉框一个样式名字
                }).append("<option value=''>请选择</option>");
                /*添加默认样式*/
                if (typeof defalutVal.selectCss == "object") {
                    $(objSelect)
                        .css(defalutVal.selectCss);
                } else if (typeof defalutVal.selectCss == "string") {
                    $(objSelect)
                        .addClass(defalutVal.selectCss);
                }
                $(objSelect)
                    .css({ "margin-right": 5 });
                /*遍历添加到初始对象中去*/
                if (defalutVal.elementWrapper) {
                    var $divContainer = $('<div class="col-md-3 publicContainerFormSelected"><div class="form-group"></div></div>');
                    $divContainer
                        .find(".form-group")
                        .append(objSelect);
                    objElement.append($divContainer);
                } else {
                    objElement.append(objSelect);
                }
            }
        } else {
            objElement.find("select").each(function () {
                charId += 1;
                $(this).attr({
                    "id": charId,
                    "class": "mjzCascade" //给下拉框一个样式名字
                }).append("<option value=''>请选择</option>");
                $(this).css(defalutVal.selectCss).css({ "margin-right": 5 });
            });
        }
        var buildSelectTemplete;
        objElement.find("select").change(function () {
            /*获取当前选择框在选择框的索引值*/
            var index = objElement.find(".mjzCascade").index(this);
            /*对地区值和地区名字的隐藏控件进行赋值操作*/
            var selectListId = "";
            var selectListName = "";
            /*需要对当前之后的下拉框的html内容进行清空操作*/
            objElement.find("select:gt(" + index + ")").html("<option value=''>请选择</option>");
            if ($(this).val() === "") {
                /*然后对隐藏控件的值进行处理*/
                objElement.find("select:lt(" + index + ")").each(function () {
                    selectListId += defalutVal.idCutStr + $(this).val();
                    selectListName += defalutVal.nameCutStr + $(this).find("option:selected").text();
                });
                /*需要对第一个下拉框选择进行逻辑处理*/
                if (index !== 0) {
                    selectListId = selectListId.substring(1);
                    selectListName = selectListName.substring(1);
                } else {
                    selectListId = "";
                    selectListName = "";
                }
                /*给传入参数的选择器赋值*/
                switch (defalutVal.idSssignType) { case "val": $(defalutVal.assignValueSelector).val(selectListId); break; case "text": $(defalutVal.assignValueSelector).text(selectListId); break; }
                switch (defalutVal.nameSssignType) { case "val": $(defalutVal.assignTextSelector).val(selectListName); break; case "text": $(defalutVal.assignTextSelector).text(selectListName); break; }
            } else {
                var thisVal = $(this).val(); //获取当前选择的对象的value值
                var thisText = $(this).find("option:selected").text();//获取当前选择的对象的text值
                objElement.find("select:lt(" + index + ")").each(function () {
                    selectListId += defalutVal.idCutStr + $(this).val();
                    selectListName += defalutVal.nameCutStr + $(this).find("option:selected").text();
                });
                /*需要对第一个下拉框选择进行逻辑处理*/
                if (index !== 0) {
                    selectListId = selectListId.substring(1) + defalutVal.idCutStr + thisVal;
                    selectListName = selectListName.substring(1) + defalutVal.nameCutStr + thisText;
                } else {
                    selectListId = thisVal;
                    selectListName = thisText;
                }
                /*给传入参数的选择器赋值*/
                switch (defalutVal.idSssignType) { case "val": $(defalutVal.assignValueSelector).val(selectListId); break; case "text": $(defalutVal.assignValueSelector).text(selectListId); break; }
                switch (defalutVal.nameSssignType) { case "val": $(defalutVal.assignTextSelector).val(selectListName); break; case "text": $(defalutVal.assignTextSelector).text(selectListName); break; }
                buildSelectTemplete($(this).val(), $(this).attr("id"));
            }
        });
        /**
         * 构建下拉框选择模版
         * @param {} val 
         * @param {} id 
         * @returns {} 
         */
        buildSelectTemplete = function (val, id) {
            var optionsChar = ""; //申明下拉选项的字符串
            /*方法拓展,需要申明在函数里面的全局变量*/
            var argsLength = arguments.length;
            var argsMents = arguments[2];
            //如果不是对象的话则不需要异步处理
            var $nextSelectorId = id + 1;
            if ($("#" + $nextSelectorId)) {
                //使用正则表达式获取1在选择框的位置
                var result = $nextSelectorId.match(/1/g);
                var count = !result ? 0 : result.length - 1;
                //如果传入的异步URL是对象的话则直接处理
                if (typeof defalutVal.ajaxUrl == "object") {
                    //选择框数组
                    var selectArray;
                    //如果最顶级
                    if (count === 0) {
                        selectArray = Enumerable
                            .from(defalutVal.ajaxUrl)
                            .where("$.level == 0")
                            .toArray();
                    } else {
                        selectArray = Enumerable
                            .from(defalutVal.ajaxUrl)
                            .where("$.level == " + count + " && $.code.substring(0," + count * 2 + ")==" + val.substring(0, count * 2) + "")
                            .toArray();
                    }
                    //遍历添加数据
                    $(selectArray).each(function (index, item) {
                        if (argsLength === 3 && item.code === argsMents) {
                            /*当传入的是三个参数并且从后台程序获取的值等于当前传入的值，则为选中*/
                            optionsChar += '<option value="' + item.code + '" selected=\"selected\">' + item.names + '</option>';
                            //tempSelectName += item.selectName;
                        } else {
                            optionsChar += '<option value="' + item.code + '">' + item.names + '</option>';
                        }
                    });
                    $("#" + $nextSelectorId).append(optionsChar); //先进行清空操作，再进行赋值操作
                } else {
                    //异步去请求
                    $.ajax({
                        url: defalutVal.ajaxUrl,
                        type: 'POST',
                        data: { parentId: val },
                        dataType: 'JSON',
                        cache: defalutVal.isEnableCache,
                        async: false,//这里需要设置同步
                        success: function (data) {
                            if (data.status === "y") {
                                $(data.records).each(function (index, item) {
                                    if (argsLength === 3 && item.selectId === argsMents) {
                                        /*当传入的是三个参数并且从后台程序获取的值等于当前传入的值，则为选中*/
                                        optionsChar += '<option value="' + item.selectId + '" selected=\"selected\">' + item.selectName + '</option>';
                                        //tempSelectName += item.selectName;
                                    } else {
                                        optionsChar += '<option value="' + item.selectId + '">' + item.selectName + '</option>';
                                    }
                                });
                                $("#" + id + 1).append(optionsChar); //先进行清空操作，再进行赋值操作
                            } else {
                                alert(data.info);
                            }
                        },
                        error: function () {
                            //#region ajax调试
                            //调试参数：XMLHttpRequest, textStatus, errorThrown
                            //alert(XMLHttpRequest.status);
                            //alert(XMLHttpRequest.readyState);
                            //alert(textStatus);
                            //#endregion
                            alert("服务器繁忙，请稍后重试");
                        }
                    });
                }
            }
        };
        if (defalutVal.isAutoAssignValue) {
            try {
                //判断传入的是否为对象
                if (options.selectDataList != undefined) {
                    var dataArrayList;
                    var tempSelectName = ""; //临时选择名字变量
                    if (!options.selectSplitChar) { //如果有分割字符，则使用传入的分割字符，否则使用默认的,
                        dataArrayList = options.selectDataList.split(',');
                    } else {
                        dataArrayList = options.selectDataList.split(options.selectSplitChar);
                    }
                    var charIdNew = ""; //新申明一个ID
                    for (var j = 0; j < dataArrayList.length; j++) {
                        if (j === 0) {
                            defalutVal.parentId = 0;
                        } else {
                            defalutVal.parentId = dataArrayList[j - 1];
                            charIdNew += 1;
                        }
                        buildSelectTemplete(defalutVal.parentId, defalutVal.selectIdChar + charIdNew, dataArrayList[j]);
                    }
                    /*获取选择的名字*/
                    if (tempSelectName && tempSelectName !== "") {
                        $(defalutVal.assignTextSelector).val(tempSelectName);
                    }
                } else {
                    var $font = "<font color='red'>提示：插件selectDataList无值，暂不能自动赋值！</font>";
                    objElement.html($font);
                }
            } catch (e) {
                objElement.html("<font color='red'>绑定下拉框错误,可能的原因：" + e.message + "</font>");
            }
            /*
            如果是自动赋值的话，则不需要绑定第一个值，因为自动赋值里面就已经绑定过值了
            */
            defalutVal.isAssignFirstValue = false;
        }
        if (defalutVal.isAssignFirstValue) {
            buildSelectTemplete(defalutVal.parentId, defalutVal.selectIdChar);
        }
    };
})(jQuery);