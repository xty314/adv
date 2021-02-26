$(document).on("click", ".delete-btn", function () {
  
    var company = $(this).data("company");
    $("#DeleteModalBtn").val(company);
    $("#DeleteModalBody").html("Are you sure to delete " + company + "?<br>This action cannot be undone!!!");
 
})
$(document).on("click", ".rename-btn", function () {
  
    var company = $(this).data("company");
    $("#RenameModalBtn").val(company);
    $("#RenameModalHeader").html(company);

})

var selectorx = $('.duallistbox').bootstrapDualListbox({
    nonSelectedListLabel: 'Unselected Categories',
    selectedListLabel: 'Selected Categories',
    selectorMinimalHeight:300,
    filterTextClear: 'Show All',
    filterPlaceHolder: 'Filter',
    moveSelectedLabel: "Add",
    moveAllLabel: 'Select All',
    removeSelectedLabel: "Remove",
    removeAllLabel: 'Remove All',
    infoText: 'total {0} categories',
    infoTextFiltered: 'get {0} categories ,total{1}categories',
    infoTextEmpty: 'empty list',
});
$(document).on("click", ".select-btn", function () {
    //selectorx.bootstrapDualListbox('destroy');
    var company = $(this).data("company");
    $("#CompanyCategoryModalHeader").html(company);
    var categories = $(this).data("categories").split(',');
    $(".duallistbox").find("option").each(function (index, each) {

        if (categories.indexOf($(each).val()) != -1) {
            $(each).attr("selected", true)
        } else {
            $(each).attr("selected", false)
        }
        selectorx.bootstrapDualListbox('refresh');

    })
    $("#SelectModalBtn").val(company);
})
