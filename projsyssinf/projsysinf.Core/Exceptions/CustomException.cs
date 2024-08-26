using System;

namespace profsysinf.Core.Exceptions;

public abstract class CustomException(string message) : Exception(message);