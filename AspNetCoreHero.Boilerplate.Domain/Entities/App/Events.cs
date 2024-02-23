using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.App
{
    public class MiningRaceEvents : AuditableEntity, IAggregateRoot
    {

        [Column("Event Type")]
        public string EventType { get; set; }

        [Column("data")]
        public string Data { get; set; }



    }
}
