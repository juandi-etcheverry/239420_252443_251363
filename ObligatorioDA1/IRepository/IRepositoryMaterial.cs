﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace IRepository
{
    public interface IRepositoryMaterial : IRepository<Material>
    {
        List<Material> FindMany(string name);
    }
}
