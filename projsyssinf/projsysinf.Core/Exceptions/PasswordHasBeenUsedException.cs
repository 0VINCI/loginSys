namespace profsysinf.Core.Exceptions;

public sealed class PasswordHasBeenUsedException() : CustomException("The given password has already been used.");