//agecheck

function agecheck()
{
   local_t = new Date();
   local_m = local_t.getMonth()+1;
   local_d = local_t.getDate();
   local_y = local_t.getYear();

   if(local_y < 1900){
     local_y += 1900;
	}

   user_m = Number(document.checkForm.month.value);
   user_d = Number(document.checkForm.day.value);
   user_y = Number(document.checkForm.year.value);

  if(isNaN(user_m)||isNaN(user_d)||isNaN(user_y)){
    location.reload();
    return;
    }

  if((user_d==0)||(user_d>31)||(user_m==0)||(user_m>12)){
    location.reload();
    return;
      }

   age = local_y - user_y;

    if(local_m < user_m){
      age--;
    }
    else{
      if((local_m <= user_m) && (local_d < user_d)){
         age--;
         }
    }

//  alert("your age is "+age+" years old");

//    ninja=getCookie("NINJA");

  if(age>17){
    location.href="top.html";
//	alert('In');
    ninja=18;
    }
   else{
    location.replace("finish.html");
//	alert('Exit');
    ninja=17;
   }

    setCookie("NINJA",ninja,local_y,local_m,local_d);

}


function getCookie(key){
	tmp=document.cookie+";";
	tmp1=tmp.indexOf(key,0);
	if(tmp1 != -1){
		tmp=tmp.substring(tmp1,tmp.length);
		start=tmp.indexOf("=",0)+1;
		end=tmp.indexOf(";",start);
		return(unescape(tmp.substring(start,end)));
	}
	return("");
}

function setCookie(key,val,yr,mt,dt){
    var exp_day = dt + "-" + mt + "-" + yr + " 23:59:59;";
	tmp=key+"="+escape(val) + ";expires=" + exp_day;
	document.cookie=tmp;
}


//agegate version
function checkGATE()
{
	ninja=getCookie("NINJA");

	if(ninja==""){
		return;
		
	}else if(ninja == 17){
		location.href="../finish.html";
		return;
	}else if(ninja == 18){
		location.href="top.html";
		return;
	}
}

//agegate version
function checkInner()
{
	ninja=getCookie("NINJA");

	if(ninja==""){
		location.href="index.html";
		return;
		
	}else if(ninja == 17){
		location.href="../finish.html";
		return;
	}else if(ninja == 18){
		return;
	}
}

//landing version
function checkOK()
{
  ninja=getCookie("NINJA");

	if(ninja==""){
    location.href="index.html";
    return;
	}
	if(ninja == 17){
    location.href="finish.html";
	return;
	}
	if(ninja == 18){
    return;
	}
}

//sorry version
function checkNG()
{
  ninja=getCookie("NINJA");

	if(ninja==""){
	location.href="index.html";
    return;
	}
	if(ninja == 17){
	return;
	}
	if(ninja == 18){
	location.href="finish.html";
    return;
	}
}

function init(){

	var m = document.checkForm.month;
	var d = document.checkForm.day;
	var y = document.checkForm.year;
	
	m.onfocus = d.onfocus = y.onfocus = function(){
		if( this.value == this.defaultValue )	this.value = "";
	}
	
	m.onblur = d.onblur = y.onblur = function(){
		if( this.value == "" )	this.value = this.defaultValue;
	}
	checkGATE();
}

