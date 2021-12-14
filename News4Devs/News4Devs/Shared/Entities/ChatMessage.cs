using System;

namespace News4Devs.Shared.Entities
{
    public class ChatMessage
    {
        public long Id { get; set; }

        public Guid? FromUserId { get; set; }

        public Guid? ToUserId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual User FromUser { get; set; }

        public virtual User ToUser { get; set; }
    }
}
