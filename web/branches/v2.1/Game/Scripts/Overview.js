function LoadOverview(sector) {
    divUnits=document.getElementById("u_"+sector);
    divBuildings=document.getElementById("b_"+sector);
    if (divUnits.innerHTML.length==0 && divBuildings.innerHTML.length==0) {
        divUnits.innerHTML="loading ...";
        divBuildings.innerHTML="loading ...";
        RS.Execute("Details.aspx", "GetUnits", divUnits.id, sector, "", "1", WriteUnits);
        RS.Execute("Details.aspx", "GetBuildings", divBuildings.id, sector, "", "1", WriteBuildings);        
    }
    else {
        divUnits.innerHTML="";
        divBuildings.innerHTML="";
    }
}