/*
函数：基于webuploader封装的文件上传插件
创建：MJZ
日期：2017年6月19日 16:59:32
*/

$(function () {
    //初始化绑定默认的属性
    $.upLoadDefaults = $.upLoadDefaults || {};
    $.upLoadDefaults.property = {
        multiple: false, //是否多文件
        water: false, //是否加水印
        thumbnail: false, //是否生成缩略图
        thumbWidth: 0, //缩略图宽度
        thumbHeight: 0,//缩略图高度
        duplicate: true, //不去重
        sendurl: null, //请求上传接口地址
        antiForgeryToken: abp.security.antiForgery.getToken(), //跨域攻击值，默认是用abp来
        filetypes: "jpg,jpeg,png,gif", //文件类型
        filesize: 2048, //文件大小
        btntext: "上传", //上传按钮的文字
        swf: "/libs/webuploader/uploader.swf", //SWF上传控件相对地址
        onSuccess: null, //文件上传成功回调函数
        onError: null, //文件上传失败回调函数
        fileVal:"fileData",
        onInforTip: null, //信息提示，用于上传过程中需要提示的东西
        auto: true //自动上传
    };
    //初始化上传控件
    $.fn.InitUploader = function (b) {
        var fun = function (parentObj) {
            var p = $.extend({}, $.upLoadDefaults.property, b || {});
            var btnObj = $('<div class="upload-btn">' + p.btntext + '</div>').appendTo(parentObj);
            //初始化WebUploader
            var uploader = WebUploader.create({
                compress: false, //不压缩
                auto: p.auto, //自动上传
                swf: p.swf, //SWF路径
                server: p.sendurl, //上传地址
                duplicate: p.duplicate,
                pick: {
                    id: btnObj,
                    multiple: p.multiple
                },
                accept: {
                    title: '请选择需要上传的文件',
                    extensions: p.filetypes
                },
                formData: {
                    'DelFilePath': '' //定义参数
                },
                fileVal: p.fileVal, //上传域的名称
                fileSingleSizeLimit: p.filesize * 1024 //文件大小
            });

            //当某个文件的分块在发送前触发，主要用来询问是否要添加附带参数
            uploader.on('uploadBeforeSend', function (object, data, header) {
                //需要传入的参数建议使用这样的方式来处理
                header["IsThumbnail"] = p.thumbnail;
                header["ThumbWidth"] = p.thumbWidth;
                header["ThumbHeight"] = p.thumbHeight;
                header["UploadFieldName"] = uploader.options.fileVal;
                //跨域攻击部分
                header["X-XSRF-TOKEN"] = p.antiForgeryToken;
            });

            //当validate不通过时，会以派送错误事件的形式通知
            uploader.on('error', function (type) {
                var errorMsg;
                switch (type) {
                    case 'Q_EXCEED_NUM_LIMIT':
                        errorMsg = "错误：上传文件数量过多！";
                        break;
                    case 'Q_EXCEED_SIZE_LIMIT':
                        errorMsg = "错误：文件总大小超出限制！";
                        break;
                    case 'F_EXCEED_SIZE':
                        errorMsg = "错误：文件大小超出限制！";
                        break;
                    case 'Q_TYPE_DENIED':
                        errorMsg = "错误：禁止上传该类型文件！";
                        break;
                    case 'F_DUPLICATE':
                        errorMsg = "错误：请勿重复上传该文件！";
                        break;
                    default:
                        errorMsg = '错误代码：' + type;
                        break;
                }
                if (typeof p.onInforTip === "function") {
                    p.onInforTip(errorMsg);
                } else {
                    alert(errorMsg);
                }
            });

            //当有文件添加进来的时候
            uploader.on('fileQueued', function (file) {
                //如果是单文件上传，把旧的文件地址传过去，方便接口修改
                if (!p.multiple) {
                    uploader.options.formData.DelFilePath = parentObj.siblings(".upload-fileName").val();
                }

                file.name = file.name.toLowerCase();

                //防止重复创建
                if (parentObj.children(".upload-progress").length == 0) {
                    //创建进度条
                    var fileProgressObj = $('<div class="upload-progress"></div>').appendTo(parentObj);
                    var progressText = $('<span class="txt">正在上传，请稍候...</span>').appendTo(fileProgressObj);
                    var progressBar = $('<span class="bar"><b></b></span>').appendTo(fileProgressObj);
                    var progressCancel = $('<a class="close" title="取消上传">关闭</a>').appendTo(fileProgressObj);
                    //绑定取消点击事件
                    progressCancel.click(function () {
                        uploader.cancelFile(file);
                        fileProgressObj.remove();
                    });
                }
            });

            //文件上传过程中创建进度条实时显示
            uploader.on('uploadProgress', function (file, percentage) {
                var progressObj = parentObj.children(".upload-progress");
                progressObj.children(".txt").html(file.name);
                progressObj.find(".bar b").width(percentage * 100 + "%");
            });

            //当文件上传出错时触发
            uploader.on('uploadError', function (file, reason) {
                uploader.removeFile(file); //从队列中移除
                if (typeof p.onError === "function") {
                    p.onError(file, reason, parentObj);
                } else {
                    if (typeof p.onInforTip === "function") {
                        p.onInforTip(file.name + "上传失败，错误代码：" + reason);
                    } else {
                        alert(file.name + "上传失败，错误代码：" + reason);
                    }
                }
            });

            //当文件上传成功时触发
            uploader.on('uploadSuccess', function (file, data) {
                if (!data.success) {
                    if (typeof p.onInforTip === "function") {
                        p.onInforTip(data.error.message);
                    } else {
                        alert(data.error.message);
                    }
                } else {
                    if (typeof p.onSuccess === "function") {
                        p.onSuccess(file, data, parentObj);
                    } else {
                        var resultData = data.result;
                        //如果是单文件上传，则赋值相应的表单
                        if (!p.multiple) {
                            parentObj.siblings(".upload-fileName").val(resultData.serverFileName);
                            parentObj.siblings(".upload-path").val(resultData.url);
                            parentObj.siblings(".upload-name").val(resultData.originFileName);
                            //处理图片问题
                            var $li = parentObj.siblings('.uploader');//图片容器对象
                            //避免重复创建图片容器
                            if (!$li.length) {
                                //创建单图的样式，如果需要修改，请考虑引用的地方是否能满足样式要求
                                //如果不想使用插件自带的图片创建，则重写onSuccess事件
                                $li = $(
                                    '<div class="row uploader">' +
                                    '   <div class="col-xs-6 col-md-3">' +
                                    '       <a href="javascript:;" class="thumbnail">' +
                                    '           <img alt="暂无图片">' +
                                    '       </a>' +
                                    '   </div>' +
                                    '</div>'
                                );
                                parentObj.before($li);
                            }
                            //处理图片的路径
                            $img = $li.find("img"); //图片对象

                            $img.attr("src", resultData.url);
                        } else {
                            console.info("多文件上传待处理...");
                        }
                    }
                }
                uploader.removeFile(file); //从队列中移除
            });

            //不管成功或者失败，文件上传完成时触发
            uploader.on('uploadComplete', function (file) {
                var progressObj = parentObj.children(".upload-progress");
                progressObj.children(".txt").html("上传完成");
                // 如果队列为空，则移除进度条
                if (uploader.getStats().queueNum == 0) {
                    //让用户看到上传完成的效果
                    setTimeout(function () {
                        progressObj.remove();
                    }, 500);
                }
            });
        };
        return $(this).each(function () {
            fun($(this));
        });
    }
});