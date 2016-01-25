namespace Go
{
    using System;

    public class ConfigurationException : Exception
    {
        private readonly string message;
        private readonly string configParam;

        public ConfigurationException(string configParam, string message) : base(message)
        {
            this.configParam = configParam;
            this.message = message;
        }

        public override string Message => message;

        public override string ToString() => $"Config Param {configParam} was not specified. {message}";
    }
}
