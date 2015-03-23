var auxiliary =
{
    postback: function (sender, args) {
        if (args.get_eventTarget().indexOf("btnSave") > -1) {
            args.set_enableAjax(false);
        }
    }
}