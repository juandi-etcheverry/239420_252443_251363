﻿using BusinessLogicExceptions;
using ValidationService;

namespace Domain
{
    public class Shape
    {
        private string _name { get; set; }

        public string ShapeName
        {
            get => _name;
            set
            {
                if (value.IsEmpty()) ThrowEmptyName();
                if (value.HasTrailingSpaces()) ThrowHasTrailingSpaces();
                _name = value;
            }
        }

        public int Id { get; set; }
        public Client Client { get; set; }

        public bool AreNamesEqual(string otherName)
        {
            return _name.ToLower() == otherName.ToLower();
        }

        private void ThrowEmptyName()
        {
            throw new NameException("Shape Name can't be empty");
        }

        private void ThrowHasTrailingSpaces()
        {
            throw new NameException("Shape Name can't have trailing spaces");
        }
    }
}