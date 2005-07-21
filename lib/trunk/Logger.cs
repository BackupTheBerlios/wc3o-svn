using System;

namespace Wc3o.Log {
    public class Logger {

        #region " Log "
        public void Log(string message, LogType type) {
			System.IO.StreamWriter writer = new System.IO.StreamWriter(Configuration.Physical_Application_Path + "\\App_Data\\Log.txt", true);
            writer.WriteLine(Game.Format(Game.GetCorrectedDate(), true) + " : " + type.ToString() + " : " + message);
            writer.Close();
        }
        #endregion

    }

    public enum LogType { System }
}
