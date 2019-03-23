using System;

namespace EquationsParser.Models
{
    internal sealed class Config
    {
        public ProgramMode ProgramMode { get; set; } = ProgramMode.NotChosen;

        public string InputFilepath { get; set; }

        public string OutputFilepath { get; set; } = $"{DateTime.Now:ddMMyyyyHHmmss}.out";
    }
}
