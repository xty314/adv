var next=0;
$(function () {
    var browserHeight=getClientHeight();
    var browserWidth=getClientWidth();
console.log("height:"+browserHeight+",width:"+browserWidth);
//height:697,width：574
//height:643,width：574
//height:982,width：745
// if(browserWidth>575){
//     var d= $(".wrapper img").css("width","400px");
    
//  }

// var carousel=document.getElementsByClassName("carousel")[0]
//     carousel.title=browserHeight;
    var refreshTime=6*60*60*1000
    setTimeout(function() {
        window.location.reload();
    }, refreshTime);
    var $wrapper=$(".wrapper")		
    var qty=$wrapper.length;
    var times=[];
    for(var i=0;i<qty;i++){
        var temp=$($(".wrapper")[i]).data("time");
        times.push(temp)
    }
    var t=times[next]*1000;
        $($(".wrapper")[next]).fadeIn()
        var set1 = setInterval(fn, t);
    function fn() {							
        // var qty=$wrapper.length;
        next++;
        $(".wrapper").hide()
            $($(".wrapper")[next]).fadeIn()
    
        if(next<qty){
            t = times[next]*1000;
            clearInterval(set1);
            set1 = setInterval(fn, t);
        }else{
            next=0
            t = times[next]*1000;
            $(".wrapper").hide()
            $($(".wrapper")[next]).fadeIn()
        }				
    }
});
function refresh(){
    window.location.reload();
}
function getClientWidth(){
    return window.innerWidth 
    || document.documentElement.clientWidth 
    || document.body.clientWidth;
}
function getClientHeight(){
    return window.innerHeight 
    || document.documentElement.clientHeight 
    || document.body.clientHeight; 
}