﻿
namespace EventBus.Messages.Events
{
    public class BaseIntegrationEvent
    {

        //Co-Relation Id
        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }


        public BaseIntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }


        public BaseIntegrationEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }
    }
}
