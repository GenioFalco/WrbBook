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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.AnswerPractical = new HashSet<AnswerPractical>();
            this.Results = new HashSet<Results>();
        }
    
        public int IDUser { get; set; }
        public byte[] ImageUser { get; set; }
        public string NameUser { get; set; }
        public string SurnameUser { get; set; }
        public string PatronymicUser { get; set; }
        public string EmailUser { get; set; }
        public string PasswordUser { get; set; }
        public int RoleUser { get; set; }
        public string SecretWordUser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerPractical> AnswerPractical { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Results> Results { get; set; }
        public virtual Role Role { get; set; }
    }
}
