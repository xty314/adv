
$(function () {
    $('.connectedSortable').sortable({
        placeholder: 'sort-highlight',
        connectWith: '.connectedSortable',
        handle: '.card-body, .card-header',
        forcePlaceholderSize: true,
        zIndex: 999999
    })
    $('.connectedSortable .card-header').css('cursor', 'move')

    //Dropzone.autoDiscover = false
    //$(".dropzone").dropzone({
    //    autoDiscover:false,
    //    autoProcessQueue: false,
    //});
 

    //var myDropzone = new Dropzone("div#UploadDiv", {
    //    url: "category_detail.aspx",
    //    autoProcessQueue: false,
    //    thumbnailWidth: 180,
    //    thumbnailHeight:180,
    //});
    //$('#UploadModalBtn').click(function () {
    //    myDropzone.processQueue();
    //});

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
Dropzone.options.UploadPicForm = {
    paramName: "file", // The name that will be used to transfer the file
    params: { cmd: "upload" },
   // uploadMultiple:true,
    maxFilesize: 100, // MB
    acceptedFiles:".jpg,.jpeg,.gif,.png,.bmp",
    //accept: function (file, done) {
    //    console.log(file, done)
    //    if (file.name == "justinbieber.jpg") {
    //        done("Naha, you don't.");
    //    }
    //    else {
    //        done();
    //        //window.location.reload();
    //    }
    //},
   // autoProcessQueue:false
};
$(document).on("click", "#UploadModalBtn", function (e) {
    window.location.reload();
    //var myDropzone = Dropzone.forElement(".dropzone");
    //myDropzone.processQueue();
 
})
//Dropzone.options.mydrop = false;
$(document).on("click", ".save-btn", function (e) {
    $(".img-file").each(function (index, each) {
        var seq= $(each).find(".seq").val(1000+index);
    
    })
})

$(document).on("click",".copy-btn",function (e) {
    var filename=$(this).data("name");
    console.log(filename)
    $("input[name='file']").val(filename)
})
$(document).on("click",".move-btn",function (e) {
    var filename=$(this).data("name");
    var src=$(this).data("src")
    $("input[name='file']").val(filename)
    $("#move_pic").attr("src",src);
})