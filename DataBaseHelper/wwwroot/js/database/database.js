

/**
    * 添加数据库弹窗
    */
function PopwinAddDataBaseConfig(id) {
    var url = '/DataBase/_ViewAddDataBase';
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