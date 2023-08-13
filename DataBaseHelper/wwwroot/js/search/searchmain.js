var SearchData = {
    TabIndex: 10, //定义已经打开的Tab的数量
}
var table_columns = [
    { field: "RowNumber", title: "#", width: 80, sortable: true },
    {
        field: "ColumnName", title: "字段列名", width: 240, sortable: true, formatter: function (value, row, index) {
            var _tmpHtml = value;
            var searchContent = $("#serarch_main").val();
            var regMatch = new RegExp(searchContent, 'i');
            if (regMatch.test(value)) {
                var newCon = value.match(regMatch)[0];
                _tmpHtml = value.replace(regMatch, "<span class='text-danger'>" + newCon + "</span>");
            }
            return _tmpHtml;
        }
    },
    { field: "ColumnDesc", title: "字段说明" },
    { field: "ColumnDataType", title: "数据类型", width: 100, sortable: true },
    { field: "ColumnDataLength", title: "数据长度", width: 80, sortable: true },
    {
        field: "IsIdentity", title: "是否标识列", width: 80, formatter: function (value, row, index) {
            var _tmpHtml = '';
            if (value == 1) {
                _tmpHtml = '是'
            }
            return _tmpHtml;
        }
    },
    {
        field: "IsKey", title: "是否主键", width: 80, formatter: function (value, row, index) {
            var _tmpHtml = '';
            if (value == 1) {
                _tmpHtml = '是'
            }
            return _tmpHtml;
        }
    },
    {
        field: "IsNullable", title: "可否为NULL", width: 80, formatter: function (value, row, index) {
            var _tmpHtml = '';
            if (value == 1) {
                _tmpHtml = '是'
            }
            return _tmpHtml;
        }
    },
    { field: "DefaultValue", width: 100, title: "默认值" },
    {
        field: "ColumnName", title: "操作", width: 180, align: 'left', valign: 'middle', formatter: function (value, row, index) {
            var _rowJson = JSON.stringify(row);
            var _html = "";
            _html = '<button type="button" class="btn btn-sm btn-outline-primary border-0" onclick=\'EditColumnDesc(' + _rowJson + ',"column")\'><i class="bi bi-card-text"></i> 修改</button>';
            return _html;
        }
    }
];
var table_options = {
    url: '/database/GetTableData/',                    //请求后台的URL（*）
    method: 'POST',              //请求方式（*）
    toolbar: '.table-toolbar:last',        //工具按钮用哪个容器
    toolbarAlign: 'right',           //工具栏对齐方式
    striped: true,                //是否显示行间隔色
    cache: false,                 //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
    //showPaginationSwitch: true,
    pagination: true,             //是否显示分页（*）
    sortable: true,               //是否启用排序
    sortOrder: "asc",             //排序方式
    sidePagination: "client",     //分页方式：client客户端分页，server服务端分页（*）
    pageNumber: 1,                //初始化加载第一页，默认第一页,并记录
    pageSize: 20,               //每页的记录行数（*）
    pageList: [20, 50, 100, 500],//可供选择的每页的行数（*）
    search: true,                //是否显示表格搜索
    searchOnEnterKey: true,       //回车后执行搜索
    //strictSearch: true,
    //showColumns: true,            //是否显示所有的列（选择显示的列）
    showRefresh: false,           //是否显示刷新按钮
    //minimumCountColumns: 2,       //最少允许的列数
    //clickToSelect: true,          //是否启用点击选中行
    //height: 500,                //行高，如果没有设置height属性，表格自动根据记录条数设置表格高度
    uniqueId: "ColumnName",               //每一行的唯一标识，一般为主键列
    formatSearch: function () { return '搜索列名' },
    //showToggle: true,             //是否显示详细视图和列表视图的切换按钮
    //cardView: true,              //是否显示详细视图
    //detailView: true,            //是否显示父子表
    //得到查询的参数
    //queryParams: function (params) {
    //    var temp = {
    //        rows: params.limit,                         //页面大小
    //        page: (params.offset / params.limit) + 1,   //页码
    //        sort: params.sort,      //排序列名
    //        sortOrder: params.order //排位命令（desc，asc）
    //    };
    //    return temp;
    //},
    searchAlign: 'left',
    formatLoadingMessage: function () {
        return '<div class="spinner-border text-secondary" role="status"><span class="visually-hidden">Loading...</span></div> 加载中...';
    },
    onSearch: TableOnSearch,
    formatShowingRows: function (pageFrom, pageTo, totalRows, totalNotFiltered) {
        return '<span class="pagination-info">总数据 ' + totalRows + ' </span>';
    },
    formatRecordsPerPage: function (pageNumber) {
        return "".concat(pageNumber, " 行/页");
    },
    customSearch: TableSearch,
    onLoadSuccess: function (data, aaa) {
        if (data != null && data.rows) {
            if (data.rows.length > 0) {
                var tableName = data.rows[0].TableName;
                var tableDesc = data.rows[0].TableDesc;
                var headerContainer = $("div.desc_14_" + tableName);
                headerContainer.find("span.headerNameDesc").html(tableDesc);
                //注册表名的说明【修改】按钮事件
                headerContainer.find("button.tableDescEdit").unbind('click').bind("click", function () {
                    EditColumnDesc(data.rows[0], 'table');
                })
            }
        }
    },
    onLoadError: function () {
        layer.msg("加载失败", { icon: 0, shade: 0.1 }, function () {
        });
    },
    columns: table_columns
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
            var loading = SearchLoading({ Title: '搜索中...', TargetID: '#searchListContent' });
            loading.Show();
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
    $("#mainTable").bootstrapTable(table_options);
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
    SearchLoading().CloseAll();
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
                    _styleIcon = '<i class="bi bi-file-earmark-ppt color-proc"></i>';
                    relType = 2;
                } else if (n.dbObjectType == 3) {//Fun_Table
                    _styleIcon = '<i class="icon iconfont icon-tubiao-hanshu color-func"></i>';
                    relType = 3;
                } else if (n.dbObjectType == 4) {//Table_Column
                    _styleIcon = '<i class="bi-table color-table"></i><i class="bi bi-layout-split text-danger"></i>';
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
            /*设置左侧的高度*/
            setSplitPageHight();
            //var gHeight = $(".gutter-horizontal").height();
            //var searchFormH = $(".searchForm").height();
            //var searchBtnnavBarH = $(".searchBtnnavBar").height();
            //$("#mainLeft").css({ "height": gHeight + "px" });//设置主布局高度
            //$("#searchListContent").css({ "height": (gHeight - searchFormH - searchBtnnavBarH - 40) + "px", "overflow": "hidden", "overflow-y": "auto" });//设置搜索的结果展示部分高度.
            //$("#mainLeft").css({ "height": "auto" });//设置主布局高度
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
    var formatTypeID = typeID;
    if (typeID == 1 || typeID == 4) { formatTypeID = 14; }//1、4都是Table，所以用一个ID=14表示，省的考虑2种情况，这样只需要考虑一种14即可。
    //查找当前是否已有相同的类型，如果有就显示即可，不必再查询
    var _typeEve = $("div.desc_" + formatTypeID + "_" + typeName);
    var isHasType = _typeEve.length;
    if (isHasType > 0) {
        var _tabID = _typeEve.data("tabID");
        ActionToTabs(_tabID);
        return;
    }
    var loading = SearchLoading({ Title: '加载中...', TargetID: '#mainRight' });
    loading.Show();
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
            var newWrapper = $("#content_wrapper").find("div.accordian_header:last");//最新的tab内容
            newWrapper.addClass("tab_" + SearchData.TabIndex);//给内容设置特定的class
            newWrapper.addClass("desc_" + formatTypeID + "_" + typeName);//给一个唯一的class便于查找,用TypeID+typename,防止重复出现。
            newWrapper.data("tabID", SearchData.TabIndex);//给内容设置ID，便于后期使用
            EventTabAction();//注册Tab事件           
            if (formatTypeID == 14) {//Table模式
                var options = {
                    DBID: ServcieData.ID,     //当前选择的数据库的配置ID
                    TypeName: typeName,    //当前的数据类型的名称、表名、存储过程名等
                    TabID: SearchData.TabIndex   //当前TabID 
                }
                var a = new TabContainer(options).InitAction();
            } else {//其他模式，存储过程+表函数
                var codeEve = $("#content_wrapper").find("div.accordian_header:last").find('code')[0];
                //hljs.highlightAll();//执行插件，设置代码高亮highlightAuto(code, languageSubset) highlightElement(element)
                hljs.highlightElement(codeEve);
                ShowLineNo(SearchData.TabIndex);//显示行号
            }
            ActionToTabs(SearchData.TabIndex);//触发当前Tab按钮的点击事件，进行切换
        },
        error: function (x, s, e) {
            layer.msg("系统异常", { icon: 2, shade: 0.1 }, function () {
            });
        },
        complete: function () {
            SearchLoading().CloseAll();
        }
    })
}
/**
 * 弹出编辑说明 * 
 * @param {any} row
 * @param {any} type
 */
