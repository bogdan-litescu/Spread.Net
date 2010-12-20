using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Spread.Core.Types;

namespace Spread.Core.Config
{

    [ActiveRecord(Table="Spread_AppSettings")]
    public class AppDbSetting : ActiveRecordBase<AppDbSetting>
    {
        public AppDbSetting()
        {
        }

        [PrimaryKey]
        public string Name { get; set; }

        [Property]
        public string Description { get; set; }

        [Property]
        public string Value { get; set; }

        [Property]
        public string DefaultValue { get; set; }
    }
}
