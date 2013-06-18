using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

using AppCommon;

namespace IlinkDb.Entity
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class EntityBase
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public long Deleted { get; set; }

    }
}
