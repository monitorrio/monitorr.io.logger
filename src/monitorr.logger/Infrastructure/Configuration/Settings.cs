﻿namespace monitorr.logger.Infrastructure.Configuration
{
    public class Settings
    {
        public AppOptions App { get; set; }
        public EmailOptions Email { get; set; }
        public AwsOptions Aws { get; set; }
    }
}
