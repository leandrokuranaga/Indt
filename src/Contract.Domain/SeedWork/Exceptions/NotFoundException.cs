using System.Diagnostics.CodeAnalysis;

namespace Contract.Domain.SeedWork.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class NotFoundException : Exception
{
}