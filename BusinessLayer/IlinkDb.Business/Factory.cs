using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using IlinkDb.Data;
using IlinkDb.Data.DynamoDb;
using IlinkDb.Data.EntityFramework;
using IlinkDb.Data.MemoryDb;
using IlinkDb.Data.PivotalApi;

namespace IlinkDb.Business
{
    internal class Factory
    {
        private static volatile Factory _factory;
        private static readonly object _syncRoot = new Object();

        public static Factory Current
        {
            get
            {
                if (_factory == null)
                {
                    lock (_syncRoot)
                    {
                        if (_factory == null)
                            _factory = new Factory();
                    }
                }
                return _factory;
            }
        }

        private readonly IRepository _repository;

        private Factory()
        {
            string repositoryType = ConfigurationManager.AppSettings["RepositoryType"];

            if (string.IsNullOrEmpty(repositoryType))
            {
                repositoryType = "EntityFramework";
            }

            switch (repositoryType.ToLower())
            {
                case "entity":
                case "entityframework":
                    _repository = new RepositoryEntityFramework();
                    break;

                case "dynamodb":
                    _repository = new RepositoryDynamoDb();
                    break;
                case "pivot":
                    _repository = new RepositoryPivotal();
                    break;

                case "ram":
                case "memory":
                    _repository = new RepositoryMemory();
                    break;

                default:
                    _repository = new RepositoryMemory();
                    break;
            }
        }

        public IRepository Repository
        {
            get { return _repository; }
        }
    }
}
