Object.keys(window).forEach(key => {
    if (/^on/.test(key)) {
        window.addEventListener(key.slice(5), event => {
            if (event.type == "change") {
                //console.log(event.path);
                //console.log(event.target.getAttribute("type"));
                //console.log(event.target.getAttribute("id"));
                //Combobox
                if (event.target.getAttribute("id") == "cboSelectSeries") {
                    var obj = {};
                    var dataObject = [];
                    obj.id = event.target.getAttribute("id");
                    obj.value = $("#" + event.target.getAttribute("id")).val();
                    obj.dataObject = dataObject;
                    localStorage.setItem(event.target.getAttribute("id"), JSON.stringify(obj));
                    obj = {};
                    dataObject = [];
                    obj.id = "cboImportType";
                    obj.value = $("#cboImportType").val();
                    obj.dataObject = dataObject;
                    localStorage.setItem("cboImportType", JSON.stringify(obj));
                } else if (event.target.getAttribute("id") === "cboOrigin") {
                    var obj = {};
                    var dataObject = [];
                    obj.id = event.target.getAttribute("id");
                    obj.value = $("#" + event.target.getAttribute("id")).val();
                    obj.dataObject = dataObject;
                    localStorage.setItem(event.target.getAttribute("id"), JSON.stringify(obj));
                } else if (event.target.getAttribute("id") == "cboSalePerson") {
                    var obj = {};
                    var dataObject = [];
                    obj.id = event.target.getAttribute("id");
                    obj.value = $("#" + event.target.getAttribute("id")).val();
                    obj.dataObject = dataObject;
                    localStorage.setItem(event.target.getAttribute("id"), JSON.stringify(obj));
                } else if (event.target.getAttribute("id") == "cboServiceType") {
                    var obj = {};
                    var dataObject = [];
                    obj.id = event.target.getAttribute("id");
                    obj.value = $("#" + event.target.getAttribute("id")).val();
                    obj.dataObject = dataObject;
                    localStorage.setItem(event.target.getAttribute("id"), JSON.stringify(obj));
                }
                //End Combobox
            }
        });
    }
});