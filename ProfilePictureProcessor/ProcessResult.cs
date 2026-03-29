using System;
using System.Collections.Generic;
using System.Text;

namespace ProfilePictureProcessor
{
    public record ProcessResult
    {
        public bool Success { get; init; }

        public string ErrorMessage { get; init; }

        public string OutputPath { get; init; }

        public static ProcessResult Ok(string outputPath) => new() { Success = true, OutputPath = outputPath };
        public static ProcessResult Fail(string error) => new() { Success = false, ErrorMessage = error };
    }
}
