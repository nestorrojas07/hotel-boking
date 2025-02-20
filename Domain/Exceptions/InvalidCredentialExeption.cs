using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions;

public class InvalidCredentialExeption : Exception
{
    public InvalidCredentialExeption()
    {
    }

    public InvalidCredentialExeption(string? message) : base(message)
    {
    }

    public InvalidCredentialExeption(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidCredentialExeption(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
