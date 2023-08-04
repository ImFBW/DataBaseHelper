var SearchData = {
    TabIndex: 10, //定义已经打开的Tab的数量
}
$(function () {
    //搜索表单初始化
    var options = {
        beforeSubmit: function () {
            var searchTxt = $.trim($("#serarch_main").val());
            var fsServiceID = ServcieData.ID;
            if (Valid.IsNullOrEmpty(searchTxt)) {
                return false;
            }
            if (fsServiceID <= 0) {
                layer.msg("未选择数据库", { icon: 0 }); return false;
            }
            $("#hid_SearchFSServcieEntityID").val(fsServiceID);
            SearchLoading.Show();
            return true;
        }, //提交前，验证
        success: SearchSuccess,  //提交成功后
        error: SearchError,
        //url:"",//form "action"已经配置了
        //"type":"post",//form "method"已经配置了
        //dataType:null,//'xml', 'script', or 'json'
        //clearForm: false,//提交成功后，清空表单
        //resetForm: false,//提交成功后，重置表单
        //timeout:   3000
    }
    $("#form_Search").submit(function () {
        $(this).ajaxSubmit(options);
        return false;
    });
    var _noDataHtml = $("#searchNoDataHtml").html();
    $("#searchListContent").append(_noDataHtml);
    //切换搜索分类的Tab
    $(".search_btn_tabs .btn").on("click", function () {
        var searchType = $(this).attr("rel");
        $(this).addClass("tab-active").siblings().removeClass("tab-active");
        $(this).addClass("btn-outline-light").removeClass("btn-outline-secondary").siblings().removeClass("btn-outline-light").addClass("btn-outline-secondary");
        var showLength = 0;
        $("#searchListContent").find("a").each(function (i, n) {
            var searchContentType = $(n).attr("rel");
            $(n).show();
            showLength++;
            if (searchType != "0" && searchType != searchContentType) {
                $(n).hide();
                showLength--;
            }
        });
        var noData = $("#searchListContent").find('.searchNoData').length;
        if (showLength == 0) {
            if (noData > 0) {
                $("#searchListContent").find('.searchNoData').show();
            } else {
                var _noDataHtml = $("#searchNoDataHtml").html();
                $("#searchListContent").append(_noDataHtml);
            }
        } else {
            if (noData > 0) {
                $("#searchListContent").find('.searchNoData').hide();
            }
        }
    })
});
/**
 * 表单提交成功
 * @param {any} responseText
 * @param {any} statusText
 * @param {any} xhr
 * @param {any} $form
 */
function SearchSuccess(responseText, statusText, xhr, $form) {
    SearchLoading.Hide();
    if (statusText == "success") {
        //debugger
        $(".search_btn_tabs .btn:eq(0)").click();
        if (responseText.status == true) {
            var _resultHtml = '';
            $.each(responseText.result, function (i, n) {
                var _styleIcon = "";
                var relType = 0;
                if (n.dbObjectType == 1) {//Table
                    _styleIcon = '<i class="bi-table color-table"></i>';
                    relType = 1;
                } else if (n.dbObjectType == 2) {//Proc
                    _styleIcon = '<i class="bi-filter-square color-proc"></i>';
                    relType = 2;
                } else if (n.dbObjectType == 3) {//Fun_Table
                    _styleIcon = '<i class="icon iconfont icon-tubiao-hanshu color-func"></i>';
                    relType = 3;
                } else if (n.dbObjectType == 4) {//Table_Column
                    //这种是搜索字段得出的表名
                    relType = 1;
                }
                _resultHtml += '<a href="#" onclick="LoadData(' + n.dbObjectType + ',\'' + n.typeName + '\')" rel="' + relType + '" class="list-group-item list-group-item-action  list-group-item-light">' + _styleIcon + n.typeName + '</a>';
            })
            $("#searchListContent").html('');
            if (_resultHtml != '') {
                $("#searchListContent").html(_resultHtml);
            } else {
                var _noDataHtml = $("#searchNoDataHtml").html();
                $("#searchListContent").html(_noDataHtml);
            }
        } else {
            layer.msg(responseText.message, { icon: 0, shade: 0.1 }, function () {
            });
        }
    } else {
        console.log(responseText);
        layer.msg("系统异常", { icon: 2, shade: 0.1 }, function () {
        });
    }
}
/**
 * 加载数据，表Table，存储过程、表值函数内容
 * @param {any} typeID
 * @param {any} typeName
 */
