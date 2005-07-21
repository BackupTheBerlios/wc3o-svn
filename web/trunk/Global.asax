<%@ Application Language="C#" %>

<script runat="server">
   
    void Application_Start(Object sender, EventArgs e) {
        Wc3o.Game.Logger = new Wc3o.Log.Logger();

        Wc3o.Game.Logger.Log("Application starts ...",Wc3o.Log.LogType.System);

        Wc3o.Game.GameData = Wc3o.GameData.Load();
        Wc3o.Game.PortalData = Wc3o.PortalData.Load();
        //Wc3o.Game.GameData = new Wc3o.GameData();
        //Wc3o.Game.PortalData = new Wc3o.PortalData(); 
        Wc3o.Game.Ticker = new Wc3o.Tick.Ticker();
    }

    void Application_End(Object sender, EventArgs e) {
        Wc3o.Game.Logger.Log("Application ends ...", Wc3o.Log.LogType.System);
    }
    
</script>
