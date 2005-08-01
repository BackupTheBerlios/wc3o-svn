function Refresh(i,sector) {
    if (i==1) {
        //currenty unused        
    }
    if (i==2) {
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
    else if (i==6) {
        document.getElementById("u_").innerHTML="";
        LoadUnits(sector,""); 
    }
    else if (i==7) {
        document.getElementById("b_").innerHTML="";
        LoadBuildings(sector,""); 
    }
}

function RefreshParent(i,sector) {
    if (i==1) {
        //currenty unused
    }
    else if (i==2) { //open the constructing
        window.opener.document.getElementById("constructing").innerHTML="";
        window.opener.LoadConstructing(sector);
    }
    else if (i==3) { //open the training
        window.opener.document.getElementById("training").innerHTML="";
        window.opener.LoadTraining(sector);
    }
    else if (i==4) { //open the overview
        window.opener.document.getElementById("u_"+sector).innerHTML="";
        window.opener.document.getElementById("b_"+sector).innerHTML="";        
        window.opener.LoadOverview(sector);
    }
    else if (i==5) { //open the navigation
        window.opener.LoadNavigation();
    }
    else if (i==6) { //open the player units
        if (!window.opener.document.getElementById("u_"))
            RefreshParent(4,sector);  
        else {
            window.opener.document.getElementById("u_").innerHTML="";
            window.opener.LoadUnits(sector,"");
        }
    }
    else if (i==7) { //open the player buildings
        if (!window.opener.document.getElementById("b_"))
            RefreshParent(4,sector);  
        else {
            window.opener.document.getElementById("b_").innerHTML="";
            window.opener.LoadBuildings(sector,""); 
       }
    }
}