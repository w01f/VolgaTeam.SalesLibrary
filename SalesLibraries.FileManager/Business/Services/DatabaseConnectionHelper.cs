using System;
using System.IO;
using SalesLibraries.Common.Configuration;
using SalesLibraries.FileManager.Business.Models.Connection;

namespace SalesLibraries.FileManager.Business.Services
{
	class DatabaseConnectionHelper
	{
		public static ConnectionState GetConnectionState(string databaseRoot)
		{
			var connectionSateInfoFilePath = Path.Combine(databaseRoot, Constants.DatabaseConnectionStateInfoFileName);
			if (File.Exists(connectionSateInfoFilePath))
			{
				var connection = new ConnectionState { User = File.ReadAllText(connectionSateInfoFilePath) };
				connection.Type = String.Equals(connection.User, Environment.UserName, StringComparison.OrdinalIgnoreCase)
					? ConnectionStateType.Available
					: ConnectionStateType.Busy;
				return connection;
			}
			return new ConnectionState { Type = ConnectionStateType.Available };
		}

		public static void Connect(string databaseRoot)
		{
			var connectionSateInfoFilePath = Path.Combine(databaseRoot, Constants.DatabaseConnectionStateInfoFileName);
			File.WriteAllText(connectionSateInfoFilePath, Environment.UserName);
		}

		public static void Disconnect(string databaseRoot)
		{
			var connectionSateInfoFilePath = Path.Combine(databaseRoot, Constants.DatabaseConnectionStateInfoFileName);
			if (File.Exists(connectionSateInfoFilePath))
				File.Delete(connectionSateInfoFilePath);
		}
	}
}
