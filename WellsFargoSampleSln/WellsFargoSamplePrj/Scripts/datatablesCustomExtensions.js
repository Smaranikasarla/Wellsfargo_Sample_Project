(function ($, window, document) {
    if ($.fn.dataTableExt && $.fn.dataTableExt.oApi) {
        /* Data Table Pagination Functions */
        $.fn.dataTableExt.oApi.fnPagingInfo = function (oSettings) {
            return {
                "iStart": oSettings._iDisplayStart,
                "iEnd": oSettings.fnDisplayEnd(),
                "iLength": oSettings._iDisplayLength,
                "iTotal": oSettings.fnRecordsTotal(),
                "iFilteredTotal": oSettings.fnRecordsDisplay(),
                "iPage": Math.ceil(oSettings._iDisplayStart / oSettings._iDisplayLength),
                "iTotalPages": Math.ceil(oSettings.fnRecordsDisplay() / oSettings._iDisplayLength)
            };
        }
    }
})(jQuery, window, document);

(function ($, window, document) {
    if ($.fn.dataTableExt) {
        /*This is a datatables plugin extension for implementing custom pagination */
        $.extend($.fn.dataTableExt.oPagination, {
            "bootstrap": {
                "fnInit": function (oSettings, nPaging, fnDraw) {
                    var oLang = oSettings.oLanguage.oPaginate;
                    var fnClickHandler = function (e) {
                        e.preventDefault();
                        if (oSettings.oApi._fnPageChange(oSettings, e.data.action)) {
                            fnDraw(oSettings);
                        }
                    };

                    $(nPaging).addClass('pagination').append(
                        '<ul>' +
                        '<li class="prev disabled"><a href="#"> |< </a></li>' +
                        '<li class="prev disabled"><a href="#"> < </a></li>' +
                        '<li id="linext" class="next disabled"><a href="#"> > </a></li>' +
                        '<li class="next disabled"><a href="#"> >| </a></li>' +
                        '</ul>'
                    );
                    var els = $('a', nPaging);
                    $(els[0]).bind('click.DT', { action: "first" }, fnClickHandler);
                    $(els[1]).bind('click.DT', { action: "previous" }, fnClickHandler);
                    $(els[2]).bind('click.DT', { action: "next" }, fnClickHandler);
                    $(els[3]).bind('click.DT', { action: "last" }, fnClickHandler);
                },

                "fnUpdate": function (oSettings, fnDraw) {
                    var iListLength = 5;
                    var oPaging = oSettings.oInstance.fnPagingInfo();
                    var an = oSettings.aanFeatures.p;
                    var i, j, sClass, iStart, iEnd, iHalf = Math.floor(iListLength / 2);
                    $('[data-toggle="tooltip"]').tooltip();
                    if (oPaging.iTotalPages < iListLength) {
                        iStart = 1;
                        iEnd = oPaging.iTotalPages;
                    }
                    else if (oPaging.iPage <= iHalf) {
                        iStart = 1;
                        iEnd = iListLength;
                    }
                    else if (oPaging.iPage >= (oPaging.iTotalPages - iHalf)) {
                        iStart = oPaging.iTotalPages - iListLength + 1;
                        iEnd = oPaging.iTotalPages;
                    }
                    else {
                        iStart = oPaging.iPage - iHalf + 1;
                        iEnd = iStart + iListLength - 1;
                    }

                    for (i = 0, iLen = an.length; i < iLen; i++) {
                        // Remove the middle elements
                        $('li:gt(1)', an[i]).filter(':not(.next)').remove();

                        var pageinfo = oPaging.iPage + 1 + " of " + oPaging.iTotalPages;

                        $('<li><a style="color:#ccc">' + pageinfo + '</a></li>')
                            .insertBefore($('#linext', an[i])[0])
                            .bind();

                        // Add / remove disabled classes from the static elements
                        if (oPaging.iPage === 0) {
                            $('li.prev', an[i]).addClass('disabled');
                        }
                        else {
                            $('li.prev', an[i]).removeClass('disabled');
                        }

                        if (oPaging.iPage === oPaging.iTotalPages - 1 || oPaging.iTotalPages === 0) {
                            $('li.next', an[i]).addClass('disabled');
                        }
                        else {
                            $('li.next', an[i]).removeClass('disabled');
                        }
                    }
                }
            }
        });

        jQuery.fn.dataTableExt.oSort['alphaNumericsort-asc'] = function (a, b) {
            var re = new RegExp("^([a-zA-Z]*)(.*)");
            var x = re.exec(a);
            var y = re.exec(b);

            // you might want to force the first portion to lowercase
            // for case insensitive matching
            // x[1] = x[1].toLowerCase();
            // y[1] = y[1].toLowerCase();

            if (x[1] > y[1]) return 1;
            if (x[1] < y[1]) return -1;

            // if you want to force the 2nd part to only be numeric:
            x[2] = parseInt(x[2]);
            y[2] = parseInt(y[2]);

            return ((x[2] < y[2]) ? -1 : ((x[2] > y[2]) ? 1 : 0));
        };

        jQuery.fn.dataTableExt.oSort['alphaNumericsort-desc'] = function (a, b) {
            var re = new RegExp("^([a-zA-Z]*)(.*)");
            var x = re.exec(a);
            var y = re.exec(b);

            // you might want to force the first portion to lowercase
            // for case insensitive matching
            // x[1] = x[1].toLowerCase();
            // y[1] = y[1].toLowerCase();

            if (x[1] > y[1]) return -1;
            if (x[1] < y[1]) return 1;

            // if you want to force the 2nd part to only be numeric:
            x[2] = parseInt(x[2]);
            y[2] = parseInt(y[2]);

            return ((x[2] < y[2]) ? 1 : ((x[2] > y[2]) ? -1 : 0));
        };
    }

    $.extend(true, $.fn.dataTable.defaults, {
        "sDom": "<'row'r>ft<'row'<'col-md-6'l><'col-md-6'p>>",  
        "sPaginationType": "bootstrap",
        "iDisplayLength": 100,
        "oLanguage": {
            "sLengthMenu": '<select class="pagedropdown">' +
            '<option value="100">100</value>' +

            '<option value="150">150</value>' +
            '<option value="200">200</value>' +
            '</select><label style="font-weight:normal;padding-top:5px;">&nbsp;Results per page</label>'
        },
        "bAutoWidth": false,
        "bSortable": true,
        "bFilter": true,  
        "bStateSave": false, 
        aaSorting: [],
        "deferRender": true,
        ordering: true,
        "order": []


    });
})(jQuery, window, document);