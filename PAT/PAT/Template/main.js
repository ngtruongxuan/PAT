
////////////////////////
///////////UA///////////
////////////////////////

var judgeAnimation ;

function checkUA(){

var _UA = navigator.userAgent;

if (_UA.indexOf('iPhone') > -1 || _UA.indexOf('iPod') > -1 || _UA.indexOf('iPad') > -1) {
	judgeAnimation = false;
}else if(_UA.indexOf('Android') > -1){
	judgeAnimation = false;
}else{　
	judgeAnimation = true;
}

}


/*-------SHADOWBOX-------*/

Shadowbox.init({
	overlayColor:"#000000",
	overlayOpacity:0.85,
	players:["swf","img","iframe","html"]
});

function setShadowbox(){
	if(!(android || iphone)){
		Shadowbox.clearCache();
		Shadowbox.setup("a[href*='window/'],a[href*='youtube.com/v/'],a[href*='youtube.com/embed/']",{
			modal:false
			});
	}else{
		console.log("done");
		for(var i=0;i<$("a[rel*='shadowbox']").length;i++){
			var o = $("a[rel*='shadowbox']").eq(i);
			if(o.attr("href").indexOf(".html") > -1){
				o.attr("target","_blank");
				o.attr("rel","");
			}
		}
	}

}

function openImage(uri){
	Shadowbox.open({
		content:uri,
		player:"img"
	});
}

function openHTML(uri,w,h){
	Shadowbox.open({
		content:uri,
		width:w,
		height:h,
		player:"iframe"
	});
}



function closeAndLink(html){
	Shadowbox.close();
	openContents(html);
}


////////////////////////
////////LOCATION////////
////////////////////////

var nowLocation ;
function checkLocation(){

var loc = window.location.pathname;

//topとそれ以外の場合とで分ける
if(loc.lastIndexOf("info_") > 0 ){nowLocation = "info"};
if(loc.lastIndexOf("ce") > 0 ){nowLocation = "info"};
if(loc.lastIndexOf("top") > 0 ){nowLocation = "top"};


switch(window.location.hash){

case '#Features':
setTimeout("scrollTo('#features','.menu_features','none');", 1000);
break;

case '#Characters':
setTimeout("scrollTo('#features','.menu_characters','none');", 1000);
break;

case '#Stages':
setTimeout("scrollTo('#features','.menu_stages','none');", 1000);
break;

case '#Movies':
setTimeout("scrollTo('#features','.menu_movies','none');", 1000);
break;

}
//if(window.location.hash == "#Features"){setTimeout("scrollTo('#features','.menu_features','none');", 1000);}


//if(loc.lastIndexOf("#features") > 0 ){setTimeout("scrollTo('#features','.menu_features','none');", 3000);}
//if(loc.lastIndexOf("#movies") > 0 ){setTimeout("scrollTo('#movies','.menu_movies','none');", 3000);}

}


////////////////////////
//////////AJAX//////////
////////////////////////

function menuSet(){
$('#menu').load('common/menu.html',
function(){setTimeout(function(){setMenuControl();},2000);}
);

}

function footerSet(){
$('#footer').load('common/footer.html');
}


////////////////////////
//////////INFO//////////
////////////////////////

var informationStatus = false;
var informationCount = 0;

var informationSize_pc = 1;
var informationSize_sp = 3;

function informationSet(){

var h;

if(informationStatus){

	h = $('#information ul').outerHeight(true);
	$('#information .btn_more').html('<img src ="img/top/information_close.png">');

}else{

	if($(window).width() <= 480){
		h = $('#information li:first').outerHeight(true)*informationSize_sp;
	}else{
		h = $('#information li:first').outerHeight(true)*informationSize_pc;
	}

	$('#information .btn_more').html('<img src ="img/top/information_more.png">');
}

$('#information div.inner').stop().animate({height:h},400,'swing');
$("#information ul div.log").load('information.html');

}


function infoMoreClick(){

	if(informationCount == 0){

	var h = $('#information ul').height();
	var $tgt = $('#information div.inner');
	var tgth = $tgt.height();

	if(h <= tgth){
		informationStatus = false ;
	}else{
		informationStatus = true ;
	}

	informationSet();

	informationCount = 1;

	return;
	}

	var h = $('#information ul').height();
	var $tgt = $('#information div.inner');
	var tgth = $tgt.height();

	if(h <= tgth){
		informationStatus = false ;
	}else{
		informationStatus = true ;
	}

	informationSet();

}





