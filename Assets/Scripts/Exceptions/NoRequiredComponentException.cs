using System;

public class NoRequiredComponentException : Exception
{
	public NoRequiredComponentException (Type componentType, string relativePathToComponent)
	  : base (string.Format("Component '{0}' not found in '{1}'.", componentType, relativePathToComponent)) {}

	public NoRequiredComponentException (Type componentType)
	  : base (string.Format("Component '{0}' not found.", componentType)) {}
}
