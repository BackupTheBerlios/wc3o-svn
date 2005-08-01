function LoadConstructing(sector) {
    divConstructing=document.getElementById("constructing");
    if (divConstructing.innerHTML.length==0) {
        divConstructing.innerHTML="loading ...";
        RS.Execute("Details.aspx", "GetConstructing", divConstructing.id, sector, WriteConstructing);      
    }
    else
        divConstructing.innerHTML="";
}


function WriteConstructing(result) {
    constructings=result.split("@");
    
    div=document.getElementById(constructings[0]);
    s="<table border='0' cellpadding='0' cellspacing='0' style='width:100%;'>";
    sector=constructings[1];
    
    for (i=2;i<constructings.length-1;i++) {
        constructingInfos=constructings[i].split("$");
        name=constructingInfos[0];
        image=constructingInfos[1];  
        finishedBuildings=constructingInfos[2];
        constructingBuildings=constructingInfos[3];    
        goldcost=constructingInfos[4];
        goldImage=constructingInfos[5];
        lumbercost=constructingInfos[6];
        lumberImage=constructingInfos[7];
        id=constructingInfos[8];
        
        s+="<tr><td style='width:200px;'><a href='../Portal/Help/Buildings.aspx?Building="+id+"'><img src='"+image+"' /></a></td><td style='text-align:left;'>";       
        s += "<b>"+name + ":</b> " + goldcost + " <img src='" + goldImage + "' /> " + lumbercost + " <img src='" + lumberImage + "' /><br />Construct <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Construct&Building=" + id + "&Number=1')\">1</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Construct&Building=" + id + "&Number=2')\">2</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Construct&Building=" + id + "&Number=3')\">3</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Construct&Building=" + id + "&Number=4')\">4</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Construct&Building=" + id + "&Number=5')\">5</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Construct&Building=" + id + "&Number=10')\">10</a><br />" + finishedBuildings + " finished / " + constructingBuildings + " constructing on this sector</td></tr>";
    }
    s+="</table>";
      
    if (constructings.length<=3)
        s+="<i>You cannot construct any buildings at the moment.</i>";
    
    div.innerHTML=s;
}