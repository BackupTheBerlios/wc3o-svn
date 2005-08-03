function LoadUnits(sector,player) {
    divUnits=document.getElementById("u_"+player);
    if (divUnits.innerHTML.length==0) {
        divUnits.innerHTML="loading ...";
        RS.Execute("Details.aspx", "GetUnits", divUnits.id, sector, player, "0", WriteUnits);   
    }
    else
        divUnits.innerHTML="";
}


function LoadTraining(sector) {
    divTraining=document.getElementById("training");
    if (divTraining.innerHTML.length==0) {
        divTraining.innerHTML="loading ...";
        RS.Execute("Details.aspx", "GetTraining", divTraining.id, sector, WriteTraining);      
    }
    else
        divTraining.innerHTML="";
}


function WriteUnits(result) {
    units=result.split("@");
    
    div=document.getElementById(units[0]);
    sector=units[1];
    isOverview=units[2];
    unitOwner=units[3];
    isUnitOwnerAlly=units[4];
    sectorOwner=units[5];
    isSectorOwnerAlly=units[6]; 
    canAttack=units[7];  
    gfx=units[8];
        
    s="";
    if (units.length>10) {    
        if (unitOwner.length<=0) {
            s="<a href=\"javascript:Popup('Command.aspx?Action=DestroyUnit&Unit=-1&Sector="+sector+"')\"><img src='"+gfx+"/Game/DestroyAll.gif' title='Destroy All units' /></a><a href=\"javascript:Popup('Command.aspx?Action=DestroyUnit&Unit=-2&Sector="+sector+"')\"><img src='"+gfx+"/Game/StopAll.gif' title='Stop All Trainings' /></a><a href=\"javascript:Popup('Command.aspx?Action=Move&Unit=-1&Sector="+sector+"')\"><img src='"+gfx+"/Game/MoveAll.gif' title='Move All' /></a><a href=\"javascript:Popup('Command.aspx?Action=Return&Unit=-1&Sector="+sector+"')\"><img src='"+gfx+"/Game/ReturnAll.gif' title='Return All' /></a><a href=\"javascript:Popup('Command.aspx?Action=MergeUnits&Unit=-1&Sector="+sector+"')\"><img src='"+gfx+"/Game/MergeAll.gif' title='Merge All Units' /></a>";
            if (isOverview=="1")
                s+="<a href='Sector.aspx?Sector="+sector+"&Refresh=Units'><img src='"+gfx+"/Game/ManageSector.gif' title='Manage Sector' /></a><a href='Sector.aspx?Sector="+sector+"&Refresh=Training'><img src='"+gfx+"/Game/TrainNewUnits.gif' title='Train New Units' /></a>";
        }
        else if (canAttack=="1" || canAttack=="2")
            s="<a href=\"Battle.aspx?Enemy="+unitOwner+"&Sector="+sector+"\"><img src='"+gfx+"/Game/Attack.gif' title='Attack' /></a>";
        if (canAttack=="2")
           s+="<a href=\"Battle.aspx?Enemy="+unitOwner+"&Sector="+sector+"&Type=Allied\"><img src='"+gfx+"/Game/AlliedAttack.gif' title='Allied Attack' /></a>";

    
    
        s+="<br /><table border='0' cellpadding='0' cellspacing='0' style='width:100%;'>";

        for (i=9;i<units.length-1;i++) {
            unitInfos=units[i].split("$");
            name=unitInfos[0];
            type=unitInfos[1];
            number=unitInfos[2];
            image=unitInfos[3];
            time=unitInfos[4];
            action=unitInfos[5];
            source=unitInfos[6];            
            sourceSector=unitInfos[7];
            hash=unitInfos[8];
            damage=unitInfos[9];
            forGold=unitInfos[10];
            forLumber=unitInfos[11];
            morphName=unitInfos[12];       
            
            s+="<tr><td style='width:200px;'><a href='../Portal/Help/Units.aspx?Unit="+type+"'><img src='"+gfx+image+"' /></a></td><td style='text-align:left;'>";
            
            s+="<b>"+number+" "+name+"</b>";
            if (damage!="0")
                s+="<br />"+damage+" % damaged";
            
            if (action=="1")
                s+=" - <i>(Training ("+time+")</i>";
            else if (action=="2")
                s+=" - <i>Mining gold</i>";
            else if (action=="3")
                s+=" - <i>Chopping lumber</i>";
            else if (action=="4" && sector==source)
                s+=" - <i>Returning ("+time+")";
            else if (action=="4" && isUnitOwnerAlly=="1")
                s+=" - <i>Arriving from "+sourceSector+" ("+time+")";
            else if (action=="4" && isUnitOwnerAlly=="0")
                s+=" - <i>Arriving ("+time+")";
            else if (action=="5")
                s+=" - <i>Resting ("+time+")</i>";    
                
            if (unitOwner.length==0) {
                if (action=="1")
                    s+="<br /><a href=\"javascript:Popup('Command.aspx?Action=DestroyUnit&Unit=" + hash + "')\"><img src='"+gfx+"/Game/Stop.gif' title='Stop Training' /></a>";
                else if (action=="2" || action=="3")
                    s+="<br /><a href=\"javascript:Popup('Command.aspx?Action=DestroyUnit&Unit=" + hash + "')\"><img src='"+gfx+"/Game/Destroy.gif' title='Destroy' /></a><a href=\"javascript:Popup('Command.aspx?Action=StopWorking&Unit=" + hash + "')\"><img src='"+gfx+"/Game/StopWorking.gif' title='Stop Working' /></a>";
                else if (action=="4") {
                    s+="<br /><a href=\"javascript:Popup('Command.aspx?Action=DestroyUnit&Unit=" + hash + "')\"><img src='"+gfx+"/Game/Destroy.gif' title='Destroy' /></a>";
                    if (source!=sector)
                        s+="<a href=\"javascript:Popup('Command.aspx?Action=Return&Unit=" + hash + "')\"><img src='"+gfx+"/Game/Return.gif' title='Return' /></a>";
                }
                else if (action=="5")
                    s+="<br /><a href=\"javascript:Popup('Command.aspx?Action=DestroyUnit&Unit=" + hash + "')\"><img src='"+gfx+"/Game/Destroy.gif' title='Destroy' /></a>";
                else {
                    s+="<br /><a href=\"javascript:Popup('Command.aspx?Action=DestroyUnit&Unit=" + hash + "')\"><img src='"+gfx+"/Game/Destroy.gif' title='Destroy' /></a><a href=\"javascript:Popup('Command.aspx?Action=Move&Unit=" + hash + "')\"><img src='"+gfx+"/Game/Move.gif' title='Move' /></a>";
                    if (isSectorOwnerAlly=="1") {
                        if (forGold=="1")
                            s+="<a href=\"javascript:Popup('Command.aspx?Action=WorkForGold&Unit=" + hash + "')\"><img src='"+gfx+"/Game/WorkForGold.gif' title='Work For Gold' /></a>";
                        if (forLumber=="1")
                            s+="<a href=\"javascript:Popup('Command.aspx?Action=WorkForLumber&Unit=" + hash + "')\"><img src='"+gfx+"/Game/WorkForLumber.gif' title='Work For Lumber' /></a>";
                    }
                    s+="<a href=\"javascript:Popup('Command.aspx?Action=MergeUnits&Unit=" + hash + "')\"><img src='"+gfx+"/Game/Merge.gif' title='Merge' /></a>";
                    if (number!="1")
                        s+="<a href=\"javascript:Popup('Command.aspx?Action=SplitUnits&Unit=" + hash + "')\"><img src='"+gfx+"/Game/Split.gif' title='Split' /></a>";
                    if (morphName.length>0)
                        s+="<br /><a href=\"javascript:Popup('Command.aspx?Action=MorphUnits&Unit=" + hash + "')\">Morph to "+morphName+"</a>"; 
                }
            }  
            
            s+="</td></tr>";
        }
        s+="</table><br ><br />";
    }
    else {
        if (unitOwner=="" || isOverview=="1")
            s+="<i>You have no units on this sector.</i>";        
        else
            s+="<i>You can't spot any units.</i>";
    }
    
    div.innerHTML=s;
}