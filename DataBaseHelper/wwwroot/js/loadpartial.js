
jQuery.extend({
    loadPartial: function (options) {
        var defaults = { link: '.nav a', container: '#razorContainer', updateNow:true };
        defaults = $.extend(defaults, options);
        razorContainer = $(defaults.container);
        $(defaults.link).each(alink);
        if (defaults.updateNow) alinkInContainer();
        function alink(idx, link) {
            if ($(link).attr("onclick")) return;
            if (link.href.indexOf('###') >= 0) return;
            var addr1 = link.href.split('#');
            var addr2 = location.href.split('#');
            if (addr1[0] == addr2[0]) return; 
            $(link).click(function (e) {
                e.preventDefault();
                if ($(link).attr("action")) {
                    ConfirmDelete(link);
                } else {
                    getRazor(link.href, null, 'get');
                }
            });
        }
        function alinkInContainer() {
            razorContainer.find('a').each(alink);
            razorContainer.find('form').submit(function (e) {
                e.preventDefault();
                getRazor(this.action, $(this).serialize(), "post");
            });
        }
        function getRazor(url, d, m) {
            $.ajax({
                url: url,
                data: d,
                cache: false,
                type: m,
                success: function (data) {
                    var r = $("<div>").append($.parseHTML(data, true)).find('#partial');
                    razorContainer.html(r);
                    showTips();
                    alinkInContainer();
                },
                error: function (response, status, xhr) {
                    razorContainer.html(response.responseText);
                }
            });
        }
        function showTips(tips) {
            var tipsDiv = $('#divTips');
            var tipsType = tipsDiv.find(":input[type='hidden']");
            if (tips) {
                tipsDiv.find('strong').html(tips.Title);
                tipsDiv.find('span').html(tips.Message);
                tipsType.val(tips.Type);
            }
            tipsType = tipsType.val();
            if (!tipsType) return;
            tipsDiv.removeClass("alert-danger alert-warning alert-success alert-info");
            switch (tipsType) {
                case "error": tipsDiv.addClass('alert-danger');
                    break;
                case "warning": tipsDiv.addClass('alert-warning');
                    break;
                case "success": tipsDiv.addClass('alert-success');
                default: tipsDiv.addClass('alert-info');
                    setTimeout(function () { tipsDiv.hide(); }, 3000);
                    break;
            }
            tipsDiv.removeClass('hidden');
        }
        function ConfirmDelete(a) {
            if (!confirm('确定删除?')) return;
            var self = $(a);
            $.get(self.attr("action"), function () {
                getRazor(a.href, null, 'get');
            });
        }
    }
});
