﻿using BusinessLogicExceptions;
using ValidationService;

namespace Domain
{
    public class Model
    {
        private string _name;
        public bool WantPreview;

        public string Name
        {
            get => _name;
            set
            {
                if (value.IsEmpty()) ThrowEmptyName();
                if (value.HasTrailingSpaces()) ThrowHasTrailingSpaces();
                _name = value;
            }
        }

        public Shape Shape { get; set; }

        public Material Material { get; set; }

        public string OwnerName { get; set; }

        public static void ThrowClientNotLoggedIn()
        {
            throw new SessionException("Client needs to be logged in to create new model");
        }

        private static void ThrowEmptyName()
        {
            throw new NameException("Model name can't be empty");
        }

        private static void ThrowHasTrailingSpaces()
        {
            throw new NameException("Model name can't have trailing spaces");
        }
    }
}