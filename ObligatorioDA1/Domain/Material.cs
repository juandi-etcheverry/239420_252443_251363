﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicExceptions;
using ValidationService;

namespace Domain
{
    public enum MaterialType
    {
        Lambertian
    }

    public class Material
    {
        private string _name;
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

        private ValueTuple<uint, uint, uint> _color { get; set; }

        public ValueTuple<uint, uint, uint> Color
        {
            get => _color;
            set
            {
                if (HasInvalidColor(value)) ThrowColorsOutOfRange();
                _color = value;
            }
        }

        public MaterialType Type { get; set; }

        private static void ThrowEmptyName()
        {
            throw new NameException("Material name can't be empty");
        }

        private static void ThrowHasTrailingSpaces()
        {
            throw new NameException("Material name can't have trailing spaces");
        }

        private static void ThrowColorsOutOfRange()
        {
            throw new ArgumentOutOfRangeException("Material color can only be between 0 and 255");
        }

        private bool HasInvalidColor(ValueTuple<uint, uint, uint> color)
        {
            return color.Item1 > 255 || color.Item2 > 255 || color.Item3 > 255;
        }
    }
}
