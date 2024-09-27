/**
        * 加载js/css文件
        * filename 文件相对路径
        * filetype 文件类型，js/css
        */
function loadjscssfile(filename, filetype) {
    if (filetype == "js") {
        var fileref = document.createElement('script')
        fileref.setAttribute("type", "text/javascript")
        fileref.setAttribute("src", filename)
    }
    else if (filetype == "css") {
        var fileref = document.createElement("link")
        fileref.setAttribute("rel", "stylesheet")
        fileref.setAttribute("type", "text/css")
        fileref.setAttribute("href", filename)
    }
    if (typeof fileref != "undefined")
        document.getElementsByTagName("head")[0].appendChild(fileref)
}
/**
* 移除js/css文件
* filename 文件相对路径
* filetype 文件类型，js/css
*/
function removejscssfile(filename, filetype) {
    var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none";
    var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none";
    var allsuspects = document.getElementsByTagName(targetelement);
    for (var i = allsuspects.length; i >= 0; i--) {
        if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null
            && allsuspects[i].getAttribute(targetattr).indexOf(filename) != -1)
            allsuspects[i].parentNode.removeChild(allsuspects[i]);
    }
}

var Valid = {
    IsNullOrEmpty: function (a) {
        if (a == undefined || a == null || a == "") {
            return true;
        }
        return false;
    },
    NumberT1: function (a) {
        var b = parseInt(a);
        if (b == NaN) return false;
        if (b > 0) return true;
        return false;
    }
}
/**
 * 搜索时的遮罩层和loadding提示
 * 可以指定遮罩层在某个元素内
 */
SearchLoading = function (option) {
    var _this = this;
    _this.option = {
        Title: '加载中...',//弹出的时候提示内容
        TargetID: '',    //弹出层指定的目标元素ID
        Loading: null,   //弹出后的对象
    },
        _this.option = { ..._this.option, ...option };
    _this.Show = function () {
        var nowLength = $(".myself_Loading").length;
        nowLength = nowLength + 1;
        _this.option.Loading = 'myLoading' + nowLength;
        //absolute    relative
        var target = $(document.body);
        if (_this.option.TargetID != '') { target = $(_this.option.TargetID); }
        var _loadShade = '<div id="' + _this.option.Loading + '" class="myself_Loading" style="width: 100%;height:100%;"><div class="layui-layer-shade m-2" style="z-index: 202301;background-color: rgb(0, 0, 0);opacity: 0.1;position:absolute;"></div>';
        var _loading = '<div class="search_Loading  text-primary fs-6 p-1" style="min-width: 100px;filter: alpha(opacity=60);color: #fff;border: none;z-index: 202302;position: absolute;top:320px;left: 48%;"><div class="spinner-border spinner-border-sm" role="status"><span class="visually-hidden"></span></div> ' + _this.option.Title + '</div></div>';
        var _loadHtml = _loadShade + _loading;
        target.append(_loadHtml);
        //target.css({ "position": "relative" });


    },
        _this.Close = function () {
            if (_this.option.Loading == null) return;
            $("#" + _this.option.Loading).remove();
        },
        _this.CloseAll = function () {
            $(".myself_Loading").remove();
        }
    return _this;
}

/**
 * 触发tooltip 功能
 */
function tooltipTrigger() {
    var tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    var tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))
}