function LoadData(typeID, typeName) {
    var id = ServcieData.ID;
    //debugger
    $.ajax({
        url: '/database/PartialViewForSearchResult/',
        type: 'GET',
        data: { id: id, typeID: typeID, typeName: typeName },
        dataType: 'html',
        success: function (data) {
            SearchData.TabIndex = SearchData.TabIndex + 1;
            var tabHtml = '<li data-tab="' + SearchData.TabIndex + '"><span class="tab_title">' + typeName + '</span><span class="close"><i class="bi bi-x-square"></i></span></li>';
            $('.tab_list').append(tabHtml);//追加Tab按钮
            $("#content_wrapper").append(data);//追加内容
            $("#content_wrapper").find("div.accordian_header:last").addClass("tab_" + SearchData.TabIndex);//给内容设置特定的class
            EventTabAction();//注册Tab事件
            hljs.highlightAll();//执行插件，设置代码格式
            ActionToTabs(SearchData.TabIndex);//触发当前Tab按钮的点击事件，进行切换
        },
        error: function (x, s, e) {
            layer.msg("系统异常", { icon: 2, shade: 0.1 }, function () {
            });
        }
    })
}
/**
 * 注册打开的选项卡的切换和关闭事件
 */
function EventTabAction() {
    //切换Tabs选项卡
    $(".tab_list li").on("click", function () {
        //$(this).addClass("active").siblings().removeClass("active");
        var tabID = $(this).data("tab");
        ActionToTabs(tabID);
    });
    $(".tab_list .close").on("click", function () {
        var tabID = $(this).closest("li").data("tab");
        var parent = $(".content_wrapper");
        parent.find(".accordian_header.tab_" + tabID).remove();
        if ($(this).closest("li").hasClass("active")) {
            $(this).closest("li").remove();
            ActionToTabs(0);
        } else {
            $(this).closest("li").remove();
        }
    });
}
/**
 * 表单提交异常
 * @param {any} x
 * @param {any} s
 * @param {any} h
 * @param {any} f
 */
function SearchError(x, s, h, f) {
    layer.closeAll();
    console.log(s);
    layer.msg("系统异常", { icon: 2, shade: 0.1 }, function () {
    });
}
/**
 * 搜索时的遮罩层和loadding提示
 */
SearchLoading = {
    Show: function () {
        var _loadShade = '<div class="layui-layer-shade search_Loading" style="z-index: 202301;background-color: rgb(0, 0, 0);opacity: 0.1;position:absolute;"></div>';
        var _loading = '<div class="search_Loading  text-primary fs-6 p-1" style="min-width: 100px;filter: alpha(opacity=60);color: #fff;border: none;z-index: 202302;position: absolute;top:120px;left: 38%;"><div class="spinner-border spinner-border-sm" role="status"><span class="visually-hidden"></span></div> 搜索中...</div>';
        var _loadHtml = _loadShade + _loading;
        $("#searchResultList").append(_loadHtml);

    },
    Hide: function () {
        $(".search_Loading").remove();
    }
}

/**
         * 点击切换搜索结果选项卡
         */
function ActionToTabs(tabID) {
    var tabs = $(".tab_list").find("li");
    var actTabID = tabID;
    $.each(tabs, function (i, n) {
        var nowTabid = $(n).data("tab");
        if (tabID == 0 && i == 0) {
            actTabID = nowTabid;
        }
        if (actTabID == nowTabid) {
            $(n).addClass("active").siblings().removeClass("active");
        }
    });
    var parent = $(".content_wrapper");
    parent.find(".accordian_header").removeClass("active").hide();
    parent.find(".accordian_header.tab_" + actTabID).addClass("active").show();
    setSplitPageHight();
}
/**
 * 显示行号 
 */
function ShowLineNo() {
    $("code").each(function (i, n) {
        $(n).html("<ol><li>" + $(n).html().replace(/[\r\n]/g, "</li><li>") + "</li></ol>");
    });
}
/**
 * 隐藏行号
 */
