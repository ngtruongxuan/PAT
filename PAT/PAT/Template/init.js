// JavaScript Document

var ua = (navigator.userAgent.indexOf("MSIE") > -1);
var vr = Math.abs(navigator.userAgent.substr(navigator.userAgent.indexOf("MSIE ") + 5,1));
var ie = (navigator.userAgent.toLowerCase().indexOf("msie") > -1);
var ie8 = (ua && vr <= 8);
var ie7 = (ua && vr <= 7);


var android = (navigator.userAgent.toLowerCase().indexOf("android") > -1);
var iphone = (navigator.userAgent.toLowerCase().indexOf("iphone") > -1 || navigator.userAgent.toLowerCase().indexOf("ipod") > -1);
var ipad = (navigator.userAgent.toLowerCase().indexOf("ipad") > -1);

