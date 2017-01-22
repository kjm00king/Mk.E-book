var isMain = true;

// Initialize your app
var myApp = new Framework7();

// Export selectors engine
var $$ = Dom7;

// Add view
var mainView = myApp.addView('.view-main', {
    // Because we use fixed-through navbar we can enable dynamic navbar
    dynamicNavbar: true
});

// Callbacks to run specific code for specific pages, for example for About page:
myApp.onPageInit('about', function (page) {
    // run createContentPage func after link was clicked
    $$('.create-page').on('click', function () {
        createContentPage();
    });
});

//if(window.location.hash.length > 0){
//    isMain = false;
//    var hashstr = window.location.hash.replace("#","");

//    $(".navbar").hide();
//    $(".toolbar").hide();

//    mainView.loadPage({ url: hashstr + ".html", animatePages:false });
//}
//else
//    $("body").css("opacity",1);

var page_route = [
    { from:"index", to:"list"},
    { from:"list", to:"1.1"},
    { from:"1.1", to:"2.1"},
    { from:"2.1", to:"3.1"},
    { from:"3.1", to:"4.1"},
    { from:"4.1", to:"4.2"},
    { from:"4.2", to:"5.1"},
    { from:"5.1", to:"5.2"},
    { from:"5.2", to:"5.3"},
    { from:"5.3", to:"6.1"},
]

//function hash(name){
//    window.location.replace("index.html#" + name);
//} 

//function goto(name){
//    mainView.loadPage(name + ".html");
//}

//function prev(){
//    var from = page_route.filter(function(v){
//        return mainView.url.indexOf( v.to ) >= 0;
//    });
//    if(from.length>0){
//        mainView.back({url: from[0].from + ".html", force:true });
//    }
//}

//function next(){
//    var to = page_route.filter(function(v){
//        return mainView.url.indexOf( v.from ) >= 0;
//    });
//    if(to.length>0){
//        mainView.loadPage(to[0].to + ".html");
//    }
//    else{
//        mainView.loadPage("list.html");
//    }
//}

//function init(){

//    if(!isMain){

//        $(".page-content").css("padding-top","0px");
//        $(".page-content").css("padding-bottom","0px");
//        $(".content-block").css("margin","0px");
            
//    }

//    if(mainView.url.indexOf("index")>=0)
//        hash("")
//    else
//        hash(mainView.url.replace(".html",""));

//    //hash(mainView.url);
//    ( mainView.url.indexOf( "index" ) >= 0 ) ? $(toPrev).hide(): $(toPrev).show();
//    ( mainView.url.indexOf( "6.1" ) >= 0 ) ? $(toNext).hide(): $(toNext).show();
//    var p = $("[name=page]");
//    if(p){
//        p.bind("load",function(){
//            $(".preloader").hide(); 
//        })
//    }
//}

//function home(){ mainView.loadPage("index.html"); }
//function list(){ mainView.loadPage("list.html"); }

//$(mainView.container).bind("pageInit",function(){ init(); })
//$(mainView.container).bind("pageAfterAnimation",function(){
//    $("body").css("opacity",1);
//})
//$(mainView.container).bind("pageAfterBack",function(){ init(); })

//if(isMain){
//    $(toPrev).hide();
//    //hash("");
//}

//init();

//$(toPrev).hide()

// Generate dynamic page
var dynamicPageIndex = 0;
function createContentPage() {
	mainView.router.loadContent(
        '<!-- Top Navbar-->' +
        '<div class="navbar">' +
        '  <div class="navbar-inner">' +
        '    <div class="left"><a href="#" class="back link"><i class="icon icon-back"></i><span>Back</span></a></div>' +
        '    <div class="center sliding">Dynamic Page ' + (++dynamicPageIndex) + '</div>' +
        '  </div>' +
        '</div>' +
        '<div class="pages">' +
        '  <!-- Page, data-page contains page name-->' +
        '  <div data-page="dynamic-pages" class="page">' +
        '    <!-- Scrollable page content-->' +
        '    <div class="page-content">' +
        '      <div class="content-block">' +
        '        <div class="content-block-inner">' +
        '          <p>Here is a dynamic page created on ' + new Date() + ' !</p>' +
        '          <p>Go <a href="#" class="back">back</a> or go to <a href="services.html">Services</a>.</p>' +
        '        </div>' +
        '      </div>' +
        '    </div>' +
        '  </div>' +
        '</div>'
    );
	return;
}
