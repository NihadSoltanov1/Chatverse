using Chatverse.Application.Common.Interfaces.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Infrastructure.MongoDB.Persistance.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string MessageCollectionName { get  ; set  ; }
        public string ConnectionString { get ; set; }
        public string DatabaseName { get ; set; }
    }
}
