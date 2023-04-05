﻿using BusinessLogic;
using DataHandlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BusinessLogicTest
{
    [TestClass]
    public class ClientValidatorTest
    {
        [TestCleanup]
        public void DataHandler_RemoveAllShapes()
        {
            DataHandler.RemoveAllClients();
        }

        [TestMethod]
        public void SignUp_Client_Name_OK_Password_OK_Test()
        {
            Client newClient = new Client();
            newClient.Name = "Nicolas";
            newClient.Password = "password123";
            DataHandler.AddClient(newClient);
            Assert.AreEqual(1, DataHandler.Clients.Count);
        }

        [TestMethod]
        public void SignUp_Client_Name_NotUnique_Password_OK_FAIL_Test()
        {
            Client client1 = new Client();
            client1.Name = "Nicolas";
            client1.Password = "password123";
            DataHandler.AddClient(client1);
            Client client2 = new Client();
            client2.Name = "Nicolas";
            client2.Password = "noimporta123";
            Assert.ThrowsException<UniqueNameException>(() => DataHandler.AddClient(client2));
        }
    }
}
