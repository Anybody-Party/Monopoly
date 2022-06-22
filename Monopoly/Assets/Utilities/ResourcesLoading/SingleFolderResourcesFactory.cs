using RH.Utilities.ResourcesLoading;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RH.Utilities.ResourcesLoading
{
    public class SingleFolderResourcesFactory : BaseResourcesFactory
    {
        private readonly string _folderName;

        public SingleFolderResourcesFactory(string folderName)
        {
            _folderName = folderName;
        }

        public override T Instantiate<T>(string name, Transform parent)
        {
            return base.Instantiate<T>(GetFullName(name), parent);
        }

        public override T Get<T>(string name)
        {
            return base.Get<T>(GetFullName(name));
        }

        private string GetFullName(string name) => Path.Combine(_folderName, name);
    }
}