using System;
using System.Collections.Generic;
using System.Text;

namespace ProfilePictureProcessor
{
    public record ProcessResult
    {
        public bool Success { get; init; }

        public string ErrorMessage { get; init; }

        public string ResultPath { get; init; }
    }
}
