//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBMSCourseStudentTesting.Entityies
{
    using System;
    using System.Collections.Generic;
    
    public partial class GroupTesting
    {
        public Nullable<int> idTest { get; set; }
        public Nullable<int> idAcademicGroup { get; set; }
        public System.DateTime expirationDate { get; set; }
        public bool isAvailable { get; set; }
    
        public virtual AcademicGroup AcademicGroup { get; set; }
        public virtual Test Test { get; set; }
    }
}