function EditColumnDesc(row, type) {
    var id = ServcieData.ID;
    //layer.alert("选择：" + row.TableName + "." + row.ColumnName + "[" + row.ColumnDataType + "(" + row.ColumnDataLength + ")" + "]");
    var _openHtml = $("#pop_UpdateColumnDesc").html();
    var titleName = '';
    var tableName = row.TableName;
    var columnName = '';
    var columnInfo = '';
    var desc = '';
    var tableNameClass = '';
    var TypeID = 0;
    if (type == "column") {
        titleName = '列名';
        columnName = "<b>.</b>" + row.ColumnName;
        columnInfo = " [" + row.ColumnDataType + "(" + row.ColumnDataLength + ")" + "]";
        desc = row.ColumnDesc;
        TypeID = 2;
        tableNameClass = 'text-secondary';
    } else if (type == "table") {
        titleName = '表名';
        desc = row.TableDesc;
        tableNameClass = 'text-danger';
        TypeID = 1;
    }
    _openHtml = _openHtml.replace("{TitleName}", titleName).replace("{TableName}", tableName).replace("{TableNameClass}", tableNameClass).replace("{ColumnName}", columnName).replace("{ColumnInfo}", columnInfo).replace("{Desc}", desc);
    layer.open({
        type: 1,
        content: _openHtml,
        area: ['620px', '460px'],
        title: "修改说明",
        shade: 0.6,
        shadeClose: false,
        btnAlign: 'r',
        maxmin: false,
        btn: ["关闭"],
        success: function () {
            $(".chose_CurrentDesc").focus();
            var data = {
                ID: ServcieData.ID,
                TypeID: TypeID,
                TableName: tableName,
                TableColumn: row.ColumnName,
                Description: '',
            }
            $(".save_submit").unbind('click').bind('click', function (e) { SaveTableColumnDesc(e, data) });
        }
        //
    })
}
/**
 * 保存说明，提交
 * @param {any} e
 * @param {any} config
 */
