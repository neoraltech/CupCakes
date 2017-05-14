using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR.Client;
using KarrenCupCakes.Models;
using Newtonsoft.Json;
using KarrenCupCakes.Controllers;

namespace KarrenCupCakes.Core
{
    public class SignalRConnector
    {
        private static IHubProxy _hubProxy { get; set; }
        private static HubConnection _connection { get; set; }

        private static string _serverURI = ConfigurationManager.AppSettings["WebURL"].ToString();
        private static string _hubName = ConfigurationManager.AppSettings["HubName"].ToString();
        private static string _connectionId = string.Empty;

        private static List<UserData> _userDataList = new List<UserData>();

        public static async void ConnectAsync()
        {
            _connection = new HubConnection(_serverURI);

            _connection.Closed += Connection_Closed;
            _hubProxy = _connection.CreateHubProxy(_hubName);

            try
            {
                await _connection.Start();

                _connectionId = _connection.ConnectionId;
                
                _hubProxy.On<string>("SendUserMessage", (message) => ContactController.MessageReceived(message));
                
            }
            catch (HttpRequestException)
            {
                //TODO: implement a logger here.
            }
        }

        public static bool AddThisUser(UserData userData)
        {
            userData.ConnectionId = _connectionId;
            Predicate<UserData> predicateData = (x => x == userData);
            bool result = false;

            try
            {
                if (!_userDataList.Exists(predicateData))
                {
                    _userDataList.Add(userData);

                    _hubProxy.Invoke("UserAccess", JsonConvert.SerializeObject(userData));
                }

                result = true;
            }
            catch (Exception ex)
            {
                //TODO: implement a logger here.
            }

            return result;
        }

        public static bool SendMessage(UserData userData, string message)
        {
            userData.ConnectionId = _connectionId;
            Predicate<UserData> predicateData = (x => x == userData);
            bool result = false;

            try
            {
                if (_userDataList.Exists(predicateData))
                {
                    _hubProxy.Invoke("SendMessageToAdmin", _connectionId, message);
                }

                result = true;
            }
            catch (Exception)
            {
                //TODO: implement a logger here.
            }

            return result;
        }



        private static void Connection_Closed()
        {
            _connection.Dispose();
        }
    }
}