using System;

namespace News4Devs.Shared.DTOs
{
    public class ChatMessageDto
    {
        public long Id { get; set; }

        public Guid? FromUserId { get; set; }

        public Guid? ToUserId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
