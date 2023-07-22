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

