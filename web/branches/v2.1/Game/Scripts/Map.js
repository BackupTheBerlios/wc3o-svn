function LoadMap(coordinates) {
    document.getElementById("divMap").innerHTML = "loading map ...";
    RS.Execute("Details/Map.aspx", "GetMap", coordinates, WriteMap);
}


function CenterOnSector() {
    LoadMap(document.getElementById("drpSectors").value);
}


function CenterOnCoordinates() {
    LoadMap(document.getElementById("txtX").value + "_" + document.getElementById("txtY").value);
}


function LoadInfo(coordinates) {
    document.getElementById("divInfo").innerHTML = "loading info ...";
    RS.Execute("Details/Map.aspx", "GetInfo", coordinates, WriteInfo)
}


function WriteInfo(result) {
    array = result.split("@");
    gfx = array[0];
    coordinate = array[1];
    sectorFullName = array[2];
    ownerName = array[3];
    ownerFullName = array[4];
    hasView = array[5];
    
    html = "<b><a href='Sector.aspx?Sector=" + coordinate + "'>" + sectorFullName + "</a></b><br />";
    if (ownerName == "")
        html += "<i>This sector has no owner.</i><br /><br />";
    else
        html += "Owner: <a href='PlayerInfo.aspx?Player=" + ownerName + "'>" + ownerFullName + "</a>";
        
    if (hasView == "0")
        html += "<br /><br /><i>You have no view on this sector.</i>";    
        
    for (i = 6; i < array.length - 1; i+=3) {
        html += "<br /><br /><div style='border: 1px solid " + array[i] + "'>";
        if (array[i+1] == "1")
            html += "<img src='"+gfx+"/Map/Units.gif' />";
        if (array[i+2] == "1")
            html += "<img src='"+gfx+"/Map/Buildings.gif' />";              
        html += "</div>";
    }     
           
    document.getElementById("divInfo").innerHTML = html;
}


function WriteMap(result) {
    array = result.split("@");
    gfx = array[0];
    size = array[1]; 
    centerX = array[2];
    centerY = array[3];
          
    html="<div id='divInfo' style='padding-top: 150px; float: left; width:30%;'></div><div style='float: right; width:65%; text-align: center;'><table cellpadding='0px' cellspacing='0px' border='0px'><tr><td><a style='cursor: nw-resize;' OnClick=\"LoadMap('" + ((+centerX)-size) + "_" + ((+centerY)-size) + "')\"><img title='Scroll up left' src='" + gfx + "/Map/Top-Left.gif' /></a></td><td><a style='cursor: n-resize;' OnClick=\"LoadMap('" + centerX + "_" + ((+centerY)-size) + "')\"><img title='Scroll up' src='" + gfx + "/Map/Top.gif' /></a></td><td><a style='cursor: ne-resize;' OnClick=\"LoadMap('" + ((+centerX)+(+size)) + "_" + ((+centerY)-size) + "')\"><img title='Scroll top right' src='" + gfx + "/Map/Top-Right.gif' /></a></td></tr><td><a style='cursor: w-resize;' OnClick=\"LoadMap('" + ((+centerX)-size) + "_" + centerY + "')\"><img title='Scroll left' src='" + gfx + "/Map/Left.gif' /></a></td><td><table cellpadding='0px' cellspacing='0px'><tr>";

    x=1;    
    for (i=4; i < array.length - 1; i++) {
        sectorArray = array[i].split("$");
        type = sectorArray[0];
        color = sectorArray[1];
        name = sectorArray[2];
        shortName = sectorArray[3];
        coordinate = sectorArray[4];
        owner = sectorArray[5];          
        
        image = "Grass";
        switch (type) {
            case "1":
                image = "Humans1";
                break;
            case "2":
                image = "Humans2";
                break;
            case "3":
                image = "Humans3";
                break;                        
            case "4":
                image = "Orcs1";
                break;
            case "5":
                image = "Orcs2";
                break;
            case "6":
                image = "Orcs3";
                break;      
            case "7":
                image = "Undead1";
                break;
            case "8":
                image = "Undead2";
                break;
            case "9":
                image = "Undead3";
                break;                        
            case "10":
                image = "NightElves1";
                break;
            case "11":
                image = "NightElves2";
                break;
            case "12":
                image = "NightElves3";
                break;   
            case "13":
                image = "GoldAndLumber";
                break;                        
            case "14":
                image = "Gold";
                break;
            case "15":
                image = "Lumber";
                break;
            case "16":
                image = "Fountain";
                break;                                          
            case "17":
                image = "Mercenaries";
                break;          
        }
       
        title = name;
        if (owner != "")
            title += " of " + owner;
       
        html += "<td title='" + title + "' OnMouseOver = \"this.style.border = '1px solid " + color + "';\" OnMouseOut = \"this.style.border = '1px solid #000000';\" OnClick=\"LoadInfo('"+coordinate+"');\" style='border: 1px solid #000000; height: 70px; width: 70px; vertical-align: bottom; color: " + color + "; background-image: url(" + gfx + "/Map/" + image + ".gif)'>" + shortName + "</td>";

        if (x == size) {
            html += "</tr><tr>";
            x = 1;
        }
        else
            x++;
    }
      
    html+="</tr></table></td><td><a style='cursor: e-resize;' OnClick=\"LoadMap('" + ((+centerX)+(+size)) + "_" + centerY + "')\"><img title='Scroll right' src='"+gfx+"/Map/Right.gif' /></a></td><tr><td><a style='cursor: sw-resize;' OnClick=\"LoadMap('" + ((+centerX)-size) + "_" + ((+centerY)+(+size)) + "')\"><img title='Scroll down left' src='"+gfx+"/Map/Bottom-Left.gif' /></a></td><td><a style='cursor: s-resize;' OnClick=\"LoadMap('" + centerX + "_" + ((+centerY)+(+size)) + "')\"><img title='Scroll down' src='"+gfx+"/Map/Bottom.gif' /></a></td><td><a style='cursor: se-resize;' OnClick=\"LoadMap('" + ((+centerX)+(+size)) + "_" + ((+centerY)+(+size)) + "')\"><img title='Scroll down right' src='"+gfx+"/Map/Bottom-Right.gif' /></a></td></tr></table></div>";      
    document.getElementById("divMap").innerHTML = html;
}