////////////////////////
//////////MENU//////////
////////////////////////



function scrollTo(targetName,menuName,targetURL){

if(nowLocation != 'top'){location.href = targetURL; return;};//top.html以外は通常リンク

var $scrollTarget = $(targetName);
var amount = $scrollTarget.offset().top;
amount -= 58;

$('html,body').animate({ scrollTop: amount }, 'swing');

}

//

var menuName = [];
var menuHeight = [];
var menuLength = 0;
var menuGaSwitch = [];

function setMenuControl (){

menuLength = $('#menu li').length;

for(var i = 0 ; i < menuLength ;i++){

	var mn = $('#menu li').eq(i).attr('class');
	mn = '.'+ mn;
	menuName.push(mn);
	menuGaSwitch.push(0);


	if(i > 0){
	var s = mn.indexOf("_");
	var l = mn.length;
	var tn = mn.substring(s+1 ,l);
	tn = '#'+ tn;

	var d = $(tn).offset().top;
	menuHeight.push(d);
	}else{
		menuHeight.push(0);
	}

}

//Socialは例外で最後に追加
var ds = $('#social').offset().top;
menuHeight.push(ds);

}


//


function menuControl(){

	var st = $(window).scrollTop();
	var sh = $(window).height();

	var offset = $(window).scrollTop();

	var position = 0;

	for(var i = 1; i <= menuLength ; i++){
		if( menuHeight[i] > offset + (sh/2) ){i == menuLength;}else{ position += 1;}
	}

	if(position < menuLength){
		$('#menu li a').removeClass('select');
		$('a', menuName[position]).addClass('select');
		if(menuGaSwitch[position] == 0){menuGaSwitch[position] = 1;gaScrollEvent(position);}
	}else{$('#menu li a').removeClass('select');}
}


function gaScrollEvent(num){
	ga('send', 'event', 'scroll_"'+menuName[num]+'"', 'scroll',{'nonInteraction': 1});
}



////////////////////////
/////////SCROLL/////////
////////////////////////


var inWindow = function(obj) {
	var st = $(window).scrollTop();
	var sh = $(window).height();

	obj = $(obj);
	var offset = obj.offset();

	if(offset){
		if(st <= offset.top + obj.height() && offset.top <= st + sh)
			return true;
	}
	return false;
};


var scrollMoveList = [];

function setMoveList(name,style,styleB,time,delay,loop){

if(loop == 'false'){loop = 1;}
else if(loop == 'true'){loop = -1;}

delay = parseInt(delay);
loop = parseInt(loop);

scrollMoveList.push({'name':name, 'style':style, 'styleB':styleB, 'time':time , 'delay':delay ,'loop':loop ,'flg':0,'dt':0});
}



function setStyle(){

for(var i = 0 ; i < scrollMoveList.length ; i++ ){
			var name = scrollMoveList[i].name;
			var styleB = scrollMoveList[i].styleB;
			$(name).css(styleB);
		}

}


function innerTopImageControll(){
if(nowLocation != "info" || !judgeAnimation){return;}

	var $tgt = $('#wrapper #topImage');
	var h = $('#wrapper #topImage').height();
	if(h != null){
		var st = $(window).scrollTop();
		if(st < 400){st = 1 - st/400;$tgt.css({opacity:st});}else{$tgt.css({opacity:0});}
	};
}



function inScroll(){

	if(!judgeAnimation){return;}

		//animeteStyle
		for(var i = 0 ; i < scrollMoveList.length ; i++){

		var loop = scrollMoveList[i].loop;

		if(loop != 0){

			var name = scrollMoveList[i].name;
			var flg = scrollMoveList[i].flg;

			if(inWindow(name)){
				if(flg == 0){

					var name = scrollMoveList[i].name;
					var style = scrollMoveList[i].style;
					var time = scrollMoveList[i].time;
					var delay = scrollMoveList[i].delay;

					if(delay > 0){
						$(name).delay(delay).animate(style,time,'swing');

					}else{
						$(name).animate(style,time,'swing');
					}

					scrollMoveList[i].loop = loop - 1;
					scrollMoveList[i].flg = 1;

				}

			}else{

				var name = scrollMoveList[i].name;
				var styleB = scrollMoveList[i].styleB;
				$(name).css(styleB);

				scrollMoveList[i].flg = 0;

			}

		}else{scrollMoveList.splice(i, 1);}//loop = false の場合は登録解除

	}


}




////////////////////////
//////LoadToLayer///////
////////////////////////

