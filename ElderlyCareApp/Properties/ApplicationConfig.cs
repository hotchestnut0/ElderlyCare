using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElderlyCareApp.Utils;

namespace ElderlyCareApp.Properties
{
    internal class ApplicationConfig
    {
        public static ApplicationConfig Instance
        {
            get { return _instance = _instance ?? new ApplicationConfig(); }
        }


        private Dictionary<string, ApplicationConfigBase> _configs;
        private static ApplicationConfig? _instance;

        public ApplicationConfig() 
        {
            if (Instance != null)
                throw new InvalidOperationException("There is already a existing instance.");
            _configs = new();
        }

        public void Add(string key, ApplicationConfigBase value)
        {
            _configs[key] = value;
        }

        public void Save(string fileName)
        {
            FileStream fileStream = File.Create(fileName);
            using BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            foreach(var i in _configs)
            {
                binaryWriter.Write(i.Key);
                i.Value.Save(fileStream);
            }
        }

        public void Load(string fileName)
        {
            FileStream fileStream = File.OpenRead(fileName);
            using BinaryReader binaryReader = new(fileStream);

            while(fileStream.Position < fileStream.Length)
            {
                string name=binaryReader.ReadString();
                ApplicationConfigBase value;
            }
        }
    }
}
