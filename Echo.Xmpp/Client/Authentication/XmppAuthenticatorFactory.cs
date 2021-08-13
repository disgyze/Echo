using System;
using System.Collections.Generic;
using System.Linq;

namespace Echo.Xmpp.Client.Authentication
{
	public sealed class XmppAuthenticatorFactory : IXmppAuthenticatorFactory
	{
		Dictionary<string, IXmppAuthenticator> map = new Dictionary<string, IXmppAuthenticator>();
		object syncRoot = new object();
		static Lazy<IXmppAuthenticatorFactory> defaultInstance = new Lazy<IXmppAuthenticatorFactory>(() => new XmppAuthenticatorFactory());

		public static IXmppAuthenticatorFactory Default
		{
			get
			{
				return defaultInstance.Value;
			}
		}

		public IXmppAuthenticator Create(IEnumerable<string> mechanisms)
		{
			lock (syncRoot)
			{
				foreach (var auth in map)
				{
					if (mechanisms.Contains(auth.Key))
					{
						return auth.Value;
					}
				}
			}
			return null;
		}

		public void Register<TAuthentication>(string mechanism) where TAuthentication : IXmppAuthenticator
		{
			map.Add(mechanism, (TAuthentication)Activator.CreateInstance(typeof(TAuthentication)));
		}
	}
}