function SaveTableColumnDesc(e, config) {
    var _thisForm = $(e.currentTarget).parents('form');
    var descContent = _thisForm.find(".chose_CurrentDesc").val();
    var parmater = {
        ...config,
        Description: descContent,
    }
    layer.msg("savwe..==" + JSON.stringify(parmater))
    $.ajax({
        url: '/database/UpdateTableColumnDesc/',
        type: 'POST',
        data: parmater,
        dataType: 'JSON',
        success: function (data) {
            if (data.status) {
                layer.closeAll();
                layer.msg("更新成功！");
                var _typeEve = $(".accordian_header.active");
                var currentTabID = 0;
                if (_typeEve.length > 0) {
                    currentTabID = _typeEve.data("tabID");
                }
                //刷新表格
                var options = {
                    TabID: currentTabID   //当前TabID 
                }
                var curContainer = new TabContainer(options);
                curContainer.TableRefresh();
            } else {
                layer.msg('更新失败：' + data.message);
            }
        },
        error: function (x, s, e) {
            layer.msg("系统异常", { icon: 2, shade: 0.1 }, function () {
            });
        }
    })
}
/**
 * table search自定义事件
 * @param {any} text
 */
function TableSearch(data, text) {
    if (text == undefined) text = '';
    var searchResultData = data.filter(function (row) {
        return (row.ColumnName.toLowerCase() + "").indexOf(text.toLowerCase()) > -1
    })
    return searchResultData;
}

/**
 * table search后调用
 */
function TableOnSearch(txt) {
    console.log('search:' + txt);
}
/**
 * 注册打开的选项卡的切换和关闭事件
 */
