// Guids.cs
// MUST match guids.h
using System;

namespace Company.WomanOfParis
{
    static class GuidList
    {
        public const string guidWomanOfParisPkgString = "79474e50-b3de-4538-8bed-1a05b98437da";
        public const string guidWomanOfParisCmdSetString = "c57778de-755b-4c84-8dfe-d619d8bb3de5";

        public static readonly Guid guidWomanOfParisCmdSet = new Guid(guidWomanOfParisCmdSetString);
    };
}