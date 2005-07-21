function Refresh(i,sector) {
    if (i==1) {
        document.getElementById("u_").innerHTML="";
        document.getElementById("b_").innerHTML="";  
        LoadSector(sector,"");  
    }
    else if (i==2) {
        document.getElementById("constructing").innerHTML="";
        LoadConstructing(sector);
    }
    else if (i==3) {
        document.getElementById("training").innerHTML="";
        LoadTraining(sector);
    }
    else if (i==4) {
        document.getElementById("u_"+sector).innerHTML="";
        document.getElementById("b_"+sector).innerHTML="";        
        LoadOverview(sector);
    }
    else if (i==5) {
        LoadNavigation();
    }
}

function RefreshParent(i,sector) {
    if (i==1) {
        if (!window.opener.document.getElementById("u_"))
            RefreshParent(4,sector);  
        else {
            window.opener.document.getElementById("u_").innerHTML="";
            window.opener.document.getElementById("b_").innerHTML="";  
            window.opener.LoadSector(sector,"");
        }  
    }
    else if (i==2) {
        window.opener.document.getElementById("constructing").innerHTML="";
        window.opener.LoadConstructing(sector);
    }
    else if (i==3) {
        window.opener.document.getElementById("training").innerHTML="";
        window.opener.LoadTraining(sector);
    }
    else if (i==4) {
        window.opener.document.getElementById("u_"+sector).innerHTML="";
        window.opener.document.getElementById("b_"+sector).innerHTML="";        
        window.opener.LoadOverview(sector);
    }
    else if (i==5) {
        window.opener.LoadNavigation();
    }
}

function RefreshNavigationTimer() {
    Refresh(5,"");
    setTimeout("RefreshNavigationTimer()",30000);
}

function LoadNavigation(doc) {
    document.getElementById("gold").innerHTML="-";
    document.getElementById("lumber").innerHTML="-";
    document.getElementById("upkeep").innerHTML="-";
    document.getElementById("food").innerHTML="-";
    document.getElementById("tick").innerHTML="-";
    document.getElementById("message").innerHTML="";
    document.getElementById("undersiege").innerHTML=""; 
    RS.Execute("Details.aspx", "GetNavigation", WriteNavigation);
}


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


function LoadSector(sector,player) {
    divUnits=document.getElementById("u_"+player);
    divBuildings=document.getElementById("b_"+player);
    if (divUnits.innerHTML.length==0 && divBuildings.innerHTML.length==0) {
        divUnits.innerHTML="loading ...";
        divBuildings.innerHTML="loading ...";
        RS.Execute("Details.aspx", "GetUnits", divUnits.id, sector, player, "0", WriteUnits);
        RS.Execute("Details.aspx", "GetBuildings", divBuildings.id, sector, player,"", WriteBuildings);        
    }
    else {
        divUnits.innerHTML="";
        divBuildings.innerHTML="";
    }
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


function LoadConstructing(sector) {
    divConstructing=document.getElementById("constructing");
    if (divConstructing.innerHTML.length==0) {
        divConstructing.innerHTML="loading ...";
        RS.Execute("Details.aspx", "GetConstructing", divConstructing.id, sector, WriteConstructing);      
    }
    else
        divConstructing.innerHTML="";
}


function WriteNavigation(result) {
    navigations=result.split("@");
    document.getElementById("gold").innerHTML=navigations[0];
    document.getElementById("lumber").innerHTML=navigations[1];
    document.getElementById("upkeep").innerHTML=navigations[2];
    document.getElementById("food").innerHTML=navigations[3];
    document.getElementById("tick").innerHTML=navigations[4];
    document.getElementById("message").innerHTML=navigations[5];
    document.getElementById("undersiege").innerHTML=navigations[6];        
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
                s+="<a href='Sector.aspx?Sector="+sector+"&Refresh=Player'><img src='"+gfx+"/Game/ManageSector.gif' title='Manage Sector' /></a><a href='Sector.aspx?Sector="+sector+"&Refresh=Training'><img src='"+gfx+"/Game/TrainNewUnits.gif' title='Train New Units' /></a>";
        }
        else if (canAttack=="1")
            s="<a href=\"Battle.aspx?Enemy="+unitOwner+"&Sector="+sector+"\"><img src='"+gfx+"/Game/Attack.gif' title='Attack' /></a>";
    
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
    else
        s+="<i>You can't spot any units.</i>";
    
    div.innerHTML=s;
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
                s+="<a href='Sector.aspx?Sector="+sector+"&Refresh=Player'><img src='"+gfx+"/Game/ManageSector.gif' title='Manage Sector' /></a><a href='Sector.aspx?Sector="+sector+"&Refresh=Constructing'><img src='"+gfx+"/Game/ConstructNewBuildings.gif' title='Construct New Buildings' /></a>";
        }
        else if (canAttack=="1")
            s="<a href=\"Battle.aspx?Enemy="+sectorOwner+"&Sector="+sector+"\"><img src='"+gfx+"/Game/Attack.gif' title='Attack' /></a>";
    
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
    else
        s+="<i>You can't spot any buildings.</i>";
    
    div.innerHTML=s;
}


