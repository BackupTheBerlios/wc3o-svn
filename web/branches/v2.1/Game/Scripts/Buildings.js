function LoadBuildings(sector,player) {
    divBuildings=document.getElementById("b_"+player);
    if (divBuildings.innerHTML.length==0) {
        divBuildings.innerHTML="loading ...";
        RS.Execute("Details.aspx", "GetBuildings", divBuildings.id, sector, player, "0", WriteBuildings);        
    }
    else
        divBuildings.innerHTML="";
}


function WriteBuildings(result) {
    buildings=result.split("@");
    
    div=document.getElementById(buildings[0]);
    sector=buildings[1];
    isOverview=buildings[2];
    sectorOwner=buildings[3];
    isSectorOwnerAlly=buildings[4];
    canAttack=buildings[5];
    gfx=buildings[6];
    
    s="";
    if (buildings.length>8) {
        if (sectorOwner.length<=0) {
            s="<a href=\"javascript:Popup('Command.aspx?Action=DestroyBuilding&Building=-1&Sector="+sector+"')\"><img src='"+gfx+"/Game/DestroyAll.gif' title='Destroy All Buildings' /></a><a href=\"javascript:Popup('Command.aspx?Action=DestroyBuilding&Building=-2&Sector="+sector+"')\"><img src='"+gfx+"/Game/StopAll.gif' title='Stop All Constructions' /></a><a href=\"javascript:Popup('Command.aspx?Action=MergeBuildings&Building=-1&Sector="+sector+"')\"><img src='"+gfx+"/Game/MergeAll.gif' title='Merge All Buildings' /></a>";
            if (isOverview=="1")
                s+="<a href='Sector.aspx?Sector="+sector+"&Refresh=Buildings'><img src='"+gfx+"/Game/ManageSector.gif' title='Manage Sector' /></a><a href='Sector.aspx?Sector="+sector+"&Refresh=Constructing'><img src='"+gfx+"/Game/ConstructNewBuildings.gif' title='Construct New Buildings' /></a>";
        }
        else if (canAttack=="1" || canAttack=="2")
            s="<a href=\"Battle.aspx?Enemy="+sectorOwner+"&Sector="+sector+"\"><img src='"+gfx+"/Game/Attack.gif' title='Attack' /></a>";
        if (canAttack=="2")
           s+="<a href=\"Battle.aspx?Enemy="+sectorOwner+"&Sector="+sector+"&Type=Allied\"><img src='"+gfx+"/Game/AlliedAttack.gif' title='Allied Attack' /></a>";
    
        s+="<br /><table border='0' cellpadding='0' cellspacing='0' style='width:100%;'>";    
        
        for (i=7;i<buildings.length-1;i++) {
            buildingInfos=buildings[i].split("$");
            name=buildingInfos[0];
            type=buildingInfos[1];
            number=buildingInfos[2];
            image=buildingInfos[3];
            time=buildingInfos[4];
            action=buildingInfos[5];
            hash=buildingInfos[6];
            damage=buildingInfos[7];
            
            s+="<tr><td style='width:200px;'><a href='../Portal/Help/Buildings.aspx?Building="+type+"'><img src='"+gfx+image+"' /></a></td><td style='text-align:left;'>";
            
            s+="<b>"+number+" "+name+"</b>";
            if (damage!="0")
                s+="<br />"+damage+" % damaged";
            
            if (action=="1")
                s+=" - <i>In construction ("+time+")</i>";
            else if (action=="2")
                s+=" - <i>Upgrading ("+time+")</i>";
                
            if (sectorOwner.length==0) {
                if (action=="1")
                    s+="<br /><a href=\"javascript:Popup('Command.aspx?Action=DestroyBuilding&Building=" + hash + "')\"><img src='"+gfx+"/Game/Stop.gif' title='Stop Construction' /></a>";
                else if (action=="2") 
                    s+="<br /><a href=\"javascript:Popup('Command.aspx?Action=DestroyBuilding&Building=" + hash + "')\"><img src='"+gfx+"/Game/Stop.gif' title='Stop Upgrading' /></a>";        
                else {
                    s+="<br /><a href=\"javascript:Popup('Command.aspx?Action=DestroyBuilding&Building=" + hash + "')\"><img src='"+gfx+"/Game/Destroy.gif' title='Destroy' /></a>";
                    s+="<a href=\"javascript:Popup('Command.aspx?Action=MergeBuildings&Building=" + hash + "')\"><img src='"+gfx+"/Game/Merge.gif' title='Merge' /></a>";
                    if (number!="1")
                        s+="<a href=\"javascript:Popup('Command.aspx?Action=SplitBuildings&Building=" + hash + "')\"><img src='"+gfx+"/Game/Split.gif' title='Split' /></a>";
                    
                    if (buildingInfos.length>7) { //upgrade buildings
                        s+="<br />";
                        for (j=8;j<buildingInfos.length;j+=2) {
                            s+="<a href=\"javascript:Popup('Command.aspx?Action=UpgradeBuilding&Building=" + hash + "&To=" + buildingInfos[j+1] + "')\">Upgrade to " + buildingInfos[j] + "</a>";
                            if (j<buildingInfos.length-2)
                                s+=" | ";
                        }
                    }
                }
            }
            
            s+="</td></tr>";
        }
        s+="</table><br /><br />";
    }
    else {
        if (sectorOwner=="" || isOverview=="1")
            s+="<i>You have no buildings on this sector.</i>";        
        else
            s+="<i>You can't spot any buildings.</i>";
    }
    
    div.innerHTML=s;
}