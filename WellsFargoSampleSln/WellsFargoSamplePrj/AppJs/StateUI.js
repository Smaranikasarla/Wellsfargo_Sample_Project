
StateUI = {

        ViewCountryData: function (jData) {

            if ($.fn.dataTable.isDataTable('#tblState')) {
                var stateTable = $('#tblState').DataTable();
                stateTable.clear().draw();
                stateTable.rows.add(jData).draw();
            }
            else {
                this.InitializeStateTable(jData);
            }
            
    },

        InitializeStateTable: function (data) {

            try {

                $('#tblState').dataTable({
                    "sPaginationType": "bootstrap",
                    "iDisplayLength": 10,
                    "oLanguage": {
                        "sLengthMenu": '<select class="pagedropdown">' +
                        '<option value="10">10</value>' +
                        '<option value="20">20</value>' +
                        '<option value="30">30</value>' +
                        '</select><div style="font-weight:normal;padding-top:5px;width: 200px;">&nbsp;Results per page</div>'
                    },
                    "data": data,
                    "columns": [
                        { title: "State", data: "name", sClass: "center" },
                        { title: "Capital", data: "capital", sClass: "center" },
                        { title: "Largest City", data:"largest_city", sClass:"center"}
                    ],
                    "fnCreatedRow": function (row, data, index) {

                    }
                });

            } catch (e) {
                console.log(e);
            }
           
        },

    };