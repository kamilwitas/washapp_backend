﻿using System.Net;

namespace washapp.services.customers.domain.Exceptions.Abstract;

public abstract class DomainException : Exception
{
    public virtual string Code { get; }
    public virtual HttpStatusCode HttpStatusCode { get; }

    public DomainException(string message) :base(message)
    {
        
    }
}