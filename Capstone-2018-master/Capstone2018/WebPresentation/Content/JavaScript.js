

$(document).ready(function () {
    console.log("ready");
});

function openNavMenu() {
    document.getElementById("mySidenavmenu").style.width = "260px";
}

function closeNav() {
    document.getElementById("mySidenavmenu").style.width = "0";
}

$("#accordion").accordion();

$('.homeCarousel').carousel();