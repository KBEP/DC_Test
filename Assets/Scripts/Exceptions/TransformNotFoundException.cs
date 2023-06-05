using System;

public class TransformNotFoundException : Exception
{
	public TransformNotFoundException (string relativePath)
	  : base (string.Format("Transform '{0}' not found.", relativePath)) {}
}
