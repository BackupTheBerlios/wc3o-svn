using System;

namespace Wc3o {
    public class Logger {

        #region " Log "
        public void Log(string message) {
			System.IO.StreamWriter writer = new System.IO.StreamWriter(Configuration.Physical_Application_Path + "\\App_Data\\Log.txt", true);
            writer.WriteLine(Game.Format(Game.GetCorrectedDate(), true) + " : " + message);
            writer.Close();
        }
        #endregion

    }

}
