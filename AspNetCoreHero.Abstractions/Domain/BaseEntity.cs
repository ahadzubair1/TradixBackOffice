﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreHero.Abstractions.Domain
{

    public abstract class BaseEntity : BaseEntity<DefaultIdType>
    {
        protected BaseEntity() => Id = NewId.Next().ToGuid();
    }

    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; protected set; } = default!;

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; } = new();
    }
}