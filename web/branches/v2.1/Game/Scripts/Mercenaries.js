function RefreshMercenaries(sector) {
    window.opener.document.getElementById("divMercenaries").innerHTML="";
    window.opener.LoadMercenaries(sector);
}


function LoadMercenaries(sector) {
    divMercenaries=document.getElementById("divMercenaries");
    if (divMercenaries.innerHTML.length==0) {
        divMercenaries.innerHTML="loading mercenaries ...";
        RS.Execute("Details/Mercenaries.aspx", "GetMercenaries", sector, WriteMercenaries);      
    }
    else
        divMercenaries.innerHTML="";
}


function WriteMercenaries(result) {
    array=result.split("@");
    
    divMercenaries=document.getElementById("divMercenaries");
    gfx=array[0];
    sector=array[1];
    
    html="<table border='0' cellpadding='0' cellspacing='0' style='width:100%;'>";    
    for (i=2;i<array.length-1;i++) {
        mercenaryArray=array[i].split("$");
        number=mercenaryArray[0];
        time=mercenaryArray[1];  
        name=mercenaryArray[2];
        image=mercenaryArray[3];    
        goldCosts=mercenaryArray[4];
        lumberCosts=mercenaryArray[5];
        foodCosts=mercenaryArray[6];  
        id=mercenaryArray[7];   
        
        index=i-2; //the index of the MercenaryInfo       
       
        html += "<tr><td style='width:200px;'><a href='../Portal/Help/Units.aspx?Unit="+id+"'><img src='"+gfx+image+"' /></a></td><td style='text-align:left;'>";       
        html += "<b>"+name + ":</b>&nbsp;&nbsp;&nbsp;" + goldCosts + " <img src='" + gfx + "/Game/Gold.gif' />&nbsp;&nbsp;&nbsp;" + lumberCosts + " <img src='" + gfx + "/Game/Lumber.gif' />&nbsp;&nbsp;&nbsp;" + foodCosts+" <img src='" + gfx + "/Game/Food.gif' /><br />";
        
        if (number>0) {
            html+="Recruit ";
            for (j=1;j<=number;j++) {
                if (j!=1)
                    html+=" | "
                html+="<a href=\"javascript:Popup('Commands/Mercenaries.aspx?Sector=" + sector + "&Action=Recruit&Mercenary=" + index + "&Number="+j+"')\">"+j+"</a>"
            }
        }
        else
            html += "<i>Available in "+time+".</i>"
    }
    s+="</table>";
    
    divMercenaries.innerHTML=html;
}