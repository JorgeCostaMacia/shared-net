﻿namespace Shared.Exception.Domain;

public abstract class IExistException : IAggregateException
{
    protected IExistException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException) { }
    protected IExistException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException) : base(aggregateId, aggregateTypeId, aggregateCode ?? 409, aggregateOccurredAt, $"{message ?? "Exist Exception"}", innerException) { }
}