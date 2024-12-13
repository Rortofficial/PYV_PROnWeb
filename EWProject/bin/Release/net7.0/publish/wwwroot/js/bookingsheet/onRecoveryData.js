
function onRecoveryData() {
    for (var i = 0; i < localStorage.length; i++) {
        $("#" + localStorage.key(i)).val(localStorage.getItem(localStorage.key(i)));
    }
}

    // Remove key with its value
    //localStorage.removeItem("color");

    // Delete everything
    //localStorage.clear();