﻿using System.Collections.Generic;
using Domain;

namespace IRepository
{
    public interface IRepositoryLog
    {
        IList<Log> GetAll();
        Log Add(Log x);
        Log Get(string sceneName, string clientName);
    }
}