function HideLineNo() {
    $("code").each(function (i, n) {
        var newHtml = $(n).html().replace(/<ol>/g, "");
        newHtml = newHtml.replace(/<li>/g, "");
        newHtml = newHtml.replace(/<\/ol>/g, "");
        newHtml = newHtml.replace(/<\/li>/g, "");
        $(n).html(newHtml);
    });
}
/**
 * 开启/关闭行号(暂未实现)
 */
function BtnSetLineNo() {
    var defaultSet = $(this).data("default");
    if (defaultSet == "1") {
        HideLineNo();
        defaultSet = "0";
        $(this).addClass("btn-outline-secondary");
        $(this).removeClass("btn-outline-primary");
    } else {
        ShowLineNo();
        defaultSet = "1";
        $(this).addClass("btn-outline-primary");
        $(this).removeClass("btn-outline-secondary");
    }
}
/**
 * 切换背景色
 */
function BtnChangeTheme(ele) {
    var defaultSet = $(ele).data("default");
    if (defaultSet == "1") {
        defaultSet = "0";
        loadjscssfile("/lib/highlight/styles/nnfx-dark.min.css", "css");
        removejscssfile("/lib/highlight/styles/vs.min.css", "css");
        $("button.switch_bgColor").addClass("btn-outline-light");
        $("button.switch_bgColor").removeClass("btn-outline-secondary");
    } else {
        defaultSet = "1";
        loadjscssfile("/lib/highlight/styles/vs.min.css", "css");
        removejscssfile("/lib/highlight/styles/nnfx-dark.min.css", "css");
        $("button.switch_bgColor").addClass("btn-outline-secondary");
        $("button.switch_bgColor").removeClass("btn-outline-light");
    }
    $("button.switch_bgColor").data("default", defaultSet);
}
/**
        * 设置split slider高度，以适应页面
        */
function setSplitPageHight() {
    var eve_Left = $("#mainLeft");
    var eve_Right = $(".accordian_header.active");
    if (!eve_Right) eve_Right = $("#mainRight");
    var mainRight = $("#mainRight");
    var leftH = eve_Left.height();
    var rightH = eve_Right.height();//mainRight
    $("#masterMain").css({ "height": "auto" });
    var mainH = $("#masterMain").height();
    eve_Left.css({ "min-height": leftH + "px" });
    eve_Right.css({ "min-height": rightH + "px" });
    //$("#masterMain").css({ "min-height": mainH + "px" });
    eve_Left.css({ "height": "auto" });
    eve_Right.css({ "height": "auto" });
    leftH = eve_Left.height();
    rightH = eve_Right.height();
    var setHeight = Math.max(leftH, rightH);
    //mainH = mainH-70;
    //setHeight = Math.max(mainH, setHeight);
    //console.log("Height:" + setHeight);
    if (rightH > leftH) setHeight = setHeight + 100;
    $("#masterMain").css({ "height": setHeight + "px" });
    mainRight.css({ "height": setHeight + "px" });
    if (rightH > leftH) setHeight = setHeight - 70;
    $(".gutter-horizontal").css({ "height": setHeight + "px" });
}

/**
 * 动态的监听高度变化，自动设置split slider的高度(已废弃)
 */
function DynamicSetSplitSliderHight() {
    /**说明：这个是利用监听的方式，监听高度变化，然后重设高度*/
    //设置需要观察高度变化的节点
    const targetNode = document.getElementById("content_wrapper");
    //观察器的配置，需要观察什么的变化,attributeFilter:观察特定属性
    const config = { subtree: true, attributes: true, attributeFilter: ['class', 'style'] };
    //当观察到变动时执行的回调函数
    const callback = function (mutationsList, observer) {
        // Use traditional 'for loops' for IE 11
        for (let mutation of mutationsList) {
            //if (mutation.type === 'childList') {
            //    console.log('A child node has been added or removed.');
            //}else
            if (mutation.type === 'attributes') {
                if (mutation.attributeName == "style") {
                    //console.log($(mutation.target).css("display"));
                    var isDisplay = $(mutation.target).css("display");
                    if (isDisplay == "block") {
                        setSplitPageHight();
                    }
                }
            }
        }
    };
    //创建一个观察器实例并传入回调函数
    const observer = new MutationObserver(callback);
    //以上述配置开始观察目标节点
    observer.observe(targetNode, config);
    // 之后，可停止观察
    //observer.disconnect();
}

