//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DbMonitor.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class DatabaseStatus
    {
        public long ID { get; set; }
        public Nullable<long> SCID { get; set; }
        public string INSTANCE_NAME { get; set; }
        public string HOST_NAME { get; set; }
        public string VERSION { get; set; }
        public string STARTUP_TIME { get; set; }
        public string STATUS { get; set; }
        public string DATABASE_STATUS { get; set; }
        public Nullable<long> REALTIME { get; set; }
        public string CreationTime { get; set; }
        public string EditingTime { get; set; }
    }
}