function Popup(url) {
    eval("window.open(url, '', 'toolbar=0,scrollbars=0,location=0,statusbar=1,menubar=0,resizable=1,width=600,height=300');");    
}


function I(x,y,name,owner) {
    document.getElementById("Info").innerHTML="<b>["+x+":"+y+"] "+name+"</b><br />";
    if (owner=="-")
        document.getElementById("Info").innerHTML+="<i>Neutral sector.</i>";
    else
        document.getElementById("Info").innerHTML+="Owner: <b>"+owner+"<b /><br /><br />";
}


function O() {
   document.getElementById("Info").innerHTML = document.getElementById("txtInfo").value
}


function C(x,y) {
   document.getElementById("Info").innerHTML = " ... ";
   RS.Execute("Map.aspx", "LoadInfo", x, y, ShowInfo);
}


function ShowInfo(result) {
    document.getElementById("Info").innerHTML=result;
    document.getElementById("txtInfo").value=result; 
}


var RS = new RemoteScripting();
function RemoteScripting()
{
	this.pool = new Array();
	this.poolSize = 0;
	this.maxPoolSize = 1000;
	this.usePOST = false;
	this.debug = false;

	// Sniff the browser
	if (document.layers)
		this.browser = "NS";
	else if (document.all)
	{
		var agent = navigator.userAgent.toLowerCase();
		if (agent.indexOf("opera") != -1)
			this.browser = "OPR";
		else if (agent.indexOf("konqueror") != -1)
			this.browser = "KONQ";
		else
			this.browser = "IE";
	}
	else if (document.getElementById)
		this.browser = "MOZ";
	else 
		this.browser = "OTHER";
}
RemoteScripting.prototype.Execute = function(url, method)
{
	var call = this.getAvailableCall();	
	var args = RemoteScripting.prototype.Execute.arguments;
	var len = RemoteScripting.prototype.Execute.arguments.length;

	var methodArgs = new Array();

	for (var i = 2; i < len; i++)
	{
		if (typeof(args[i]) == 'function')
		{
			call.callback = args[i++];		  
			
			if (i < len && typeof(args[i]) == 'function')
				call.callbackForError = args[i++];
			
			var ca = 0;
			for (; i < len; i++)
				call.callbackArgs[ca++] = args[i];
			break;
		}
		
		methodArgs[i - 2] = args[i];
	}
	
	call.showIfDebugging();

	if (this.usePOST && ((this.browser == 'IE') || (this.browser == 'MOZ')))
		call.POST(url, method, methodArgs);
	else 
		call.GET(url, method, methodArgs);
	
	return this.id;
}
RemoteScripting.prototype.PopupDebugInfo = function()
{
	var doc = window.open().document;
	doc.open();
	doc.write('<html><body>Pool Size: ' + this.poolSize + '<br><font face = "arial" size = "2"><b>');
	for (var i = 0; i < this.pool; i++)
	{
		var call = this.pool[i];
		doc.write('<hr>' + call.id + ' : ' + (call.busy ? 'busy' : 'available') + '<br>');
		doc.write(call.container.document.location.pathname + '<br>');
		doc.write(call.container.document.location.search + '<br>');
		doc.write('<table border = "1"><tr><td>' + call.container.document.body.innerHTML + '</td></tr></table>');
	}
	doc.write('</table></body></html>');
	doc.close();
	return false;
}
RemoteScripting.prototype.ReplaceOptions = function(element, optionsHTML)
{
	// Remove any existing options 
	while (element.options.length > 0)
        element.options[0] = null;

	// Create an array of each item for the dropdown
	var options = optionsHTML.split("</option>");
    var selectIndex = 0;
    var quote = (optionsHTML.indexOf("\"") > 0 ? "\"" : "'");

	// Fill 'er up
	for (var i = 0; i < options.length - 1; i++)
	{
	    aValueText = options[i].split(">");

	    option = new Option;
	    option.text = aValueText[aValueText.length - 1];
	    
	    // Account for the possibility of a value containing a >
	    for (var e = 1; e < aValueText.length - 1; e++)
			aValueText[0] += ">" + aValueText[e];

	    // Extract the value
	    var firstQuote = aValueText[0].indexOf(quote);
	    var lastQuote = aValueText[0].lastIndexOf(quote);
	    if (firstQuote > 0 && lastQuote > firstQuote + 1)
	        option.value = aValueText[0].substring(firstQuote + 1, lastQuote);

	    // Check if it's selected
	    if (aValueText[0].indexOf('selected') > 0)
	        selectIndex = i;

	    element.options[element.options.length] = option;
	}

	element.options[selectIndex].selected = true;
}
RemoteScripting.prototype.ReplaceOptions2 = function(optionsHTML, element)
{
	RS.ReplaceOptions(element, optionsHTML);
	window.status = "";	
}
RemoteScripting.prototype.getAvailableCall = function()
{
	for (var i = 0; i < this.poolSize; i++)
	{
		var call = this.pool['C' + (i + 1)];
		if (!call.busy)
		{
			call.busy = true;      
			return this.pool[call.id];
		}
	}
	
	// If we got here, there are no existing free calls
	if (this.poolSize <= this.maxPoolSize)
	{
		var callID = "C" + (this.poolSize + 1);
		this.pool[callID] = new RemoteScriptingCall(callID);
		this.poolSize++;
		return this.pool[callID];
	}

	alert("RemoteScripting Error: Call pool is full (no more than 1000 calls can be made simultaneously).");
	return null;
}
function RemoteScriptingCall(callID)
{
	this.id = callID;
	this.busy = true;
	this.callback = null;
	this.callbackForError = null;
	this.callbackArgs = new Array();

	switch (RS.browser)
	{
		case 'IE':
			document.body.insertAdjacentHTML("afterBegin", '<span id = "SPAN' + callID + '"></span>');
			this.span = document.all("SPAN" + callID);
			var html = '<iframe style = "width:800px" name = "' + callID + '" src = "./"></iframe>';
			this.span.innerHTML = html;
			this.span.style.display = 'none';
			this.container = window.frames[callID];
			break;
			
		case 'NS':
			this.container = new Layer(100);
			this.container.name = callID;
			this.container.visibility = 'hidden';
			this.container.clip.width = 100;
			this.container.clip.height = 100;
			break;
			
		case 'MOZ':
			this.span = document.createElement('SPAN');
			this.span.id = "SPAN" + callID;
			document.body.appendChild(this.span);
			var iframe = document.createElement('IFRAME');
			iframe.id = callID;
			iframe.name = callID;
			iframe.style.width = 800;
			iframe.style.height = 200;
			this.span.appendChild(iframe);
			this.container = iframe;
			break;
			
		case 'OPR':        
			this.span = document.createElement('SPAN');
			this.span.id = "SPAN" + callID;
			document.body.appendChild(this.span);
			var iframe = document.createElement('IFRAME');
			iframe.id = callID;
			iframe.name = callID;
			iframe.style.width = 800;
			iframe.style.height = 200;
			this.span.appendChild(iframe);
			this.container = iframe;
			break;
			
		case 'KONQ':  
		default:
			this.span = document.createElement('SPAN');
			this.span.id = "SPAN" + callID;
			document.body.appendChild(this.span);
			var iframe = document.createElement('IFRAME');
			iframe.id = callID;
			iframe.name = callID;
			iframe.style.width = 800;
			iframe.style.height = 200;
			this.span.appendChild(iframe);
			this.container = iframe;
			
			// Needs to be hidden for Konqueror, otherwise it'll appear on the page
			this.span.style.display = none;
			iframe.style.display = none;
			iframe.style.visibility = hidden;
			iframe.height = 0;
			iframe.width = 0;
	}	
}
RemoteScriptingCall.prototype.POST = function(url, method, args)
{
	var d = new Date();
	var unique = d.getTime() + '' + Math.floor(1000 * Math.random());
	var doc = (RS.browser == "IE") ? this.container.document : this.container.contentDocument;
	var paramSep = (url.lastIndexOf('?') < 0 ? '?' : '&');
	doc.open();
	doc.write('<html><body>');
	doc.write('<form name="rsForm" method="post" target=""');
	doc.write('action="' + url + paramSep + 'U=' + unique + '">');
	doc.write('<input type="hidden" name="RC" value="' + this.id + '">');
	
	// func and args are optional
	if (method != null)
	{
		doc.write('<input type = "hidden" name = "M" value = "' + method + '">');
		
		if (args != null)
		{
			if (typeof(args) == "string")
			{
				// single parameter
				doc.write('<input type = "hidden" name = "P0" '
					+ 'value = "[' + this.escapeParam(args) + ']">');
			}
			else 
			{
				// assume args is array of strings
				for (var i = 0; i < args.length; i++)
				{
					doc.write('<input type = "hidden" name = "P' + i + '" '
						+ 'value = "[' + this.escapeParam(args[i]) + ']">');
				}
			} // parm type
		} // args
	} // method
	
	doc.write('</form></body></html>');
	doc.close();
	doc.forms['rsForm'].submit();
}
RemoteScriptingCall.prototype.GET = function(url, method, args)
{
	// build URL to call
	var URL = url;
	var paramSep = (url.lastIndexOf('?') < 0 ? '?' : '&');
	
	// always send call
	URL += paramSep + "RC=" + this.id;
	
	// method and args are optional
	if (method != null)
	{
		URL += "&M=" + escape(method);
		
		if (args != null)
		{
			if (typeof(args) == "string")
			{
				// single parameter
				URL += "&P0=[" + escape(args + '') + "]";
			}
			else 
			{
				// assume args is array of strings
				for (var i = 0; i < args.length; i++)
				{
					URL += "&P" + i + "=[" + escape(args[i] + '') + "]";
				}
			} // parm type
		} // args
	} // method
	
	// unique string to defeat cache
	var d = new Date();
	URL += "&U=" + d.getTime();
	
	// make the call
	switch (RS.browser)
	{
		case 'IE':
			this.container.document.location.replace(URL);
			break;
		case 'NS':
			this.container.src = URL;
			break;
		case 'MOZ':
		case 'OPR':
		case 'KONQ':
		default:
			this.container.src = '';
			this.container.src = URL; 
			break;
	}  
}
RemoteScriptingCall.prototype.setResult = function(result)
{
	var argsCount = this.callbackArgs.length;

	if (result == true)
	{
		if (this.callback != null)
			this.callback(this.unescape(this.getPayload()), argsCount > 0 ? this.callbackArgs[0] : null, argsCount > 1 ? this.callbackArgs[1] : null, argsCount > 2 ? this.callbackArgs[2] : null);
	}
	else
	{
		if (this.callbackForError == null)
			alert(this.unescape(this.getPayload()));
		else
			this.callbackForError(this.unescape(this.getPayload()), argsCount > 0 ? this.callbackArgs[0] : null, argsCount > 1 ? this.callbackArgs[1] : null, argsCount > 2 ? this.callbackArgs[2] : null);
	}
		
	this.callback = null;
	this.callbackForError = null;
	this.callbackArgs = new Array();
	this.busy = false;	
}
RemoteScriptingCall.prototype.getPayload = function()
{
	switch (RS.browser)
	{
		case 'IE':
			return this.container.document.forms['rsForm']['rsPayload'].value;
		case 'NS':
			return this.container.document.forms['rsForm'].elements['rsPayload'].value;
		case 'MOZ':
			return window.frames[this.container.name].document.forms['rsForm']['rsPayload'].value; 
		case 'OPR':
			return window.frames[this.container.name].document.forms['rsForm']['rsPayload'].value; 
		case 'KONQ':
		default:
			return window.frames[this.container.name].document.getElementById("rsPayload").value;
	}  
}
RemoteScriptingCall.prototype.showIfDebugging = function()
{
	var vis = (RS.debug == true);
	switch (RS.browser)
	{
		case 'IE':
			document.all("SPAN" + this.id).style.display = (vis) ? '' : 'none';
			break;
		case 'NS':
			this.container.visibility = (vis) ? 'show' : 'hidden';
			break;
		case 'MOZ':
		case 'OPR':
		case 'KONQ':
		default:
			document.getElementById("SPAN" + this.id).style.visibility = (vis) ? '' : 'hidden';
			this.container.width = (vis) ? 250 : 0;
			this.container.height = (vis) ? 100 : 0;
			break; 
	}  
}
RemoteScriptingCall.prototype.escapeParam = function(str)
{
	return str.replace(/'"'/g, '\\"');
}
RemoteScriptingCall.prototype.unescape = function(str)
{
	return str.replace(/\\\//g, "/");
}
