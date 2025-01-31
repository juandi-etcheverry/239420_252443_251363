﻿using System;
using BusinessLogicExceptions;
using ValidationService;

namespace Domain
{
    public class Client
    {
        private static readonly uint MIN_NAME_LENGTH = 3;
        private static readonly uint MAX_NAME_LENGTH = 20;
        private static readonly uint MIN_PASSWORD_LENGTH = 5;
        private static readonly uint MAX_PASSWORD_LENGTH = 25;
        private string _name;
        private string _password;

        public Client()
        {
            RegistrationDate = DateTime.Now;
            ClientScenePreferences = new ClientScenePreferences
            {
                FoVDefault = 30
            };
            ClientScenePreferences.SetLookFromDefault((0, 2, 0));
            ClientScenePreferences.SetLookAtDefault((0, 2, 5));
        }

        public DateTime RegistrationDate { get; set; }
        public ClientScenePreferences ClientScenePreferences { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (!value.IsBetween(MIN_NAME_LENGTH, MAX_NAME_LENGTH)) ThrowNameNotInRange();
                if (value.HasSpaces()) ThrowHasSpaces();
                if (!value.IsAlphaNumeric()) ThrowNotAlphanumeric();
                _name = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (!value.IsBetween(MIN_PASSWORD_LENGTH, MAX_PASSWORD_LENGTH)) ThrowPasswordNotInRange();
                if (!value.HasUpper()) ThrowPasswordNoCapitalLetter();
                if (!value.HasNumber()) ThrowNoNumberPassword();
                _password = value;
            }
        }

        private void ThrowNotAlphanumeric()
        {
            throw new NameException("Client name can't have non-alphanumeric characters");
        }

        private void ThrowHasSpaces()
        {
            throw new NameException("Client name can't have spaces");
        }

        private void ThrowNameNotInRange()
        {
            throw new NameException("Client name must be between 3 and 20 characters");
        }

        private void ThrowPasswordNotInRange()
        {
            throw new PasswordException("Client password must be between 5 and 25 characters");
        }

        private void ThrowPasswordNoCapitalLetter()
        {
            throw new PasswordException("Client password must have at least one capital letter");
        }

        private void ThrowNoNumberPassword()
        {
            throw new PasswordException("Client password must have at least one number");
        }
    }
}