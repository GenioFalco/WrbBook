//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnswerPractical
    {
        public int IdAnswer { get; set; }
        public byte[] PracricalAnswer { get; set; }
        public int IdUser { get; set; }
        public int IdTask { get; set; }
        public Nullable<int> GradeAnswer { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public string ExtensionAnsw { get; set; }
    
        public virtual User User { get; set; }
    }
}
