using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace Sender
{
	class Program
	{
		private static Uri _publicKeyServiceUri = new Uri("http://localhost:7695/PublicKey");
		private static Uri _secretMessageServiceUri = new Uri("http://localhost:7695/Message");

		static void Main(string[] args)
		{
			PublicKeyResponse publicKeyResponse = GetPublicKeyMessageId();
			if (publicKeyResponse.MessageId != null && publicKeyResponse.RsaPublicKey != null)
			{
				Console.Write("Public key request successful. Enter your message: ");
				string message = Console.ReadLine();
				SymmetricAlgorithmService symmetricService = new SymmetricAlgorithmService();
				SymmetricEncryptionResult symmetricEncryptionResult = symmetricService.Encrypt(message);
				AsymmetricEncryptionService asymmetricService = new AsymmetricEncryptionService(publicKeyResponse.RsaPublicKey);
				string encryptedAesPublicKey = asymmetricService.GetCipherText(ConfigurationManager.AppSettings["RijndaelManagedKey"]);
				SendMessageArguments sendMessageArgs = new SendMessageArguments() { EncryptedPublicKey = encryptedAesPublicKey, 
					SymmetricEncryptionArgs = symmetricEncryptionResult, MessageId = publicKeyResponse.MessageId };
				string jsonifiedArgs = JsonConvert.SerializeObject(sendMessageArgs);
				SendSecretMessageToReceiver(jsonifiedArgs);
			}
			else
			{
				Console.WriteLine("Public key request failed.");
			}
			Console.ReadKey();
		}

		private static void SendSecretMessageToReceiver(string jsonArguments)
		{
			try
			{
				HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, _secretMessageServiceUri);
				requestMessage.Headers.ExpectContinue = false;
				requestMessage.Content = new StringContent(jsonArguments, Encoding.UTF8, "application/json");
				HttpClient httpClient = new HttpClient();
				Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(requestMessage,
					HttpCompletionOption.ResponseContentRead, CancellationToken.None);
				HttpResponseMessage httpResponse = httpRequest.Result;
				HttpStatusCode statusCode = httpResponse.StatusCode;
				HttpContent responseContent = httpResponse.Content;
				if (responseContent != null)
				{
					Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
					String stringContents = stringContentsTask.Result;
					Console.Write(stringContents);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception caught while sending the secret message to the Receiver: "
					+ ex.Message);
			}
		}

		private static PublicKeyResponse GetPublicKeyMessageId()
		{
			PublicKeyResponse resp = new PublicKeyResponse();
			try
			{
				HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, _publicKeyServiceUri);
				requestMessage.Headers.ExpectContinue = false;
				HttpClient httpClient = new HttpClient();
				httpClient.Timeout = new TimeSpan(0, 10, 0);
				Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(requestMessage,
						HttpCompletionOption.ResponseContentRead, CancellationToken.None);
				HttpResponseMessage httpResponse = httpRequest.Result;
				HttpStatusCode statusCode = httpResponse.StatusCode;
				HttpContent responseContent = httpResponse.Content;
				if (responseContent != null)
				{
					Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
					String stringContents = stringContentsTask.Result.Replace("$", string.Empty);
					XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(stringContents, "root");
					XmlElement root = doc.DocumentElement;
					XmlNode messageIdNode = root.SelectSingleNode("MessageId");
					resp.MessageId = Guid.Parse(doc.DocumentElement.SelectSingleNode("MessageId").InnerText);
					RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
					rsaProvider.FromXmlString(doc.DocumentElement.SelectSingleNode("PublicKeyXml").InnerXml);
					resp.RsaPublicKey = rsaProvider;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return resp;
		}
	}
}
