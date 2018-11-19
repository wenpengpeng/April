function treeMenu(array) {
    this.tree = array || [];
    this.groups = {};
};
treeMenu.prototype = {
    init: function (parentId) {
        this.group();
        return this.getDom(this.groups[parentId]);
    },
    group: function () {
        for (var i = 0; i < this.tree.length; i++) {
            if (this.groups[this.tree[i].parentId]) {
                this.groups[this.tree[i].parentId].push(this.tree[i]);
            } else {
                this.groups[this.tree[i].parentId] = [];
                this.groups[this.tree[i].parentId].push(this.tree[i]);
            }
        }
    },
    getDom: function (item) {
        if (!item) { return ''; }
        var html = '';

        if (item != null && item[0].parentId == null) {
            html += '\n<ul class="sidebar-menu" data-widget="tree">\n';
        } else {
            html += '\n<ul class="treeview-menu">\n';
        }
        for (var i = 0; i < item.length; i++) {
            if (!item[i].hasLevel) {
                html += "<li>";
            }
            else {
                html += '<li class="treeview">';
            }
            if (!item[i].hasLevel) {
                html += '<a href="#/" ui-sref="' + item[i].code + '">' +
                    '<i class="' + item[i].icon + '" ></i>' +
                    '<span>' + item[i].displayName + '</span></a >';
                //html += '<a href="#" ui-sref="' + item[i].code + '">' +
                //    '<i class="fa '+ item[i].icon + '" ></i><span>'
                //    + item[i].displayName + "</span></a >";
            } else {
                html += '<a href="#"><i class="fa ' + item[i].icon + '">' +
                    '</i><span>' + item[i].displayName + '</span>' +
                    '<span class="pull-right-container">' +
                    '<i class="fa fa-angle-left pull-right">' +
                    '</i></span></a >';
                html += this.getDom(this.groups[item[i].id]);
            }
            html += "</li>\n";
        };
        html += "</ul>\n";
        return html;
    }
};