

/**
    * 添加数据库弹窗
    */
function PopwinAddDataBaseConfig(id) {
    var url = CommonSetting.Domain + 'DataBase/_ViewAddDataBase';
    if (id != undefined || id != null) {
        url = url + "/?id=" + id;
    }
    layer.open({
        type: 2,
        content: [url, 'yes'],
        area: ['780px', '550px'],
        title: "添加数据库",
        shade: 0.6,
        shadeClose: false,
        btnAlign: 'c',
        maxmin: false,
        //btn: ["  确定  ", "取消"],
        //
    })
}

/**
      * 删除一条数据库配置
      */
function deleteDatabase(id) {
    if (id == undefined || id == null || id == "") {
        layer.msg("删除：ID参数缺失"); return false;
    }
    var IDval = parseInt(id);
    if (IDval == NaN || IDval <= 0) {
        layer.msg("删除：ID参数错误"); return false;
    }
    layer.confirm('确定删除吗？', {
        title: '请确认',
        shadeClose: true,
        btn: ['确定', '取消'] //按钮
    }, function () {
        $.ajax(
            {
                url: CommonSetting.Domain + 'DataBase/DeleteDatabase/',
                type: 'POST',
                data: { ID: id },
                dataType: "text",
                success: function (data) {
                    var resObj = JSON.parse(data);
                    if (resObj.status == true) {
                        layer.msg(resObj.message, { icon: 1 });
                        setTimeout(function () { window.location.reload(); }, 1500);
                    } else {
                        layer.msg(resObj.message);
                    }
                },
                error: function (s, x) {
                    layer.msg("请求异常");
                    console.log(s);
                },
                complate: function (s, x) {
                    layer.close(layorIndex);
                    console.log(s);
                }
            })
    }, function () {
        //取消
    });
}