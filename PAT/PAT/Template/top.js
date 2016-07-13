$(function(){

	var $container = $('.container','#flick');
	var $item = $('.item','#flick');
	
	$(window).bind(('orientationchange resize load'), function(){
		//var windowWidth = (!(window.innerWidth)) ? document.documentElement.clientWidth : window.innerWidth;
		var windowWidth = $("#flickObj").width();
		
			$item.css({width : windowWidth});
			$container.css({width : windowWidth});
	});
	
	
	//$("#flick").flick({});
	
	menuSet();
	footerSet();
	informationSet();
	//slideImage('f_001',1);
	
	
	if(judgeAnimation){
		
		//setMouseEvent()
		//setExpWindowSize();
		
		Shadowbox.init({  
        language:   "ja",  
        players: ['img', 'html', 'iframe', 'qt', 'wmp', 'swf', 'flv']
		}); 
		
		$(function(){
		$('#example1').fbWall({ id:'deadoralivegame',accessToken:'AAAC7gAoWDQ0BAF1zPw7WzQAI6gC5Y6bkCZA7W4ZBJRyKTweZB40uNOZBEZCyEcZCOyC5TIkssjGH0IEX0PwLyGhQjdPo9pStoZD',showGuestEntries:false });
		});
		
		for(var i = 0 ; i < 6 ;i++){$('#information li').eq(i).addClass('num_'+i);}
		for(var i = 0 ; i < 9 ;i++){$('#movies li').eq(i).addClass('num_'+i);}
		
		setMoveList('#menu',{'opacity':1,top: '0px'},{'opacity':0, top :'-40px'}, '200' , '100' , 'false');
		setMoveList('#visual',{'opacity':1},{'opacity':0}, '1800' , '100' , 'false');
		setMoveList('h1',{'opacity':1},{'opacity':0}, '800' , '500' , 'false');
		
		setMoveList('#information li.num_0',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '800' , 'false');
		setMoveList('#information li.num_1',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '900' , 'false');
		setMoveList('#information li.num_2',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '1000' , 'false');
		setMoveList('#information li.num_3',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '1100' , 'false');
		setMoveList('#information li.num_4',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '1200' , 'false');
		setMoveList('#information li.num_5',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '1300' , 'false');
		setMoveList('#information .btn_more',{'opacity':1},{'opacity':0}, '200' , '1200' , 'false');
		setMoveList('#information h3',{'opacity':1,left: '0px'},{'opacity':0, left :'40px'}, '200' , '600' , 'false');
		setMoveList('#spec',{'opacity':1},{'opacity':0}, '200' , '500' , 'false');
		
		setMoveList('#features h2 img',{'opacity':1,left: '0px'},{'opacity':0, left :'-100px'}, '200' , '10' , 'true');
		setMoveList('#characters h2 img',{'opacity':1,left: '0px'},{'opacity':0, left :'-100px'}, '200' , '10' , 'true');
		setMoveList('#movies h2 img',{'opacity':1,left: '0px'},{'opacity':0, left :'-100px'}, '200' , '10' , 'true');
		setMoveList('#stages h2 img',{'opacity':1,left: '0px'},{'opacity':0, left :'-100px'}, '200' , '10' , 'true');
		setMoveList('#contact h2 img',{'opacity':1,left: '0px'},{'opacity':0, left :'-100px'}, '200' , '10' , 'true');
		
		setMoveList('#slider_features',{'opacity':1},{'opacity':0}, '200' , '300' , 'false');
		setMoveList('#exp_features',{'opacity':1},{'opacity':0}, '200' , '400' , 'false');
		
		setMoveList('#characters_base a.t01',{'opacity':1},{'opacity':0}, '200' , '100' , 'false');
		setMoveList('#characters_base a.t02',{'opacity':1},{'opacity':0}, '200' , '150' , 'false');
		setMoveList('#characters_base a.t03',{'opacity':1},{'opacity':0}, '200' , '200' , 'false');
		setMoveList('#characters_base a.t04',{'opacity':1},{'opacity':0}, '200' , '250' , 'false');
		setMoveList('#characters_base a.t05',{'opacity':1},{'opacity':0}, '200' , '300' , 'false');
		setMoveList('#characters_base a.t06',{'opacity':1},{'opacity':0}, '200' , '350' , 'false');
		setMoveList('#characters_base a.t07',{'opacity':1},{'opacity':0}, '200' , '400' , 'false');
		
		setMoveList('#characters_base a.t08',{'opacity':1},{'opacity':0}, '200' , '100' , 'false');
		setMoveList('#characters_base a.t09',{'opacity':1},{'opacity':0}, '200' , '150' , 'false');
		setMoveList('#characters_base a.t10',{'opacity':1},{'opacity':0}, '200' , '200' , 'false');
		setMoveList('#characters_base a.t11',{'opacity':1},{'opacity':0}, '200' , '250' , 'false');
		setMoveList('#characters_base a.t12',{'opacity':1},{'opacity':0}, '200' , '300' , 'false');
		setMoveList('#characters_base a.t13',{'opacity':1},{'opacity':0}, '200' , '350' , 'false');
		setMoveList('#characters_base a.t14',{'opacity':1},{'opacity':0}, '200' , '400' , 'false');
		
		setMoveList('#characters_base a.t15',{'opacity':1},{'opacity':0}, '200' , '100' , 'false');
		setMoveList('#characters_base a.t16',{'opacity':1},{'opacity':0}, '200' , '150' , 'false');
		setMoveList('#characters_base a.t17',{'opacity':1},{'opacity':0}, '200' , '200' , 'false');
		setMoveList('#characters_base a.t18',{'opacity':1},{'opacity':0}, '200' , '250' , 'false');
		setMoveList('#characters_base a.t19',{'opacity':1},{'opacity':0}, '200' , '300' , 'false');
		setMoveList('#characters_base a.t20',{'opacity':1},{'opacity':0}, '200' , '350' , 'false');
		setMoveList('#characters_base a.t21',{'opacity':1},{'opacity':0}, '200' , '400' , 'false');
		
		setMoveList('#characters_base a.t22',{'opacity':1},{'opacity':0}, '200' , '100' , 'false');
		setMoveList('#characters_base a.t23',{'opacity':1},{'opacity':0}, '200' , '150' , 'false');
		setMoveList('#characters_base a.t24',{'opacity':1},{'opacity':0}, '200' , '200' , 'false');
		setMoveList('#characters_base a.t25',{'opacity':1},{'opacity':0}, '200' , '250' , 'false');
		setMoveList('#characters_base a.t26',{'opacity':1},{'opacity':0}, '200' , '300' , 'false');
		setMoveList('#characters_base a.t27',{'opacity':1},{'opacity':0}, '200' , '350' , 'false');
		setMoveList('#characters_base a.t28',{'opacity':1},{'opacity':0}, '200' , '400' , 'false');
		
		setMoveList('#characters_base a.t29',{'opacity':1},{'opacity':0}, '200' , '100' , 'false');
		setMoveList('#characters_base a.t30',{'opacity':1},{'opacity':0}, '200' , '150' , 'false');
		setMoveList('#characters_base a.t31',{'opacity':1},{'opacity':0}, '200' , '200' , 'false');
		setMoveList('#characters_base a.t32',{'opacity':1},{'opacity':0}, '200' , '250' , 'false');
		setMoveList('#characters_base a.t33',{'opacity':1},{'opacity':0}, '200' , '300' , 'false');
		setMoveList('#characters_base a.t34',{'opacity':1},{'opacity':0}, '200' , '350' , 'false');
		
		setMoveList('#characters_base .bnr',{'opacity':1},{'opacity':0}, '200' , '100' , 'false');
		
		setMoveList('#stages_base .t01',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '300' , 'false');
		setMoveList('#stages_base .t02',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '380' , 'false');
		
		setMoveList('#movies li.num_0',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '100' , 'false');
		setMoveList('#movies li.num_1',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '150' , 'false');
		setMoveList('#movies li.num_2',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '200' , 'false');
		setMoveList('#movies li.num_3',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '250' , 'false');
		setMoveList('#movies li.num_4',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '300' , 'false');
		setMoveList('#movies li.num_5',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '350' , 'false');
		setMoveList('#movies li.num_6',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '400' , 'false');
		setMoveList('#movies li.num_7',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '450' , 'false');
		setMoveList('#movies li.num_8',{'opacity':1,top: '0px'},{'opacity':0, top :'30px'}, '200' , '500' , 'false');
		
		setMoveList('#facebook h4',{'opacity':1,left: '0px'},{'opacity':0, left :'40px'}, '200' , '600' , 'true');
		setMoveList('#twitter h4',{'opacity':1,left: '0px'},{'opacity':0, left :'40px'}, '200' , '300' , 'true');
		
		setStyle();
		
		}
	
});
