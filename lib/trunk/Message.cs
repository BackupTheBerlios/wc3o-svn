using System;

namespace Wc3o {
    [Serializable]
    public class Message : DataObject {

        #region " Constructor "
		public Message() {
		}

        public Message(Player recipient,Player sender,string subject, string text) {
            this.recipient = recipient;
            this.sender = sender;
			this.subject = subject;
			this.text = text;
			this.unread = true;
			this.date = DateTime.Now;
			Recipient.Messages.Add(this);
        }
        #endregion

        #region " Destroy "
        public override void Destroy() {
            Recipient.Messages.Remove(this);
        }
        #endregion

        #region " Properties "
        Player recipient;
        public Player Recipient {
            get {
                return recipient;
            }
            set {
                recipient = value;
            }
        }

        Player sender;
        public Player Sender {
            get {
                return sender;
            }
            set {
                sender = value;
            }
        }

        string subject;
        public string Subject {
            get {
                return subject;
            }
            set {
                subject = value;
            }
        }

        string text;
        public string Text {
            get {
                return text;
            }
            set {
                text = value;
            }
        }

        bool unread;
        public bool Unread {
            get {
                return unread;
            }
            set {
                unread = value;
            }
        }

        DateTime date;
        public DateTime Date {
            get {
                return date;
            }
            set {
                date = value;
            }
        }
        #endregion


    }
}
