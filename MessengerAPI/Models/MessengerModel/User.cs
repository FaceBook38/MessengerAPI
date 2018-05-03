namespace MessengerAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            GroupMessages = new HashSet<GroupMessage>();
            UserMessages = new HashSet<UserMessage>();
            UserMessages1 = new HashSet<UserMessage>();
            Groups = new HashSet<Group>();
        }

        public int UserId { get; set; }
        
        [StringLength(50)]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string UserEmail { get; set; }
        [MinLength(8,ErrorMessage = "Must be 8--12")]
        [MaxLength(12, ErrorMessage = "Must be 8--12")]
        [StringLength(16)]
        public string UserPassword { get; set; }

        [Column(TypeName = "image")]
        public byte[] UserImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupMessage> GroupMessages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMessage> UserMessages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMessage> UserMessages1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
