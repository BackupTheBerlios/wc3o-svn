function LoadTraining(sector) {
    divTraining=document.getElementById("training");
    if (divTraining.innerHTML.length==0) {
        divTraining.innerHTML="loading ...";
        RS.Execute("Details.aspx", "GetTraining", divTraining.id, sector, WriteTraining);      
    }
    else
        divTraining.innerHTML="";
}


function WriteTraining(result) {
    trainings=result.split("@");
    
    div=document.getElementById(trainings[0]);
    s="<table border='0' cellpadding='0' cellspacing='0' style='width:100%;'>";
    sector=trainings[1];
    gfx=trainings[2];
    
    for (i=3;i<trainings.length-1;i++) {
        trainingInfos=trainings[i].split("$");
        name=trainingInfos[0];
        image=trainingInfos[1];        
        goldcost=trainingInfos[2];
        lumbercost=trainingInfos[3];
        foodcost=trainingInfos[4];
        id=trainingInfos[5];
        
        s+="<tr><td style='width:200px;'><a href='../Portal/Help/Units.aspx?Unit="+id+"'><img src='"+gfx+image+"' /></a></td><td style='text-align:left;'>";
        s += "<b>" + name + ":</b> " + goldcost + " <img src='" + gfx + "/Game/Gold.gif' />&nbsp;&nbsp;&nbsp;&nbsp;" + lumbercost + " <img src='" + gfx + "/Game/Lumber.gif' />&nbsp;&nbsp;&nbsp;&nbsp;" + foodcost + " <img src='" + gfx + "/Game/Food.gif' /><br />Train <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Train&Unit=" + id + "&Number=1')\">1</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Train&Unit=" + id + "&Number=2')\">2</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Train&Unit=" + id + "&Number=3')\">3</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Train&Unit=" + id + "&Number=4')\">4</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Train&Unit=" + id + "&Number=5')\">5</a> | <a href=\"javascript:Popup('Command.aspx?Sector=" + sector + "&Action=Train&Unit=" + id + "&Number=10')\">10</a></td></tr>";
    }
    s+="</table>";
      
    if (trainings.length<=4)
        s+="<i>You cannot train any units at the moment.</i>";
    
    div.innerHTML=s;
}