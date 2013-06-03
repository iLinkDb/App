using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace IlinkDb.Entity
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Tenant : EntityBase
    {
        [JsonProperty(PropertyName = "domain")]
        [Display(Name = "Domain")]
        [Required(ErrorMessage = "Domain is required.")]
        public string Domain { get; set; }
    }
}
