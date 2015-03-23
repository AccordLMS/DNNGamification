var editBadge =
{
    onUnitChanged: function (sender, args) {
        var tbQuantity = null;

        if (sender._value == '-1') {
            if ((tbQuantity = $('#' + tbQuantityCID)) != null) {
                tbQuantity.val('');
            }
        }

        return false;
    },
    onValidateQuantity: function (sender, args) {
        var cbUnit = null;

        if ((cbUnit = $find(cbUnitCID)) != null) {
            args.IsValid = (cbUnit._value != '-1' && args.Value != '') || cbUnit._value == '-1';
        }
    },
    onValidateImage: function (sender, args) {
        var hfFileId = null; args.IsValid = false;

        if (hfFileId = $('#' + fsBadgeImageCID + ' input[type="hidden"]')) {
            var hiddenId = hfFileId.val(); args.IsValid = hiddenId && hiddenId > -1;
        }
    },

    resetExpiration: function () {
        var tbQuantity = null, cbUnit = null;

        if ((cbUnit = $find(cbUnitCID)) != null) {
            var item = cbUnit.findItemByValue('-1'); if (item) item.select();
        }
        if ((tbQuantity = $('#' + tbQuantityCID)) != null) {
            tbQuantity.val('');
        }

        return false;
    },

    init: function () { }
}