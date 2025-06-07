using Data.Load;
using Data.Save;
using UnityEngine;
using VContainer;

namespace Registrators
{
    public class DataRegistrator
    {
        public static void Register(IContainerBuilder builder)
        {
            builder.Register<ISaveManager, SaveManager>(Lifetime.Singleton);
            builder.Register<ILoadManager, LoadManager>(Lifetime.Singleton);
            
            var levels = Resources.LoadAll<ScriptableObject>("Data/Levels");
            foreach (var level in levels)
            {
                Debug.Log($"Registering data: {level.name} ({level.GetType()})");
                builder.RegisterInstance(level)
                    .As(level.GetType())
                    .AsImplementedInterfaces();
            }
            var ships = Resources.LoadAll<ScriptableObject>("Data/Ships");
            foreach (var ship in ships)
            {
                Debug.Log($"Registering data: {ship.name} ({ship.GetType()})");
                builder.RegisterInstance(ship)
                    .As(ship.GetType())
                    .AsImplementedInterfaces();
            }
        }
    }
}