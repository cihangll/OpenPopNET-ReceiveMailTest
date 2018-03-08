using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReceiveMailTest {
	public class Pop3Util {

		private const string HOSTNAME = "abc.abc.com";
		private const Int16 PORT = 110;
		private const string USERNAME = "abc@abc.com";
		private const string PASSWORD = "abc";


		public static bool TestConnect() {
			using (Pop3Client client = new Pop3Client()) {
				try {
					client.Connect(HOSTNAME, PORT, false);
					client.Authenticate(USERNAME, PASSWORD);

					client.Disconnect();
				}
				catch (Exception) {
					return false;
				}

				return true;
			}
		}

		public static Pop3Client Connect() {
			Pop3Client client = new Pop3Client();

			client.Connect(HOSTNAME, PORT, false);
			client.Authenticate(USERNAME, PASSWORD);

			return client;
		}

		public static bool Disconnect(Pop3Client client) {

			if (client.Connected) {
				client.Disconnect();
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.SuppressFinalize(client);
				return true;
			}
			else {
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.SuppressFinalize(client);
				return false;
			}
		}

		/// <summary>
		/// Example showing:
		///  - how to fetch all messages from a POP3 server
		/// </summary>
		/// <param name="hostname">Hostname of the server. For example: pop3.live.com</param>
		/// <param name="port">Host port to connect to. Normally: 110 for plain POP3, 995 for SSL POP3</param>
		/// <param name="useSsl">Whether or not to use SSL to connect to server</param>
		/// <param name="username">Username of the user on the server</param>
		/// <param name="password">Password of the user on the server</param>
		/// <returns>All Messages on the POP3 server</returns>
		public static List<Message> FetchAllMessages(string hostname, int port, bool useSsl, string username, string password) {

			// The client disconnects from the server when being disposed
			using (Pop3Client client = new Pop3Client()) {
				// Connect to the server
				client.Connect(hostname, port, useSsl);

				// Authenticate ourselves towards the server
				client.Authenticate(username, password);

				// Get the number of messages in the inbox
				int messageCount = client.GetMessageCount();

				// We want to download all messages
				List<Message> allMessages = new List<Message>(messageCount);

				// Messages are numbered in the interval: [1, messageCount]
				// Ergo: message numbers are 1-based.
				// Most servers give the latest message the highest number
				for (int i = messageCount; i > 0; i--) {
					allMessages.Add(client.GetMessage(i));
				}

				// Now return the fetched messages
				return allMessages;
			}
		}

		/// <summary>
		/// Example showing:
		///  - how to use UID's (unique ID's) of messages from the POP3 server
		///  - how to download messages not seen before
		///    (notice that the POP3 protocol cannot see if a message has been read on the server
		///     before. Therefore the client need to maintain this state for itself)
		/// </summary>
		/// <param name="hostname">Hostname of the server. For example: pop3.live.com</param>
		/// <param name="port">Host port to connect to. Normally: 110 for plain POP3, 995 for SSL POP3</param>
		/// <param name="useSsl">Whether or not to use SSL to connect to server</param>
		/// <param name="username">Username of the user on the server</param>
		/// <param name="password">Password of the user on the server</param>
		/// <param name="seenUids">
		/// List of UID's of all messages seen before.
		/// New message UID's will be added to the list.
		/// Consider using a HashSet if you are using >= 3.5 .NET
		/// </param>
		/// <returns>A List of new Messages on the server</returns>
		public static List<Message> FetchUnseenMessages(string hostname, int port, bool useSsl, string username, string password, List<string> seenUids) {

			// The client disconnects from the server when being disposed
			using (Pop3Client client = new Pop3Client()) {
				// Connect to the server
				client.Connect(hostname, port, useSsl);

				// Authenticate ourselves towards the server
				client.Authenticate(username, password);

				// Fetch all the current uids seen
				List<string> uids = client.GetMessageUids();

				// Create a list we can return with all new messages
				List<Message> newMessages = new List<Message>();


				// All the new messages not seen by the POP3 client
				for (int i = 0; i < uids.Count; i++) {
					string currentUidOnServer = uids[i];
					if (!seenUids.Contains(currentUidOnServer)) {
						// We have not seen this message before.
						// Download it and add this new uid to seen uids

						// the uids list is in messageNumber order - meaning that the first
						// uid in the list has messageNumber of 1, and the second has 
						// messageNumber 2. Therefore we can fetch the message using
						// i + 1 since messageNumber should be in range [1, messageCount]
						Message unseenMessage = client.GetMessage(i + 1);

						// Add the message to the new messages
						newMessages.Add(unseenMessage);

						// Add the uid to the seen uids, as it has now been seen
						seenUids.Add(currentUidOnServer);
					}
				}

				// Return our new found messages
				return newMessages;
			}
		}


		public static List<Message> FetchUnseenMessages(Pop3Client client, List<string> seenUids) {

			// Fetch all the current uids seen
			List<string> uids = client.GetMessageUids();

			// Create a list we can return with all new messages
			List<Message> newMessages = new List<Message>();

			// All the new messages not seen by the POP3 client
			for (int i = 0; i < uids.Count; i++) {
				string currentUidOnServer = uids[i];

				if (!seenUids.Contains(currentUidOnServer)) {
					// We have not seen this message before.
					// Download it and add this new uid to seen uids

					// the uids list is in messageNumber order - meaning that the first
					// uid in the list has messageNumber of 1, and the second has 
					// messageNumber 2. Therefore we can fetch the message using
					// i + 1 since messageNumber should be in range [1, messageCount]
					Message unseenMessage = client.GetMessage(i + 1);

					// Add the message to the new messages
					newMessages.Add(unseenMessage);

					// Add the uid to the seen uids, as it has now been seen
					// seenUids.Add(currentUidOnServer);
				}

			}

			// Return our new found messages
			return newMessages;
		}

		/// <summary>
		/// Example showing:
		///  - how to a find plain text version in a Message
		///  - how to save MessageParts to file
		/// </summary>
		/// <param name="message">The message to examine for plain text</param>
		public static void GetDataWithTypeInMessage(Message message, MessageTypes type) {
			MessagePart messagePart = null;
			if (type == MessageTypes.PlainText) {
				messagePart = message.FindFirstPlainTextVersion();
			}
			else if (type == MessageTypes.Html) {
				messagePart = message.FindFirstHtmlVersion();
			}
			else if (type == MessageTypes.XmlDocument) {
				messagePart = message.FindFirstMessagePartWithMediaType("text/xml");
			}

			if (messagePart != null) {
				if (type == MessageTypes.XmlDocument) {
					string xmlString = messagePart.GetBodyAsText();
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.LoadXml(xmlString);
					doc.Save("test.xml");
				}
				else {
					// Save the plain text to a file, database or anything you like
					messagePart.Save(new FileInfo("text.txt"));
				}
			}
		}

		/// <summary>
		/// Example showing:
		///  - how to delete a specific message from a server
		/// </summary>
		/// <param name="hostname">Hostname of the server. For example: pop3.live.com</param>
		/// <param name="port">Host port to connect to. Normally: 110 for plain POP3, 995 for SSL POP3</param>
		/// <param name="useSsl">Whether or not to use SSL to connect to server</param>
		/// <param name="username">Username of the user on the server</param>
		/// <param name="password">Password of the user on the server</param>
		/// <param name="messageNumber">
		/// The number of the message to delete.
		/// Must be in range [1, messageCount] where messageCount is the number of messages on the server.
		/// 
		/// 
		/// </param>
		public static void DeleteMessageOnServer(string hostname, int port, bool useSsl, string username, string password, int messageNumber) {

			// Silme şu mantıkla yapılıyor.Serverda bulunan bütün mailler tarihe göre sıralı halde.En uzaktaki tarihi 
			// 1 , sonraki 2 ...Yani en son eklenen maili silmek için "messageCount" değeri girilmelidir.


			// The client disconnects from the server when being disposed
			using (Pop3Client client = new Pop3Client()) {
				// Connect to the server
				client.Connect(hostname, port, useSsl);

				// Authenticate ourselves towards the server
				client.Authenticate(username, password);

				// Mark the message as deleted
				// Notice that it is only MARKED as deleted
				// POP3 requires you to "commit" the changes
				// which is done by sending a QUIT command to the server
				// You can also reset all marked messages, by sending a RSET command.
				client.DeleteMessage(messageNumber);

				// When a QUIT command is sent to the server, the connection between them are closed.
				// When the client is disposed, the QUIT command will be sent to the server
				// just as if you had called the Disconnect method yourself.


				//TODO: disconnect() çağrılmadığı sürece maili silmez.!!!
				client.Disconnect();
			}
		}

		/// <summary>
		/// Example showing:
		///  - how to delete fetch an emails headers only
		///  - how to delete a message from the server
		/// </summary>
		/// <param name="client">A connected and authenticated Pop3Client from which to delete a message</param>
		/// <param name="messageId">A message ID of a message on the POP3 server. Is located in <see cref="MessageHeader.MessageId"/></param>
		/// <returns><see langword="true"/> if message was deleted, <see langword="false"/> otherwise</returns>
		public static bool DeleteMessageByMessageId(Pop3Client client, string messageId) {
			// Get the number of messages on the POP3 server
			int messageCount = client.GetMessageCount();

			// Run trough each of these messages and download the headers
			for (int messageItem = messageCount; messageItem > 0; messageItem--) {
				// If the Message ID of the current message is the same as the parameter given, delete that message
				if (client.GetMessageHeaders(messageItem).MessageId == messageId) {
					// Delete
					client.DeleteMessage(messageItem);

					Disconnect(client);
					return true;
				}
			}

			Disconnect(client);

			// We did not find any message with the given messageId, report this back
			return false;
		}

		/// <summary>
		/// Example showing:
		///  - how to save a message to a file
		///  - how to load a message from a file at a later point
		/// </summary>
		/// <param name="message">The message to save and load at a later point</param>
		/// <returns>The Message, but loaded from the file system</returns>
		public static Message SaveAndLoadFullMessage(Message message) {
			// FileInfo about the location to save/load message
			FileInfo file = new FileInfo("someFile.eml");

			// Save the full message to some file
			message.Save(file);

			// Now load the message again. This could be done at a later point
			Message loadedMessage = Message.Load(file);

			// use the message again
			return loadedMessage;
		}

		/// <summary>
		/// Example showing:
		///  - how to fetch only headers from a POP3 server
		///  - how to examine some of the headers
		///  - how to fetch a full message
		///  - how to find a specific attachment and save it to a file
		/// </summary>
		/// <param name="hostname">Hostname of the server. For example: pop3.live.com</param>
		/// <param name="port">Host port to connect to. Normally: 110 for plain POP3, 995 for SSL POP3</param>
		/// <param name="useSsl">Whether or not to use SSL to connect to server</param>
		/// <param name="username">Username of the user on the server</param>
		/// <param name="password">Password of the user on the server</param>
		/// <param name="messageNumber">
		/// The number of the message to examine.
		/// Must be in range [1, messageCount] where messageCount is the number of messages on the server.
		/// </param>
		public static void HeadersFromAndSubject(string hostname, int port, bool useSsl, string username, string password, int messageNumber) {
			// The client disconnects from the server when being disposed
			using (Pop3Client client = new Pop3Client()) {
				// Connect to the server
				client.Connect(hostname, port, useSsl);

				// Authenticate ourselves towards the server
				client.Authenticate(username, password);

				// We want to check the headers of the message before we download
				// the full message
				MessageHeader headers = client.GetMessageHeaders(messageNumber);

				RfcMailAddress from = headers.From;
				string subject = headers.Subject;

				// Only want to download message if:
				//  - is from test@xample.com
				//  - has subject "Some subject"
				if (from.HasValidMailAddress && from.Address.Equals("test@example.com") && "Some subject".Equals(subject)) {
					// Download the full message
					Message message = client.GetMessage(messageNumber);

					// We know the message contains an attachment with the name "useful.pdf".
					// We want to save this to a file with the same name
					foreach (MessagePart attachment in message.FindAllAttachments()) {
						if (attachment.FileName.Equals("useful.pdf")) {
							//TODO: gelen dosyanın boyutuna göre, task oluşturduktan sonra o task'a ait tabloya güncelleme yap.
							//long byteSize = attachment.Body.Length;

							// Save the raw bytes to a file
							File.WriteAllBytes(attachment.FileName, attachment.Body);
						}
					}
				}
			}
		}


	}

	public enum MessageTypes {
		PlainText,
		Html,
		XmlDocument
	}

}