function EventTabAction() {
    //切换Tabs选项卡
    $(".tab_list li").unbind('click').bind("click", function () {
        //$(this).addClass("active").siblings().removeClass("active");
        var tabID = $(this).data("tab");
        ActionToTabs(tabID);
    });
    $(".tab_list .close").unbind('click').bind("click", function () {
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
function ShowLineNo(tabID) {
    if (tabID == '') {
        $("code").each(function (i, n) {
            $(n).html("<ol><li>" + $(n).html().replace(/[\r\n]/g, "</li><li>") + "</li></ol>");
        });
    } else {
        var parent = $(".content_wrapper");
        var codeEve = parent.find(".accordian_header.tab_" + tabID).find('code');
        $(codeEve).html("<ol><li>" + $(codeEve).html().replace(/[\r\n]/g, "</li><li>") + "</li></ol>");
    }
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
        ShowLineNo('');
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
    var eve_Right = $("#mainRight");
    if ($(".accordian_header.active").length > 0) {
        eve_Right = $(".accordian_header.active");
    }
    var mainRight = $("#mainRight");
    var rightTabHeight = $("#right_container").find(".tab_list").height();
    var winHeight = $(window).height();
    var headerHeight = $("header").height();
    var footerHeight = $("footer").height();
    var winSetH = winHeight - headerHeight - footerHeight - 30;//当前页面可现实的高度
    var leftH = eve_Left.height();
    var rightH = eve_Right.height();//右侧当前动态高度
    // - footerHeight - rightTabHeight - 20;
    var setHeight = Math.max(winSetH, rightH);
    //console.log('winSetH = ' + winSetH + '  rightH = ' + rightH + '  setHeight = ' + setHeight);
    var masterH = 0;//master主容器的高度
    var spiderHeight = setHeight;//分割块的高度
    if (winSetH > rightH) {
        masterH = winSetH + footerHeight + rightTabHeight - 12;
    } else {
        masterH = winSetH + footerHeight + rightTabHeight + 80;
        spiderHeight = spiderHeight - footerHeight - rightTabHeight;
    }
    //$("#masterMain").css({ "height": masterH + "px" });
    $("#masterMain").css({ "height": "auto" });
    eve_Left.css({ "height": setHeight + "px" });
    var searchFormH = $(".searchForm").height();
    var searchBtnnavBarH = $(".searchBtnnavBar").height();
    $("#searchListContent").css({ "height": (setHeight - searchFormH - searchBtnnavBarH - 40) + "px", "overflow": "hidden", "overflow-y": "auto" });//设置搜索的结果展示部分高度.
    $(".gutter-horizontal").css({ "height": spiderHeight + "px" });
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
/**
 * 每开启一个tab，则初始化一个对象，然后在对象内操作各种事件
 * TabContainer
 */
(function () {
    var TabContainer = function (options) {
        var _this = this;
        _this.option = {
            DBID: 0,     //当前选择的数据库的配置ID
            TypeName: '',    //当前的数据类型的名称、表名、存储过程名等
            TabID: 0,   //当前TabID
            BootStrapTable: null,    //当前bootstrap-Table的对象
        }
        _this.option = { ..._this.option, ...options };
        if (TabContainerList.length > 0) {
            var matchContainer = TabContainerList.filter((n, i, self) => {
                if (n.option.TabID == _this.option.TabID) {
                    return n;
                }
            });
            if (matchContainer != undefined && matchContainer != null && matchContainer.length > 0) return matchContainer[0];
        }
        TabContainerList.push(_this);

        _this.InitAction = function () {
            layer.msg("开始");
            var newWrapper = $(".tab_" + this.option.TabID);
            var tableEve = newWrapper.find('table')[0];
            table_options.url = '/database/GetTableData/?ID=' + this.option.DBID + "&tableName=" + this.option.TypeName;
            _this.option.BootStrapTable = $(tableEve).bootstrapTable(table_options);//用bootstrap-table 插件初始化Table
            //注册几个事件
            newWrapper.find("button.btn_table_refresh").on('click', function () { _this.TableRefresh() });
            newWrapper.find("button.btn_table_export").on('click', function () {
                layer.msg('功能待开发...');
            });
            tooltipTrigger();
        }
        /**
         * 刷新表格数据
         */
        _this.TableRefresh = function () {
            if (_this.option.BootStrapTable == null) return;
            $(_this.option.BootStrapTable).data('bootstrap.table').refresh();
        }
        return _this;
        //TabContainer.InitAction();
    }
    //全局
    if (typeof window !== 'undefined') {
        window.TabContainer = TabContainer;
        window.TabContainerList = [];
    }

})();
