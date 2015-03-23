var mechanics =
{
    _clientIds: [],

    keyPressed: function (e) {
        var key = e.keyCode;

        if (key == 13) {
            $get(btnSearchCID).click(); return false;
        }

        return true;
    },

    rebindActivities: function () {
        var self = this;
        var grid = null; if ((grid = $find(self._clientIds[0])) != null) {
            grid.get_masterTableView().rebind();
        }
    },
    rebindBadges: function () {
        var self = this;
        var grid = null; if ((grid = $find(self._clientIds[1])) != null) {
            grid.get_masterTableView().rebind();
        }
    },
    rebindAssignment: function () {
        var self = this;
        var grid = null; if ((grid = $find(self._clientIds[2])) != null) {
            grid.get_masterTableView().rebind();
        }
    },

    init: function () {
        var self = this;

        self._clientIds[0] = grdActivitiesCID;
        self._clientIds[1] = grdBadgesCID;
        self._clientIds[2] = grdAssignmentCID;

        $('#divTabs').dnnTabs();
    }
}