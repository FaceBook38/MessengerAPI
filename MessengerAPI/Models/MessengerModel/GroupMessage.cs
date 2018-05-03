namespace MessengerAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupMessage")]
    public partial class GroupMessage
    {
        [Key]
        public int MessageId { get; set; }

        [StringLength(550)]
        public string MessageContent { get; set; }

        public DateTime? MessageDate { get; set; }

        public int? SenderId { get; set; }

        public int? groupId { get; set; }

        public bool? Read { get; set; }

        public bool? Received { get; set; }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }
    }
}
