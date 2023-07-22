/**
    * 添加数据库弹窗
    */
function PopwinAddDataBaseConfig() {
    layer.open({
        type: 2,
        content: ['/DataBase/_ViewAddDataBase', 'yes'],
        area: ['660px', '420px'], 
        title: "添加数据库",
        shade: 0.6,
        shadeClose: false,
        btnAlign:'c',
        maxmin: false,
        btn: ["  确定  ", "取消"],
        yes: function (index,layero) {
            layer.msg("OK");
        }
    })

}