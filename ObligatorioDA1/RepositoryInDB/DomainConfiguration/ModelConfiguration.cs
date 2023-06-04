﻿using Domain;
using System.Data.Entity;

namespace RepositoryInDB.DomainConfiguration
{
    internal class ModelConfiguration
    {
        public static void ConfigureEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>().ToTable("Models");
        }
    }
}