var standardHeight_features = {base:0,layerTop:500},
standardHeight_characters = {base:0,layerTop:500},
standardHeight_stages = {base:0,layerTop:500};

var stateHeight = {'features':'base', 'characters':'base', 'stages':'base'};

function setStanderdHeight(){//baseとLayerTopの値をstanderdHeightに格納
	standardHeight_features.base = $('#features_base').outerHeight();
	standardHeight_features.layerTop = $('#features_layerTop').outerHeight();
	standardHeight_characters.base = $('#characters_base').outerHeight();
	standardHeight_characters.layerTop = $('#characters_layerTop').outerHeight();
	standardHeight_stages.base = $('#stages_base').outerHeight();
	standardHeight_stages.layerTop = $('#stages_layerTop').outerHeight();
	e();
}

function e (){
console.log('f='+standardHeight_features.base + ',' + standardHeight_features.layerTop);
	console.log('c='+standardHeight_characters.base + ',' + standardHeight_characters.layerTop);
	console.log('s='+standardHeight_stages.base + ',' + standardHeight_stages.layerTop);
}

function loadToLayerTop(target,contents){
	$target = $(target);

	var end = target.indexOf('_');
	var tf = target.slice(0,end);
	var ta = tf + '_base';
	var $targetB = $(ta);

	var tn = target.slice(end+1,target.length);
	$target.load(contents,function(){setInitFlexSlider();setTimeout(function(){setStanderdHeight();switchHeight(tf,tn);},1500);setShadowbox();});

	$target.css({opacity:0,display:'block'}).stop().animate({opacity:1},500);
	$targetB.css({opacity:0,display:'none'});
}


function backToBase(target){
	$target = $(target);

	var end = target.indexOf('_');
	var tf = target.slice(0,end);
	var ta = tf + '_base'
	var $targetB = $(ta);

	var tn = target.slice(end+1,target.length);
	setStanderdHeight();switchHeight(tf,'base');

	console.log(tn);

	$targetB.css({opacity:1,display:'block'});
	$target.animate({opacity:0},500,function(){
	$target.empty().css({display:'none'});


	});

	$target.css({display:'block'}).animate({opacity:1},500);
}


function setInitFlexSlider(){
 $('.flexslider').flexslider({
    animation: "slide"
  });
 $('.flexslider_c').flexslider({
    animation: "slide",
    animationLoop: true,
	slideshowSpeed: 4000,
    itemMargin: 10,
	itemWidth: 200,
    minItems: 2,
    maxItems: 2
  });
}

function switchHeight(target,state){
	var tgt = target.slice(1,target.length);
	eval('stateHeight["' +tgt +'"] = "' + state +'"');

	ajustHeight(tgt);
}

function ajustHeight(tgt){
	if(tgt == 'all'){
		var state ,h;

		state = stateHeight['features'];
		h = eval('standardHeight_features.'+ state);
		$('#features').css({'height':h});

		state =  stateHeight['characters'];
		h = eval('standardHeight_characters.'+state);
		$('#caracters').css({'height':h});

		state =  stateHeight['stages'];
		h = eval('standardHeight_stages.'+state);
		$('#stages').css({'height':h});

	}else{
		var state, h;
		h = $('#'+tgt).height();
		if(h == 0){
		$('#'+tgt).css({height:700});
		console.log('n');
		}
		state =  eval('stateHeight["'+ tgt +'"]');
		h = eval('standardHeight_'+tgt+'.'+ state);
		console.log(h);
		$('#'+tgt).stop().animate({'height':h},300);
	}

}


////////////////////////
/////////LOADED/////////
////////////////////////


function setscrollFunction(){
$(window).scroll(function(){inScroll();innerTopImageControll();menuControl();})
.resize(function(){inScroll();informationSet();innerTopImageControll();menuControl();setStanderdHeight();ajustHeight('all');});
}

function hideLoadingMark(){
$("#loadingMark").css({display:'none'});
$("#contents").css({display:'block'});

}

function visualMovieAnimation(){
if(navigator.userAgent.indexOf("Firefox")==-1){
$("#visual").delay(1800).animate({opacity:0},500,'swing',function(){player.playVideo();});
$("#tubular-container").delay(1800).css({opacity:1});
}
}


$(function(){
checkUA();
$(window).load(function(){
hideLoadingMark();inScroll();informationSet();menuControl();setStanderdHeight();setscrollFunction();checkLocation();
if(judgeAnimation){visualMovieAnimation();}

backToBase('#characters_layerTop');
backToBase('#stages_layerTop');

});


});
