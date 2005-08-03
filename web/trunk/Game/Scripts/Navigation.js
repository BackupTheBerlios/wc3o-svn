function RefreshNavigation() {
    LoadNavigation();
    setTimeout("RefreshNavigation()", 60000);
}


function LoadNavigation() {
    document.getElementById("gold").innerHTML = "-";
    document.getElementById("lumber").innerHTML = "-";
    document.getElementById("upkeep").innerHTML = "-";
    document.getElementById("food").innerHTML = "-";
    document.getElementById("tick").innerHTML = "-";
    document.getElementById("message").innerHTML = "";
    document.getElementById("undersiege").innerHTML = ""; 
    RS.Execute("Details.aspx", "GetNavigation", WriteNavigation);
}


function WriteNavigation(result) {
    navigations=result.split("@");
    document.getElementById("gold").innerHTML = navigations[0];
    document.getElementById("lumber").innerHTML = navigations[1];
    document.getElementById("upkeep").innerHTML = navigations[2];
    document.getElementById("food").innerHTML = navigations[3];
    document.getElementById("tick").innerHTML = navigations[4];
    document.getElementById("message").innerHTML = navigations[5];
    document.getElementById("undersiege").innerHTML = navigations[6];        
}
