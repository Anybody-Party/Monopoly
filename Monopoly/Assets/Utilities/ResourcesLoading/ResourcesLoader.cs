using UnityEngine;

namespace RH.Utilities.ResourcesLoading
{
    public static class ResourcesLoader
    {
        private static BaseResourcesFactory _resourcesFactory;

        static ResourcesLoader()
        {
            _resourcesFactory = new BaseResourcesFactory();
        }

        public static T Get<T>(string name) where T : Object
            => _resourcesFactory.Get<T>(name);

        public static T Instantiate<T>(string name, Transform parent = null) where T : Component
            => _resourcesFactory.Instantiate<T>(name, parent);

        public static T Instantiate<T>(string name, Vector3 position, Transform parent = null) where T : Component
        {
            T loadedObject = Instantiate<T>(name, parent);
            loadedObject.transform.position = position;

            return loadedObject;
        }
    }
}