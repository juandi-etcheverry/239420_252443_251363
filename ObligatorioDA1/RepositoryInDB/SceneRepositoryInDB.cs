﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain;
using IRepository;

namespace RepositoryInDB
{
    public class SceneRepositoryInDB : IRepositoryScene
    {
        public Scene Add(Scene scene)
        {
            using (var context = new BusinessContext())
            {
                var loggedClient = context.Clients.FirstOrDefault(c => c.Name == scene.Client.Name);
                scene.Client = loggedClient;
                context.Scenes.Add(scene);
                context.SaveChanges();
            }

            return scene;
        }

        public IList<Scene> GetAll()
        {
            using (var context = new BusinessContext())
            {
                return context.Scenes
                    .Include(s => s.Client)
                    .Include(s => s.Models.Select(m => m.Model))
                    .ToList();
            }
        }

        public Scene Get(int id)
        {
            using (var context = new BusinessContext())
            {
                return GetWithContext(context, id);
            }
        }

        public Scene Remove(Scene scene)
        {
            using (var context = new BusinessContext())
            {
                var sceneToRemove = context.Scenes.Include(s => s.Models).FirstOrDefault(s => s.Id == scene.Id);
                context.Scenes.Remove(sceneToRemove);
                context.SaveChanges();
                return sceneToRemove;
            }
        }

        public void DeleteModel(Scene scene, PositionedModel model)
        {
            using (var context = new BusinessContext())
            {
                var sceneToDelete = context.Scenes
                    .Include(m => m.Models)
                    .Include(m => m.ClientScenePreferences)
                    .FirstOrDefault(s => s.Id == scene.Id);

                var modelAux = context.Scenes.Include(s => s.Models)
                    .FirstOrDefault(s => s.Id == scene.Id).Models
                    .FirstOrDefault(m => m.Id == model.Id);

                context.Entry(modelAux).State = EntityState.Deleted;

                sceneToDelete.LastModificationDate = DateTime.Now;
                context.SaveChanges();
            }
        }

        public void AddModel(Scene scene, PositionedModel model)
        {
            using (var context = new BusinessContext())
            {
                var loggedClient = context.Clients
                    .Include(c => c.ClientScenePreferences)
                    .FirstOrDefault(c => c.Name == scene.Client.Name);

                var sceneAux = context.Scenes
                    .Include(s => s.Models)
                    .Include(s => s.ClientScenePreferences)
                    .FirstOrDefault(s => s.Id == scene.Id);

                sceneAux.Client = loggedClient;

                var modelAux = context.Models.Find(model.Model.Id);
                model.Model = modelAux;

                sceneAux.Models.Add(model);
                sceneAux.LastModificationDate = DateTime.Now;
                context.SaveChanges();
            }
        }


        public PositionedModel GetModel(Scene scene, int idModel)
        {
            using (var context = new BusinessContext())
            {
                var sceneToGet = context.Scenes
                    .Include(m => m.Models.Select(p => p.Model).Select(x => x.Shape))
                    .Include(m => m.Models.Select(p => p.Model).Select(x => x.Material))
                    .FirstOrDefault(s => s.Id == scene.Id);

                return sceneToGet.Models.FirstOrDefault(m =>
                    m.Id == idModel);
            }
        }

        public Scene Update(Scene scene)
        {
            using (var context = new BusinessContext())
            {
                var sceneToUpdate = GetWithContext(context, scene.Id);
                sceneToUpdate.SceneName = scene.SceneName;
                sceneToUpdate.LastModificationDate = scene.LastModificationDate;
                sceneToUpdate.LastRenderDate = scene.LastRenderDate;
                sceneToUpdate.ClientScenePreferences = scene.ClientScenePreferences;
                context.SaveChanges();
                return sceneToUpdate;
            }
        }

        private Scene GetWithContext(BusinessContext context, int id)
        {
            return context.Scenes
                .Include(s => s.Client)
                .Include(s => s.Models.Select(p => p.Model))
                .Include(s => s.Models.Select(p => p.Model).Select(m => m.Material))
                .Include(s => s.Models.Select(p => p.Model).Select(m => m.Shape))
                .Include(s => s.ClientScenePreferences)
                .FirstOrDefault(s => s.Id == id);
        }

        public PositionedModel GetModel1(Scene scene, int id)
        {
            using (var context = new BusinessContext())
            {
                var sceneToGet = context.Scenes
                    .Include(m => m.Models.Select(p => p.Model).Select(x => x.Shape))
                    .Include(m => m.Models.Select(p => p.Model).Select(x => x.Material))
                    .FirstOrDefault(s => s.Id == scene.Id);

                return sceneToGet.Models.FirstOrDefault(m =>
                    m.Model.Id == id);
            }
        }
    }
}