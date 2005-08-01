function LoadSector(sector,player) {
    divUnits=document.getElementById("u_"+player);
    divBuildings=document.getElementById("b_"+player);
    if (divUnits.innerHTML.length==0 && divBuildings.innerHTML.length==0) {
        divUnits.innerHTML="loading ...";
        divBuildings.innerHTML="loading ...";
        RS.Execute("Details.aspx", "GetUnits", divUnits.id, sector, player, "0", WriteUnits);
        RS.Execute("Details.aspx", "GetBuildings", divBuildings.id, sector, player,"0", WriteBuildings);        
    }
    else {
        divUnits.innerHTML="";
        divBuildings.innerHTML="";
    }
}