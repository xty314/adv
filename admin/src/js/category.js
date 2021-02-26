$(document).on("click", ".add-btn", function () {
    $("#NewCategoryHeader").html("New Category");
    $("#NewCategorySubmit").val("new");
})
$(document).on("click", ".copy-btn", function () {
    var copySourceCategory = $(this).data("category");
    $("#NewCategoryHeader").html("Copy Category " + copySourceCategory);
    $("#copy_source_category").val(copySourceCategory);
    $("#NewCategorySubmit").val("copy");
})
$(document).on("click", ".delete-btn", function (e) {
    var category = $(this).data("category");
    $("#DeleteModalBody").html("Are you sure to delete the category : " + category);
    $("#DeleteModalBtn").val(category);
})