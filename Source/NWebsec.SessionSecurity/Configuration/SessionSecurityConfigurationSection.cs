﻿// Copyright (c) André N. Klingsheim. See License.txt in the project root for license information.

using System.Configuration;
using NWebsec.SessionSecurity.Configuration.Validation;

namespace NWebsec.SessionSecurity.Configuration
{
    public class SessionSecurityConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("sessionFixationProtection", IsRequired = true)]
        [SessionFixationProtectionValidator]
        public SessionFixationProtectionConfigurationElement SessionFixationProtection
        {
            get
            {
                return (SessionFixationProtectionConfigurationElement)this["sessionFixationProtection"];
            }
            set
            {
                this["sessionFixationProtection"] = value;
            }
        }
    }

    public class SessionFixationProtectionConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = false)]
        public bool Enabled
        {
            get { return (bool) this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("useMachineKey", IsRequired = false, DefaultValue = true)]
        public bool UseMachineKey
        {
            get { return (bool)this["useMachineKey"]; }
            set { this["useMachineKey"] = value; }
        }

        [ConfigurationProperty("sessionAuthenticationKey", IsRequired = false)]
        public SessionAuthenticationKeyConfigurationElement SessionAuthenticationKey
        {
            get { return (SessionAuthenticationKeyConfigurationElement) this["sessionAuthenticationKey"]; }
            set { this["sessionAuthenticationKey"] = value; }
        }
    }

    public class SessionAuthenticationKeyConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("value", IsRequired = true, DefaultValue = "0000000000000000000000000000000000000000000000000000000000000000")]
        [SessionAuthenticationKeyValidator]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}
