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
    
    public partial class RoleAuthority
    {
        public long ID { get; set; }
        public Nullable<long> RID { get; set; }
        public Nullable<long> MAID { get; set; }
        public Nullable<long> IsEnabled { get; set; }
    
        public virtual ModuleAuthority ModuleAuthority { get; set; }
        public virtual Role Role { get; set; }
    }
}
