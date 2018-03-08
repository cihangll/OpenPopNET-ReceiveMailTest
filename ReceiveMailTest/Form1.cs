using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReceiveMailTest {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			var list = Pop3Util.FetchAllMessages("abc.abc.com", 110, false, "abc@abc.com", "abc");
		}

		private void button2_Click(object sender, EventArgs e) {

			Pop3Client client = Pop3Util.Connect();
			if (client != null) {

				var list = Pop3Util.FetchAllMessages("abc.abc.com", 110, false, "abc@abc.com", "abc");
				Pop3Util.GetDataWithTypeInMessage(list[0], MessageTypes.PlainText);
			}

			Pop3Util.Disconnect(client);
		}

		private void button3_Click(object sender, EventArgs e) {

			Pop3Client client = Pop3Util.Connect();
			if (client != null) {

				var list = Pop3Util.FetchAllMessages("abc.abc.com", 110, false, "abc@abc.com", "abc");
				Pop3Util.GetDataWithTypeInMessage(list[0], MessageTypes.Html);
			}

			Pop3Util.Disconnect(client);
		}

		private void button4_Click(object sender, EventArgs e) {

			Pop3Client client = Pop3Util.Connect();
			if (client != null) {

				var list = Pop3Util.FetchAllMessages("abc.abc.com", 110, false, "abc@abc.com", "abc");

				Pop3Util.GetDataWithTypeInMessage(list[0], MessageTypes.XmlDocument);
			}

			Pop3Util.Disconnect(client);

		}

		private void button5_Click(object sender, EventArgs e) {
			var list = Pop3Util.FetchAllMessages("abc.abc.com", 110, false, "abc@abc.com", "abc");
			Pop3Util.DeleteMessageOnServer("abc.abc.com", 110, false, "abc@abc.com", "abc", 3);
		}

		private void button6_Click(object sender, EventArgs e) {
			var list = Pop3Util.FetchAllMessages("abc.abc.com", 110, false, "abc@abc.com", "abc");
			//PROJE_YOLU\repos\ReceiveMailTest\ReceiveMailTest\bin\Debug içerisinde eml dosyası oluşturup geri aldık.
			OpenPop.Mime.Message message = Pop3Util.SaveAndLoadFullMessage(list[0]);
		}

		private void button7_Click(object sender, EventArgs e) {
			List<OpenPop.Mime.Message> unseenMessages = Pop3Util.FetchUnseenMessages("abc.abc.com", 110, false, "abc@abc.com", "abc", new List<string> { "1234567890" });
		}

		private void button8_Click(object sender, EventArgs e) {


			bool status = Pop3Util.DeleteMessageByMessageId(Pop3Util.Connect(), "abcdefg+abnc=teA-caaca=caccc@mail.gmail.com");

		}

		private void receiveMail_Click(object sender, EventArgs e) {

			DefaultConnection connection = new DefaultConnection();

			Pop3Client client = Pop3Util.Connect();

			List<OpenPop.Mime.Message> mails = Pop3Util.FetchUnseenMessages(client, connection.ReceivedMails.Select(r => r.Uid.ToString()).ToList());

			ReceivedMail receivedMail;
			for (int i = 0; i < mails.Count; i++) {

				receivedMail = new ReceivedMail();
				receivedMail.MessageId = mails[i].Headers.MessageId;
				receivedMail.Uid = client.GetMessageUid(i + 1);
				receivedMail.CreatedDate = DateTime.Now;
				receivedMail.ReceiveDate = mails[i].Headers.DateSent;
				receivedMail.SendBy = mails[i].Headers.From.MailAddress.Address;
				receivedMail.Title = mails[i].Headers.Subject;
				receivedMail.Body = mails[i].FindFirstPlainTextVersion() != null ? mails[i].FindFirstPlainTextVersion().GetBodyAsText() : "";
				receivedMail.Status = 0;

				var ccList = mails[i].Headers.Cc;
				var ccListString = "";
				for (int j = 0; j < ccList.Count; j++) {
					if (ccList[j].HasValidMailAddress) {
						ccListString += ccList[j].Address;
					}

					if (j != ccList.Count - 1) {
						ccListString += ";";
					}
				}
				receivedMail.Cc = ccListString;

				connection.ReceivedMails.Add(receivedMail);
				receivedMail = null;


			}
			connection.SaveChanges();

			Pop3Util.Disconnect(client);
		}
	}
}
