﻿using System;


namespace Mason.IssueTracker.Server.Domain.Exceptions
{
  public class MissingResourceException : Exception
  {
    public MissingResourceException(string msg, params object[] p)
      : base(string.Format(msg, p))
    {
    }
  }
}
